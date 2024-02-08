namespace IOProject.Models
{
    public class HelpProjectRepos : IHelpProjectRepos
    {
        private readonly IOProjectDbContext _context;

        public HelpProjectRepos(IOProjectDbContext context)
        {
            _context = context;
        }

        public void AddHelpProject(HelpProject helpProject)
        {
            _context.HelpProjects.Add(helpProject);
            _context.SaveChanges();
        }

        public IEnumerable<HelpProject> AllProjects
        {
            get
            {
                return _context.HelpProjects;
            }
        }

        public IEnumerable<HelpProject> Latest
        {
            get 
            {
                return _context.HelpProjects.Where(x => DateTime.Now.Day - x.WhenCreated.Day < 2);
            }
        }

        public HelpProject GetById(int id)
        {
            return _context.HelpProjects.FirstOrDefault(x => x.Id == id);
        }
    }
}
