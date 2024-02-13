using IOProject.CustomValidation;
using IOProject.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IOProject.ViewModels
{
    public class HelpProjectViewModel
    {
        [Required(ErrorMessage ="Title is required. 50 characters max")]
        [Display(Name = "Title")]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Short description is required. 200 characters max")]
        [Display(Name = "Short description")]
        [StringLength(200)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Long description is required")]
        [Display(Name = "Long description")]
        [StringLength(2000)]
        public string LongDescription { get; set; } = string.Empty;

        public DateTime WhenCreated { get; set; }

        [Required(ErrorMessage = "Thumbnail is required")]
        [ValidationImage(ErrorMessage = "File should be an image")]
        public IFormFile? Thumbnail { get; set; }

        public List<IFormFile>? Attachments {get; set;}


        [ValidationTags(ErrorMessage = "Choose at least one tag")]

        public List<string>? Tags { get; set;}

        public List<Checkbox> Checkboxes;
    }
}
