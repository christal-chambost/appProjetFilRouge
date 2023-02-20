using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppProjetFilRouge.Data;
using AppProjetFilRouge.Data.Entities;
using AppProjetFilRouge.Models;

namespace AppProjetFilRouge.Controllers
{
    public class RolesJLController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RolesJLController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RolesJL
        public async Task<IActionResult> Index()
        {
              return _context.RoleViewModelJL != null ? 
                          View(await _context.RoleViewModelJL.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.RoleViewModelJL'  is null.");
        }

        // GET: RolesJL/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RoleViewModelJL == null)
            {
                return NotFound();
            }

            var roleViewModelJL = await _context.RoleViewModelJL
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleViewModelJL == null)
            {
                return NotFound();
            }

            return View(roleViewModelJL);
        }

        // GET: RolesJL/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RolesJL/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NormalizedName,ConcurrencyStamp")] RoleViewModelJL roleViewModelJL)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleViewModelJL);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roleViewModelJL);
        }

        // GET: RolesJL/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RoleViewModelJL == null)
            {
                return NotFound();
            }

            var roleViewModelJL = await _context.RoleViewModelJL.FindAsync(id);
            if (roleViewModelJL == null)
            {
                return NotFound();
            }
            return View(roleViewModelJL);
        }

        // POST: RolesJL/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NormalizedName,ConcurrencyStamp")] RoleViewModelJL roleViewModelJL)
        {
            if (id != roleViewModelJL.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleViewModelJL);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleViewModelJLExists(roleViewModelJL.Id))
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
            return View(roleViewModelJL);
        }

        // GET: RolesJL/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RoleViewModelJL == null)
            {
                return NotFound();
            }

            var roleViewModelJL = await _context.RoleViewModelJL
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleViewModelJL == null)
            {
                return NotFound();
            }

            return View(roleViewModelJL);
        }

        // POST: RolesJL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RoleViewModelJL == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RoleViewModelJL'  is null.");
            }
            var roleViewModelJL = await _context.RoleViewModelJL.FindAsync(id);
            if (roleViewModelJL != null)
            {
                _context.RoleViewModelJL.Remove(roleViewModelJL);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleViewModelJLExists(int id)
        {
          return (_context.RoleViewModelJL?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
