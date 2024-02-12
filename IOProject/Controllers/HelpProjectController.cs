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

        public IActionResult PrintProjects()
        {
            return View(_helpProjectRepos.AllProjects);
        }

        public IActionResult DesignProject()
        {
            var model = new HelpProjectViewModel();
            model.Checkboxes = new List<Checkbox>
            {
            new Checkbox()
            {
                isChecked = false,
                description = "Medical procedure"
            },
            new Checkbox()
            {
                isChecked = false,
                description = "Rehabilitation"
            },
            new Checkbox()
            {
                isChecked = false,
                description = "Natural disasters"
            },
            new Checkbox()
            {
                isChecked = false,
                description = "Help for refugees"
            },
            new Checkbox()
            {
                isChecked = false,
                description = "Cultural event"
            },
        };
            return View(model);
        }

        [HttpPost]
        public IActionResult DesignProject(HelpProjectViewModel model)
        {
            string uploadsFolder = Path.Combine(Enviroment.WebRootPath, "Files");
            if (ModelState.IsValid)
            {
                var fileAttachments = new List<string>();
                string filePath = string.Empty;
                string uniqueFileName = string.Empty;

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
                            Stream stream = new FileStream(filePath, FileMode.Create);
                            attachment.CopyTo(stream);
                            stream.Close();
                            fileAttachments.Add(filePath);
                        }
                    }
                }
                if (model.Thumbnail != null &&
                model.Thumbnail.ContentType.StartsWith("image"))
                {
                    uniqueFileName = Guid.NewGuid().ToString() + "_" +
                    model.Thumbnail.FileName;
                    filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    Stream stream = new FileStream(filePath, FileMode.Create);
                    model.Thumbnail.CopyTo(stream);
                    stream.Close();
                }
                HelpProject newHelpProject = new HelpProject
                {
                    Title = model.Title,
                    ShortDescription = model.ShortDescription,
                    LongDescription = model.LongDescription,
                    WhenCreated = DateTime.Now,
                    Thumbnail = filePath,
                    FileAttachments = fileAttachments,
                    createdBy = User.Identity.Name,
                    Tags = model.Tags
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
