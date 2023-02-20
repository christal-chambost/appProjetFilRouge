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
    public class AgentsJLController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgentsJLController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AgentsJL
        public async Task<IActionResult> Index()
        {
            // Récupère un tableau d'objets Agent en base et convertit en List.
            var agentListVar = await _context.AgentViewModelJL.ToListAsync();

            // Ceéer une liste vide d'objet AgentViewModelJL 
            var agentlistViewModelVar = new List<AgentViewModelJL>();

            // itére sur notre liste de techno récupérée en base
            foreach (AgentViewModelJL agent in agentListVar)
            {
                // ajoute à la liste de technoviewmodel le retour de la fonction
                // CastToTechnologyViewModel()
                agentlistViewModelVar.Add(CastToAgentViewModel(agent));
            };

            return View(agentlistViewModelVar);
            /*_context.AgentViewModelJL != null ? 
                          View(await _context.AgentViewModelJL.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AgentViewModelJL'  is null.");*/
        }

        // GET: AgentsJL/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AgentViewModelJL == null)
            {
                return NotFound();
            }

            var agentViewModelJL = await _context.AgentViewModelJL
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentViewModelJL == null)
            {
                return NotFound();
            }

            return View(agentViewModelJL);
        }

        // GET: AgentsJL/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgentsJL/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(AgentViewModelJL agentViewModelJL)
        //public async Task<IActionResult> Create([Bind("Id,UserName,Email,PhoneNumber,FirstName,LastName")] AgentViewModelJL agentViewModelJL)
        {
            var agentVar = CastToAgent(agentViewModelJL);

            if (!ModelState.IsValid)
            {
                _context.Add(agentViewModelJL);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agentViewModelJL);
        }

        // GET: AgentsJL/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AgentViewModelJL == null)
            {
                return NotFound();
            }

            var agentById = await _context.AgentViewModelJL.FindAsync(id);

            if (agentById == null)
            {
                return NotFound();
            }
            return View(CastToAgentViewModel(agentById));
            //return View(agentViewModelJL);
        }

        // POST: AgentsJL/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Email,PhoneNumber,FirstName,LastName")] AgentViewModelJL agentViewModelJL)
        public async Task<IActionResult> Edit(int id, AgentViewModelJL agentViewModelJL)
        {

            var agentById = CastToAgent(agentViewModelJL);

            if (id != agentViewModelJL.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentViewModelJL);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentViewModelJLExists(agentViewModelJL.Id))
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
            return View(agentViewModelJL);
        }

        // GET: AgentsJL/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AgentViewModelJL == null)
            {
                return NotFound();
            }

            var agentById = await _context.AgentViewModelJL.FirstOrDefaultAsync(m => m.Id == id);

            if (agentById == null)
            {
                return NotFound();
            }

            //return View(agentById);
            return View(CastToAgentViewModel(agentById));
        }

        // POST: AgentsJL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AgentViewModelJL == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AgentViewModelJL'  is null.");
            }
            var agentViewModelId = await _context.AgentViewModelJL.FindAsync(id);
            if (agentViewModelId != null)
            {
                _context.AgentViewModelJL.Remove(agentViewModelId);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentViewModelJLExists(int id)
        {
          return (_context.AgentViewModelJL?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public AgentViewModelJL CastToAgent(AgentViewModelJL agentViewModelJL)
        {
            var agentViewModel = new AgentViewModelJL
            {
                FirstName = agentViewModelJL.LastName,
                Id = agentViewModelJL.Id,
            };
            return agentViewModel;
        }

        public AgentViewModelJL CastToAgentViewModel( AgentViewModelJL agentViewModelJL)
        {
            var agentViewModelVar = new AgentViewModelJL
            {
                FirstName = agentViewModelJL.LastName,
                Id = agentViewModelJL.Id,
            };
            return agentViewModelVar;
        }
    }
}
