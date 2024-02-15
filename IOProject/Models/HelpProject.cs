using System.ComponentModel.DataAnnotations.Schema;

namespace IOProject.Models
{
    public class HelpProject
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public DateTime WhenCreated { get; set; }
        public string? Thumbnail { get; set; }
        public List<string>? FileAttachments { get; set; }
        public List<string>? Tags { get; set; }
        public string? OwnerID { get; set; }
        public virtual SystemUser? Owner { get; set; } = null!;
        public DateTime WhenEnds { get; set; }
        public uint targetAmount { get; set; }

        public bool isActive { get; set; } = true;
    }
}
