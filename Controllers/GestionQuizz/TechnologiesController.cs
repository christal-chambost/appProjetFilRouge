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
    public class TechnologiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TechnologiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Technologies
        public async Task<IActionResult> Index()
        {
            // Récupère un tableau d'objets Technologies en base et convertit en List.
            var technologyList = await _context.Technologies.ToListAsync();
            
            // Ceéer une liste vide d'objet TechnologieViewModel 
            var listTechnologiesViewModel = new List<TechnologyViewModel>();
            
            // itére sur notre liste de techno récupérée en base
            foreach (Technology technology in technologyList)
            {
                // ajoute à la liste de technoviewmodel le retour de la fonction
                // CastToTechnologyViewModel()
                listTechnologiesViewModel.Add(CastToTechnologyViewModel(technology));
            };

            return View(listTechnologiesViewModel);
         /*_context.Technologies != null ? 
               View(await _context.Technologies.ToListAsync()) :
               Problem("Entity set 'ApplicationDbContext.Technologies'  is null.");*/
        }

        // GET: Technologies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Technologies == null)
            {
                return NotFound();
            }

            var technology = await _context.Technologies
                .FirstOrDefaultAsync(m => m.TechnologyId == id);
            if (technology == null)
            {
                return NotFound();
            }

            return View(technology);
        }

        // GET: Technologies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Technologies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TechnologyViewModel technologyViewModel)
        {

            var technology = CastToTechnology(technologyViewModel);

            if (ModelState.IsValid)
            {
                _context.Add(technology);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(technology);
        }

        // GET: Technologies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var technologyById = await _context.Technologies.FindAsync(id);

            if (technologyById == null)
            {
                return NotFound();
            }
            return View(CastToTechnologyViewModel(technologyById));
        }

        // POST: Technologies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TechnologyViewModel technologyViewModel)
        {

            var technologyById = CastToTechnology(technologyViewModel);

            if (id != technologyViewModel.TechnologyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technologyById);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechnologyExists(technologyViewModel.TechnologyId))
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
            return View(technologyViewModel);
        }

        // GET: Technologies/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var technologyById = await _context.Technologies
                .FirstOrDefaultAsync(m => m.TechnologyId == id);
            if (technologyById == null)
            {
                return NotFound();
            }

            return View(CastToTechnologyViewModel(technologyById));
        }

        // POST: Technologies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (_context.Technologies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Technologies'  is null.");
            }

            var technologyById = await _context.Technologies.FindAsync(id);

            if (technologyById != null)
            {
                _context.Technologies.Remove(technologyById);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechnologyExists(int id)
        {
          return (_context.Technologies?.Any(e => e.TechnologyId == id)).GetValueOrDefault();
        }


        public Technology CastToTechnology(TechnologyViewModel technologyViewModel)
        {
            var technology = new Technology
            {
                Name = technologyViewModel.Name,
                TechnologyId = technologyViewModel.TechnologyId,


            };
            return technology;

        }

        public TechnologyViewModel CastToTechnologyViewModel(Technology technology)
        {
            var technologyViewModel = new TechnologyViewModel
            {
                Name = technology.Name,
                TechnologyId = technology.TechnologyId,

            };
            return technologyViewModel;

        }
    }
}
