namespace IOProject.Models
{
    public class FileAttachmentRepos :IFileAttachmentRepos
    {
        private readonly IOProjectDbContext _context;

        public FileAttachmentRepos(IOProjectDbContext context)
        {
            _context = context;
        }
        public IEnumerable<FileAttachment> AllFiles
        {
            get
            {
                return _context.FileAttachments;
            }
        }
        public FileAttachment GetFileById (int id)
        {
            return _context.FileAttachments.FirstOrDefault(x => x.Id == id);
        }
    }
}
