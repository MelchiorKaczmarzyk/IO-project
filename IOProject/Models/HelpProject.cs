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
    }
}
