using IOProject.CustomValidation;
using IOProject.Models;
using System.ComponentModel.DataAnnotations;

namespace IOProject.ViewModels
{
    public class ApplicationViewModel
    {
        [Required(ErrorMessage ="At least one attachment is required")]
        [ValidationAttachments(ErrorMessage = "One of files is too big or is of invalid type")]
        public List<IFormFile>? Attachments { get; set; }
    }
}
