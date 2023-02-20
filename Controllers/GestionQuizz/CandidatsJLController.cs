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
    public class CandidatsJLController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CandidatsJLController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CandidatsJL
        public async Task<IActionResult> Index()
        {
            // Récupère un tableau d'objets Technologies en base et convertit en List.
            var candidatList = await _context.CandidatViewModelJL.ToListAsync();

            // Ceéer une liste vide d'objet Candidat ViewModel 
            var listCandidatsViewModel = new List<CandidatViewModelJL>();

            // itére sur notre liste de techno récupérée en base
            foreach (CandidatViewModelJL Candidat in candidatList)
            {
                // ajoute à la liste de candidatviewmodel le retour de la fonction
                // CastToCandidatViewModel()
                listCandidatsViewModel.Add(CastToCandidatViewModel(Candidat));
            };

            return View(listCandidatsViewModel);
            /*_context.Technologies != null ? 
                  View(await _context.Technologies.ToListAsync()) :
                  Problem("Entity set 'ApplicationDbContext.Technologies'  is null.");*/

            /*_context.CandidatViewModelJL != null ? 
                  View(await _context.CandidatViewModelJL.ToListAsync()) :
                  Problem("Entity set 'ApplicationDbContext.CandidatViewModelJL'  is null.");*/
        }

        // GET: CandidatsJL/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CandidatViewModelJL == null)
            {
                return NotFound();
            }

            var candidatViewModelJL = await _context.CandidatViewModelJL
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidatViewModelJL == null)
            {
                return NotFound();
            }

            return View(candidatViewModelJL);
        }

        // GET: CandidatsJL/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CandidatsJL/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,BLockoutEnd,LockoutEnabled,AccessFailedCount,ABirthDate,FirstName,HandleBy,LastName,Discriminator")] CandidatViewModelJL candidatViewModelJL)
        public async Task<IActionResult> Create(CandidatViewModelJL candidatViewModelJL)
        {
            var landidatCreateVar = CastToCandidat(candidatViewModelJL);

            if (!ModelState.IsValid)
            {
                _context.Add(candidatViewModelJL);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(landidatCreateVar);
        }

        // GET: CandidatsJL/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CandidatViewModelJL == null)
            {
                return NotFound();
            }

            var candidatViewModelJL = await _context.CandidatViewModelJL.FindAsync(id);
            if (candidatViewModelJL == null)
            {
                return NotFound();
            }
            return View(candidatViewModelJL);
            //return View(CastToTechnologyViewModel(technologyById));
        }

        // POST: CandidatsJL/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,BLockoutEnd,LockoutEnabled,AccessFailedCount,ABirthDate,FirstName,HandleBy,LastName,Discriminator")] CandidatViewModelJL candidatViewModelJL)
        {
            if (id != candidatViewModelJL.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidatViewModelJL);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatViewModelJLExists(candidatViewModelJL.Id))
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
            return View(candidatViewModelJL);
        }

        // GET: CandidatsJL/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CandidatViewModelJL == null)
            {
                return NotFound();
            }

            var candidatViewModelJL = await _context.CandidatViewModelJL
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidatViewModelJL == null)
            {
                return NotFound();
            }

            return View(candidatViewModelJL);
        }

        // POST: CandidatsJL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CandidatViewModelJL == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CandidatViewModelJL'  is null.");
            }
            var candidatViewModelJL = await _context.CandidatViewModelJL.FindAsync(id);
            if (candidatViewModelJL != null)
            {
                _context.CandidatViewModelJL.Remove(candidatViewModelJL);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidatViewModelJLExists(int id)
        {
          return (_context.CandidatViewModelJL?.Any(e => e.Id == id)).GetValueOrDefault();
        }

                public CandidatViewModelJL CastToCandidat(CandidatViewModelJL candidatViewModelJL)
        {
            var CandidatVar = new CandidatViewModelJL
            {
                FirstName = candidatViewModelJL.FirstName,
                Id = candidatViewModelJL.Id,
            };
            return CandidatVar;

        }

        public CandidatViewModelJL CastToCandidatViewModel(CandidatViewModelJL candidatViewModelJL)
        {
            var CandidatViewModelVar = new CandidatViewModelJL
            {
                FirstName = candidatViewModelJL.FirstName,
                Id = candidatViewModelJL.Id,

            };
            return CandidatViewModelVar;

        }

    }
}
