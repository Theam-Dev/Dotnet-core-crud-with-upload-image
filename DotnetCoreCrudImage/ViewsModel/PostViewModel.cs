using System.ComponentModel.DataAnnotations;

namespace DotnetCoreCrudImage.ViewsModal
{
    public class PostViewModel : EditImageViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
    }
}
