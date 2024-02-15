using Microsoft.EntityFrameworkCore;

namespace IOProject.Models
{
    public class Application
    {
        public int Id { get; set; }
        public DateTime WhenCreated { get; set; }
        public List<string> FileAttachments { get; set; }
        public string ApplicantID { get; set; }
        public int ProjectID { get; set; }
    }
}
 