using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IOProject.Models;
using System.Security.Claims;

//Controller only for showing projects. Everything is auto generated unless commented otherwise.
namespace IOProject.Controllers
{
    public class HelpProjectsPrintController : Controller
    {
        private readonly IOProjectDbContext _context;

        public HelpProjectsPrintController(IOProjectDbContext context)
        {
            _context = context;
        }

        // GET: HelpProjectsPrint - shows a list of projects.
        public async Task<IActionResult> Index()
        {
            return View(await _context.HelpProjects.ToListAsync());
        }

        // GET: HelpProjectsPrint/ShowSearchForm -  simple form for search input, redirects to ShowSearchResult
        public IActionResult ShowSearchForm() => View();

        // POST: HelpProjectsPrint/ShowSearchResults - shows search results, uses the Index view instead of creating a new one.
        // SearchPhrase gets sent from ShowSearchResultForm form.
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.HelpProjects.Where(j => j.ShortDescription.Contains(SearchPhrase) && j.isActive).ToListAsync());
        }

        public ActionResult SupportProject(int? id)
        {
            HelpOffersController helpOffer = new HelpOffersController(_context);
            return Redirect("/HelpOffers/Create");
        }

        public async Task<IActionResult> OrganisationProjects()
        {
            return View("Index", await _context.HelpProjects.Where(j => j.OwnerID.Equals(User.FindFirstValue(ClaimTypes.NameIdentifier))).ToListAsync());
        }

        // GET: HelpProjectsPrint/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helpProject = await _context.HelpProjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helpProject == null)
            {
                return NotFound();
            }

            return View(helpProject);
        }
        //GET
        public async Task<IActionResult> Deactivate(int? id)
        {
            var helpProject = await _context.HelpProjects.FindAsync(id);
            if (helpProject != null)
            {
                helpProject.isActive = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ActionName("Deactivate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var helpProject = await _context.HelpProjects.FindAsync(id);
            if (helpProject != null)
            {
                helpProject.isActive = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: HelpProjectsPrint/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: HelpProjectsPrint/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helpProject = await _context.HelpProjects.FindAsync(id);
            if (helpProject == null)
            {
                return NotFound();
            }
            return View(helpProject);
        }

        // POST: HelpProjectsPrint/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ShortDescription,LongDescription,WhenCreated,Thumbnail,FileAttachments,Tags,WhenEnds,OwnerID,targetAmount")] HelpProject helpProject)
        {
            helpProject.OwnerID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != helpProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(helpProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HelpProjectExists(helpProject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(helpProject);
        }

        // GET: HelpProjectsPrint/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helpProject = await _context.HelpProjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helpProject == null)
            {
                return NotFound();
            }

            return View(helpProject);
        }

        // POST: HelpProjectsPrint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var helpProject = await _context.HelpProjects.FindAsync(id);
            if (helpProject != null)
            {
                _context.HelpProjects.Remove(helpProject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HelpProjectExists(int id)
        {
            return _context.HelpProjects.Any(e => e.Id == id);
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
            Response.ContentType = "application/"+fileExtension;
            Response.Headers.Append("Content-Disposition", "attachment;filename=\"" + fileInfo.Name + "\"");
            Response.Headers.Append("Content-Length", fileInfo.Length.ToString());
            
            // Send the file to the client
            return File(System.IO.File.ReadAllBytes(outputFilePath), "application/" + fileExtension, fileInfo.Name);
        }
    }
}
