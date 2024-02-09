using IOProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using Microsoft.Extensions.Hosting;
using IOProject.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.IdentityModel.Tokens;

namespace IOProject.Controllers
{
    public class HelpProjectController : Controller
    {
        private readonly IHelpProjectRepos _helpProjectRepos;
        //private readonly IFileAttachmentRepos _fileRepos;

        public IActionResult PrintProjects()
        {
            return View(_helpProjectRepos.AllProjects);
        }

        public IActionResult DesignProject() => View();

        [HttpPost]
        public IActionResult DesignProject(HelpProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fileAttachments = new List<string>();
                string filePath = string.Empty;
                string uniqueFileName = string.Empty;
                if (model.Thumbnail != null &&
                     model.Thumbnail.ContentType.StartsWith("image"))
                {
                    string uploadsFolder = Path.Combine(Enviroment.WebRootPath, "Files");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + 
                        model.Thumbnail.FileName;
                    filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    //fileAttachments.Add(filePath);

                    model.Thumbnail.CopyTo(new FileStream(filePath, FileMode.Create));

                    if (!model.Attachments.IsNullOrEmpty())
                    {
                        foreach (var attachment in model.Attachments)
                        {
                            if (attachment != null)
                            {
                                var lol = attachment.ContentType;
                                uniqueFileName = Guid.NewGuid().ToString() + "_" +
                                    attachment.FileName;
                                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                                attachment.CopyTo(new FileStream(filePath, FileMode.Create));
                                fileAttachments.Add(filePath);
                            }
                        }
                    }

                }

                HelpProject newHelpProject = new HelpProject
                {
                    Title = model.Title,
                    ShortDescription = model.ShortDescription,
                    LongDescription = model.LongDescription,
                    WhenCreated = DateTime.Now,
                    Thumbnail = filePath,
                    FileAttachments = fileAttachments
                };
                _helpProjectRepos.AddHelpProject(newHelpProject);
                return RedirectToAction("ProjectAdded");
            }
            else
            {
                return View(model);
            }
            
        }

        public IActionResult ProjectAdded() => View();

        private readonly IHostingEnvironment? Enviroment;
        public HelpProjectController(IHelpProjectRepos helpProjectRepos, 
            IHostingEnvironment enviroment)

        {
            Enviroment = enviroment;
            _helpProjectRepos = helpProjectRepos;
        }

    }
}
