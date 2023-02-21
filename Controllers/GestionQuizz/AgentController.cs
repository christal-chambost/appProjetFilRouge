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
    public class AgentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Agent
        public async Task<IActionResult> Index()
        {
            return _context.AgentViewModel != null ?
                        View(await _context.AgentViewModel.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.AgentViewModel'  is null.");
        }

        // GET: Agent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AgentViewModel == null)
            {
                return NotFound();
            }

            var agentViewModel = await _context.AgentViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentViewModel == null)
            {
                return NotFound();
            }

            return View(agentViewModel);
        }

        // GET: Agent/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Email,PhoneNumber,FirstName,LastName")] AgentViewModel agentViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agentViewModel);
        }

        // GET: Agent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AgentViewModel == null)
            {
                return NotFound();
            }

            var agentViewModel = await _context.AgentViewModel.FindAsync(id);
            if (agentViewModel == null)
            {
                return NotFound();
            }
            return View(agentViewModel);
        }

        // POST: Agent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Email,PhoneNumber,FirstName,LastName")] AgentViewModel agentViewModel)
        {
            if (id != agentViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentViewModelExists(agentViewModel.Id))
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
            return View(agentViewModel);
        }

        // GET: Agent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AgentViewModel == null)
            {
                return NotFound();
            }

            var agentViewModel = await _context.AgentViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentViewModel == null)
            {
                return NotFound();
            }

            return View(agentViewModel);
        }

        // POST: Agent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AgentViewModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AgentViewModel'  is null.");
            }
            var agentViewModel = await _context.AgentViewModel.FindAsync(id);
            if (agentViewModel != null)
            {
                _context.AgentViewModel.Remove(agentViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentViewModelExists(int id)
        {
            return (_context.AgentViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
