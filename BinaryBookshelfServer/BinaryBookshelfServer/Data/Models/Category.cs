using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinaryBookshelfServer.Data.Models
{
    [Table("Categories")]
    [Index(nameof(Label))]
    public class Category
    {
        /// <summary>
        /// The unique id and primary key for this category
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Category label
        /// </summary>
        [StringLength(25)]
        [Unicode(false)]
        public required string Label { get; set; }

        /// <summary>
        /// A collection of all categorizations
        /// </summary>
        public ICollection<Book> Books { get; } = [];
        public virtual ICollection<Categorization> Categorizations { get; } = [];
    }
}
