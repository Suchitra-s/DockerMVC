using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DockerMVC.Models
{
    [Table("Books")]
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;set; }

        [Required(ErrorMessage = "Book Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Book Title is required")]
        public string Title { get; set; }

        public int Price { get; set; }

    }
}
