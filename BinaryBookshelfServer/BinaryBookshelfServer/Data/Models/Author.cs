using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinaryBookshelfServer.Data.Models
{
    [Table("Authors")]
    [Index(nameof(Name))]
    public class Author
    {
        /// <summary>
        /// The unique id and primary key for this author
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Author's name
        /// </summary>
        [StringLength(50)]
        public required string Name { get; set; }

        /// <summary>
        /// Author's background information
        /// </summary>
        public string? Background { get; set; }

        /// <summary>
        /// A collection of all books written by author
        /// </summary>
        public ICollection<Book> Books { get; } = [];
    }
}
