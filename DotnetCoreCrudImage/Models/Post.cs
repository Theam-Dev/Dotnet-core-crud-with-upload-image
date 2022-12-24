using System.ComponentModel.DataAnnotations;

namespace DotnetCoreCrudImage.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
