namespace IOProject.Models
{
    public interface IFileAttachmentRepos
    {
        IEnumerable<FileAttachment> AllFiles { get; }
        FileAttachment GetFileById(int id);
    }
}
