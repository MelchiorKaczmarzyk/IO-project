using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IOProject.Models;
using IOProject.ViewModels;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace IOProject.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly IOProjectDbContext _context;
        private readonly IHostingEnvironment? Enviroment;

        public ApplicationsController(IOProjectDbContext context, IHostingEnvironment? enviroment)
        {
            _context = context;
            Enviroment = enviroment;
        }

        // GET: Application
        public async Task<IActionResult> Index()
        {
            return View(await _context.Application.ToListAsync());
        }

        public async Task<IActionResult> ShowProjectApplications(int HelpProjectID)
        {
            return View("Index", await _context.Application.Where(j => j.ProjectID.Equals(HelpProjectID)).ToListAsync());
        }
        
        public async Task<IActionResult> MyApplications()
        {
            return View("Index", await _context.Application.Where(j => j.ApplicantID.Equals(User.FindFirstValue(ClaimTypes.NameIdentifier))).ToListAsync());
        }

        // GET: Application/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .Where(j => j.Id.Equals(id))
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        /*// GET: Application/Create
        public IActionResult Create()
        {
            ViewData["ApplicantID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }*/

        public IActionResult Create(int ProjectID)
        {
            ApplicationViewModel model = new ApplicationViewModel();
            ViewBag.ProjectID = ProjectID;
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ApplicationViewModel model, int ProjectID)
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
                Application newApplication = new Application
                {
                    ApplicantID = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    WhenCreated = DateTime.Now,
                    ProjectID = ProjectID,
                    FileAttachments = fileAttachments,
                };
                _context.Application.Add(newApplication);
                _context.SaveChanges();
                return RedirectToAction("ApplicationAdded");
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult ApplicationAdded() => View();
       /* // POST: Application/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WhenCreated,FileAttachments,ApplicantID,ProjectID")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantID"] = new SelectList(_context.Users, "Id", "Id", application.ApplicantID);
            return View(application);
        }*/

        // GET: Application/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .Where(j => j.Id.Equals(id))
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Application.FindAsync(id);
            int tempId = application.ProjectID;
            if (application != null)
            {
                _context.Application.Remove(application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { HelpProjectID =tempId});
        }

        private bool ApplicationExists(int id)
        {
            return _context.Application.Any(e => e.Id == id);
        }

        public IActionResult DownloadFile(String outputFilePath)
        {

            if (!System.IO.File.Exists(outputFilePath))
            {
                // Return a 404 Not Found error if the file does not exist
                return NotFound();
            }

            string fileExtension = Path.GetExtension(outputFilePath);
            if (fileExtension == ".pdf")
            {
                //open pdf in new tab
                return File(System.IO.File.ReadAllBytes(outputFilePath), "application/pdf");
            }
            var fileInfo = new System.IO.FileInfo(outputFilePath);
            Response.ContentType = "application/" + fileExtension;
            Response.Headers.Append("Content-Disposition", "attachment;filename=\"" + fileInfo.Name + "\"");
            Response.Headers.Append("Content-Length", fileInfo.Length.ToString());

            // Send the file to the client
            return File(System.IO.File.ReadAllBytes(outputFilePath), "application/" + fileExtension, fileInfo.Name);
        }
    }
}
