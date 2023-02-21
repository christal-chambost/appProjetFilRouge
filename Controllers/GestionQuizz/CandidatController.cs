using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppProjetFilRouge.Data;
using AppProjetFilRouge.Models;

namespace AppProjetFilRouge.Controllers.GestionQuizz
{
    public class CandidatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CandidatController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Candidat
        public async Task<IActionResult> Index()
        {
              return _context.CandidatViewModel != null ? 
                          View(await _context.CandidatViewModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CandidatViewModel'  is null.");
        }

        // GET: Candidat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CandidatViewModel == null)
            {
                return NotFound();
            }

            var candidatViewModel = await _context.CandidatViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidatViewModel == null)
            {
                return NotFound();
            }

            return View(candidatViewModel);
        }

        // GET: Candidat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,FirstName,LastName,Email,PhoneNumber,ABirthDate")] CandidatViewModel candidatViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidatViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(candidatViewModel);
        }

        // GET: Candidat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CandidatViewModel == null)
            {
                return NotFound();
            }

            var candidatViewModel = await _context.CandidatViewModel.FindAsync(id);
            if (candidatViewModel == null)
            {
                return NotFound();
            }
            return View(candidatViewModel);
        }

        // POST: Candidat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,FirstName,LastName,Email,PhoneNumber,ABirthDate")] CandidatViewModel candidatViewModel)
        {
            if (id != candidatViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidatViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatViewModelExists(candidatViewModel.Id))
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
            return View(candidatViewModel);
        }

        // GET: Candidat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CandidatViewModel == null)
            {
                return NotFound();
            }

            var candidatViewModel = await _context.CandidatViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidatViewModel == null)
            {
                return NotFound();
            }

            return View(candidatViewModel);
        }

        // POST: Candidat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CandidatViewModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CandidatViewModel'  is null.");
            }
            var candidatViewModel = await _context.CandidatViewModel.FindAsync(id);
            if (candidatViewModel != null)
            {
                _context.CandidatViewModel.Remove(candidatViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidatViewModelExists(int id)
        {
          return (_context.CandidatViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
