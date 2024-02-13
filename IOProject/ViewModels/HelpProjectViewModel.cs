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
        public string LongDescription { get; set; } = string.Empty;

        public DateTime WhenCreated { get; set; }

        //[ValidationSize(ErrorMessage = "File is to big")]
        [ValidationImage(ErrorMessage = "File should be an image")]
        public IFormFile? Thumbnail { get; set; }

        //[ValidationAttachments(ErrorMessage = "One of the files is too big or has inproper type")]
        public List<IFormFile>? Attachments {get; set;}

        public List<string>? Tags { get; set;}

        public List<Checkbox> Checkboxes;

        [Required(ErrorMessage ="Closing date is required")]
        [Display(Name ="WhenEnds")]
        public DateTime WhenEnds { get; set; }

        [Required(ErrorMessage ="Fundraising goal is required.")]
        [Display(Name ="TargetAmount")]
        public uint targetAmount { get; set; }
    }
}
