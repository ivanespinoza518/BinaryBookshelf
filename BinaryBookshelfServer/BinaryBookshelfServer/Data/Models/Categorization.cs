using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinaryBookshelfServer.Data.Models
{
    [Table("Categorizations")]
    public class Categorization
    {
        [ForeignKey(nameof(Book))]
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        [ForeignKey(nameof(Category))]
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
