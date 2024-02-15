using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IOProject.Models;
using Microsoft.CodeAnalysis;
using Microsoft.Build.Evaluation;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace IOProject.Controllers
{
    public class HelpOffersController : Controller
    {
        static int projectId = 0;
        private readonly IOProjectDbContext _context;

        public HelpOffersController(IOProjectDbContext context)
        {
            _context = context;
        }

        // GET: HelpOffers
        public async Task<IActionResult> Index()
        {
            return View(await _context.HelpOffer.ToListAsync());
        }

        public async Task<IActionResult> ShowProjectOffers(int HelpProjectID)
        {
            return View("Index", await _context.HelpOffer.Where(j => j.ProjectId.Equals(HelpProjectID)).ToListAsync());
        }
        public async Task<IActionResult> MyOffers()
        {
            return View("Index", await _context.HelpOffer.Where(j => j.BenefactorId.Equals(User.FindFirstValue(ClaimTypes.NameIdentifier))).ToListAsync());
        }

        // GET: HelpOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helpOffer = await _context.HelpOffer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helpOffer == null)
            {
                return NotFound();
            }

            return View(helpOffer);
        }

        // GET: HelpOffers/Create
        public IActionResult Create(int ProjectID)
        {
            projectId= ProjectID;
            return View();
        }

        // POST: HelpOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,Description")] HelpOffer helpOffer)
        {
            helpOffer.ProjectId = projectId;
            helpOffer.BenefactorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            helpOffer.WhenCreated = DateTime.Now;
            _context.HelpOffer.Add(helpOffer);
            _context.SaveChanges();
            return RedirectToAction("OfferAdded");
        }

        public IActionResult OfferAdded() => View();

        public async Task<IActionResult> helpProject(int? id)
        {
            projectId = id.Value;
            return Redirect("/HelpOffers/Create");
        }

        // GET: HelpOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helpOffer = await _context.HelpOffer.FindAsync(id);
            if (helpOffer == null)
            {
                return NotFound();
            }
            return View(helpOffer);
        }

        // POST: HelpOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BenefactorId,ProjectId,Amount,Description,WhenCreated")] HelpOffer helpOffer)
        {
            if (id != helpOffer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(helpOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HelpOfferExists(helpOffer.Id))
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
            return View(helpOffer);
        }

        // GET: HelpOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helpOffer = await _context.HelpOffer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helpOffer == null)
            {
                return NotFound();
            }

            return View(helpOffer);
        }

        // POST: HelpOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var helpOffer = await _context.HelpOffer.FindAsync(id);
            if (helpOffer != null)
            {
                _context.HelpOffer.Remove(helpOffer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { HelpProjectID = helpOffer.ProjectId });
        }

        private bool HelpOfferExists(int id)
        {
            return _context.HelpOffer.Any(e => e.Id == id);
        }
    }
}
