﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IOProject.Models;

//Controller for showing projects. Mostly auto generated.
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

        // POST: HelpProjectsPrint/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View(await _context.HelpProjects.Where(j=>j.Title.Contains(SearchPhrase)).ToListAsync());
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ShortDescription,LongDescription,WhenCreated,Thumbnail,FileAttachments")] HelpProject helpProject)
        {
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
    }
}