﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BinaryBookshelfServer.Data;
using BinaryBookshelfServer.Data.Dto;
using BinaryBookshelfServer.Data.Models;

namespace BinaryBookshelfServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController(ApplicationDbContext context) : ControllerBase
    {
        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<ApiResult<AuthorDTO>>> GetAuthors(
            int pageIndex = 0,
            int pageSize = 10,
            string? sortColumn = null,
            string? sortOrder = null,
            string? filterColumn = null,
            string? filterQuery = null)
        {
            return await ApiResult<AuthorDTO>.CreateAsync(
                context.Authors.AsNoTracking()
                    .Select(a => new AuthorDTO()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Background = a.Background,
                        TotalBooks = a.Books!.Count
                    }),
                pageIndex,
                pageSize,
                sortColumn,
                sortOrder,
                filterColumn,
                filterQuery);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // GET: api/Authors/BooksByAuthor/5
        [HttpGet("BooksByAuthor/{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthor(int id)
        {
            return await context.Books.Where(b => b.AuthorId == id).ToListAsync();
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            context.Entry(author).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            context.Authors.Add(author);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            context.Authors.Remove(author);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return context.Authors.Any(e => e.Id == id);
        }

        [HttpPost]
        [Route("IsDupeField")]
        public bool IsDupeField(
            int authorId,
            string fieldName,
            string fieldValue)
        {
            switch (fieldName)
            {
                case "name":
                    return context.Authors.Any(
                    a => a.Name == fieldValue && a.Id != authorId);
                case "background":
                    return context.Authors.Any(
                    a => a.Background == fieldValue && a.Id != authorId);
                default:
                    return false;
            }
            //// Alternative approach (using System.Linq.Dynamic.Core)
            //// This method is more DRY but adds overhead from Linq.Dynamic.Core
            //return (ApiResult<Author>.IsValidProperty(fieldName, true))
            //? context.Authors.Any(
            //string.Format("{0} == @0 && Id != @1", fieldName),
            //fieldValue,
            //authorId)
            //: false;
        }
    }
}
