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
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
              return _context.RoleViewModel != null ? 
                          View(await _context.RoleViewModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.RoleViewModel'  is null.");
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RoleViewModel == null)
            {
                return NotFound();
            }

            var roleViewModel = await _context.RoleViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleViewModel == null)
            {
                return NotFound();
            }

            return View(roleViewModel);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NormalizedName,ConcurrencyStamp")] RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roleViewModel);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RoleViewModel == null)
            {
                return NotFound();
            }

            var roleViewModel = await _context.RoleViewModel.FindAsync(id);
            if (roleViewModel == null)
            {
                return NotFound();
            }
            return View(roleViewModel);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NormalizedName,ConcurrencyStamp")] RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleViewModelExists(roleViewModel.Id))
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
            return View(roleViewModel);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RoleViewModel == null)
            {
                return NotFound();
            }

            var roleViewModel = await _context.RoleViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleViewModel == null)
            {
                return NotFound();
            }

            return View(roleViewModel);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RoleViewModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RoleViewModel'  is null.");
            }
            var roleViewModel = await _context.RoleViewModel.FindAsync(id);
            if (roleViewModel != null)
            {
                _context.RoleViewModel.Remove(roleViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleViewModelExists(int id)
        {
          return (_context.RoleViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
