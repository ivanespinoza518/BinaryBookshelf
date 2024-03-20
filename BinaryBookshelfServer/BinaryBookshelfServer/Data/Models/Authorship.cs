using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinaryBookshelfServer.Data.Models
{
    [Table("Authorships")]
    public class Authorship
    {
        [ForeignKey(nameof(Author))]
        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        [ForeignKey(nameof(Book))]
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
