using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinaryBookshelfServer.Data.Models
{
    [Table("Books")]
    [Index(nameof(Title))]
    [Index(nameof(Price))]
    public class Book
    {
        /// <summary>
        /// The unique id and primary key for this Book
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Book title
        /// </summary>
        [StringLength(50)]
        [Unicode(false)]
        public required string Title { get; set; }

        /// <summary>
        /// Book subtitle
        /// </summary>
        [StringLength(100)]
        [Unicode(false)]
        public string? Subtitle { get; set; }

        /// <summary>
        /// Book image url
        /// </summary>
        [Unicode(false)]
        public required string ImageUrl { get; set; }

        /// <summary>
        /// Book description
        /// </summary>
        [Unicode(false)]
        public required string Description { get; set; }

        /// <summary>
        /// Book average price
        /// </summary>
        [Column(TypeName = "numeric(8, 2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// Book's international standard book number (ISBN-13)
        /// </summary>
        [StringLength(14)]
        [Unicode(false)]
        public required string Isbn13 { get; set;}

        /// <summary>
        /// A collection of all the authors that wrote the book
        /// [InverseProperty] annotation not included as it is only needed 
        /// when there is more than one relationship between the same types
        /// With a single relationship, the two navigations are paired
        /// automatically. See EF Core documentation on:
        /// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/mapping-attributes
        /// </summary>
        public ICollection<Author> Authors { get; } = []; 

        /// <summary>
        /// A collection of all categories that describe the book
        /// </summary>
        public ICollection<Category> Categories { get; } = [];
    }
}
