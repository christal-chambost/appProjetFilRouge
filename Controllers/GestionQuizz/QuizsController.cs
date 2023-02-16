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
    public class QuizsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuizsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Quizs
        public async Task<IActionResult> Index()
        {
            var quizzList = await _context.Quizzes.Include(q => q.Level).Include(q => q.Technology).ToListAsync();
        
            var listQuizzViewModel = new List<QuizzViewModel>();

            foreach (Quiz quizz in quizzList)
            {
                listQuizzViewModel.Add((CastToQuizzViewModel(quizz)));
            }

            return View(listQuizzViewModel);
        }

        // GET: Quizs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Quizzes == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes
                .Include(q => q.Level)
                .Include(q => q.Technology)
                .FirstOrDefaultAsync(m => m.QuizId == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // GET: Quizs/Create
        public IActionResult Create()
        {
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name");
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name");
            return View();
        }

        // POST: Quizs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuizzViewModel quizzViewModel)
        {

            var quizz = CastToQuiz(quizzViewModel);

            if (ModelState.IsValid)
            {
                _context.Add(quizz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name", quizz.LevelId);
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name", quizz.TechnologyId);
            return View(quizz);
        }

        // GET: Quizs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
     
            var quizzById = await _context.Quizzes.FindAsync(id);
            if (quizzById == null)
            {
                return NotFound();
            }

            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name", quizzById.LevelId);
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name", quizzById.TechnologyId);
            return View(CastToQuizzViewModel(quizzById));
        }

        // POST: Quizs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, QuizzViewModel quizzViewModel)
        {
            var quizzById = CastToQuiz(quizzViewModel);

            if (id != quizzViewModel.QuizId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quizzById);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quizzViewModel.QuizId))
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
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name", quizzViewModel.LevelId);
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name", quizzViewModel.TechnologyId);
            return View(quizzViewModel);
        }

        // GET: Quizs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var quizzById = await _context.Quizzes
                .Include(q => q.Level)
                .Include(q => q.Technology)
                .FirstOrDefaultAsync(m => m.QuizId == id);
            if (quizzById == null)
            {
                return NotFound();
            }

            return View(CastToQuizzViewModel(quizzById));
        }

        // POST: Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Quizzes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Quizzes'  is null.");
            }

            var quizzById = await _context.Quizzes.FindAsync(id);

            if (quizzById != null)
            {
                _context.Quizzes.Remove(quizzById);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizExists(int id)
        {
            return (_context.Quizzes?.Any(e => e.QuizId == id)).GetValueOrDefault();
        }

        public Quiz CastToQuiz(QuizzViewModel quizzViewModel)
        {
            var quizz = new Quiz
            {
                Name = quizzViewModel.Name,
                QuizId = quizzViewModel.QuizId,
                Technology = quizzViewModel.Technology,
                TechnologyId = quizzViewModel.TechnologyId,
                LevelId = quizzViewModel.LevelId,
                Level = quizzViewModel.Level,
            };
            return quizz;

        }

        public QuizzViewModel CastToQuizzViewModel(Quiz quiz)
        {
            var quizzViewModel = new QuizzViewModel
            {
                Name = quiz.Name,
                QuizId = quiz.QuizId,
                Technology = quiz.Technology,
                TechnologyId = quiz.TechnologyId,
                LevelId = quiz.LevelId,
                Level = quiz.Level,
            };
            return quizzViewModel;

        }
    }
}
