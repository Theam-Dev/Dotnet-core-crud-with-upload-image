using System.ComponentModel.DataAnnotations;

namespace DotnetCoreCrudImage.ViewsModal
{
    public class ImageViewModel
    {
        [Required]
        [Display(Name = "Image")]
        public IFormFile? Image { get; set; }
    }
}
