namespace IOProject.Models
{
    public interface IHelpProjectRepos
    {
        IEnumerable<HelpProject> AllProjects { get; }
        IEnumerable<HelpProject> Latest { get; }

        void AddHelpProject(HelpProject helpProject);
        HelpProject GetById(int id);
    }
}
