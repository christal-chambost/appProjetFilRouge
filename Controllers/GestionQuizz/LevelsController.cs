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

namespace AppProjetFilRouge.Controllers.GestionQuizz
{
    public class LevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Levels
        public async Task<IActionResult> Index()
        {
            var levelList = await _context.Levels.ToListAsync();

            var listLevelsViewModel = new List<LevelViewModel>();

            foreach (Level level in levelList)
            {
                listLevelsViewModel.Add(CastToLevelViewModel(level));
            };

            return View(listLevelsViewModel);
        }

        // GET: Levels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Levels == null)
            {
                return NotFound();
            }

            var level = await _context.Levels
                .FirstOrDefaultAsync(m => m.LevelId == id);
            if (level == null)
            {
                return NotFound();
            }

            return View(level);
        }

        // GET: Levels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Levels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LevelViewModel levelViewModel)
        {
            
            var level = CastToLevel(levelViewModel);

            if (ModelState.IsValid)
            {
                _context.Add(level);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(level);
        }

        // GET: Levels/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
    
            var levelById = await _context.Levels.FindAsync(id);

            if (levelById == null)
            {
                return NotFound();
            }
            return View(CastToLevelViewModel(levelById));
        }

        // POST: Levels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LevelViewModel levelViewModel)
        {
            var levelById = CastToLevel(levelViewModel);

            if (id != levelViewModel.LevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(levelById);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LevelExists(levelViewModel.LevelId))
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
            return View(levelViewModel);
        }

        // GET: Levels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            var levelById = await _context.Levels
                .FirstOrDefaultAsync(m => m.LevelId == id);
            if (levelById == null)
            {
                return NotFound();
            }

            return View(CastToLevelViewModel(levelById));
        }

        // POST: Levels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Levels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Levels'  is null.");
            }

            var levelById = await _context.Levels.FindAsync(id);

            if (levelById != null)
            {
                _context.Levels.Remove(levelById);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LevelExists(int id)
        {
          return (_context.Levels?.Any(e => e.LevelId == id)).GetValueOrDefault();
        }

        public Level CastToLevel(LevelViewModel levelViewModel)
        {
            var level = new Level
            {
                Name = levelViewModel.Name,
                LevelId = levelViewModel.LevelId,


            };
            return level;

        }

        public LevelViewModel CastToLevelViewModel(Level level)
        {
            var levelViewModel = new LevelViewModel
            {
                Name = level.Name,
                LevelId = level.LevelId,

            };
            return levelViewModel;

        }
    }
}
