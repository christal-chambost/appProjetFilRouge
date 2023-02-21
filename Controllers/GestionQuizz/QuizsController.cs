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
using NuGet.Packaging.Signing;
using System.ComponentModel;
using System.Reflection;
using System.Security.AccessControl;
using X.PagedList;

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
            var quizzList = await _context.Quizzes.Include(q => q.Level).Include(q => q.Technology).Include(q => q.ApplicationUser).ToListAsync();

            var listQuizzViewModel = new List<QuizzViewModel>();

            foreach (Quiz quizz in quizzList)
            {
                listQuizzViewModel.Add((CastToQuizzViewModel(quizz)));
            }

            return View(listQuizzViewModel);
        }

        // GET: Quizs/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var quizById = await _context.Quizzes
                .Include(q => q.Level)
                .Include(q => q.Technology)
                .Include(q => q.Questions)
                .ThenInclude(q => q.QuestionAnswers)
            .FirstOrDefaultAsync(q => q.QuizId == id!);

            if (quizById == null)
            {
                return NotFound();
            }

            var quizByIdList = new List<QuizzViewModel>
            {
                CastToQuizzViewModel(quizById)
            };

            return View(quizByIdList);
        }

        // GET: Quizs/Create
        public IActionResult Create()
        {


            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name");
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name");
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "LastName");

            return View();
        }

        // POST: Quizs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuizzViewModel quizzViewModel)
        {
            var user = await _context.ApplicationUsers.Where(q => q.Id == quizzViewModel.ApplicationUser.Id).FirstOrDefaultAsync();

            quizzViewModel.ApplicationUser = user;

            var quizz = CastToQuiz(quizzViewModel);

        
                _context.Add(quizz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name", quizz.LevelId);
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name", quizz.TechnologyId);
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "LastName", quizz.ApplicationUser);

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


        // GET: Quizs/GenerateQuiz
        public IActionResult GenerateQuiz()
        {

            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name");
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name");
            return View();
        }

        // POST: Quizs/GenerateQuiz
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateQuiz(QuizzViewModel quizzViewModel)
        {
            //Je récupère le résultat du form et le transform en objet Quiz
            var quizz = CastToQuiz(quizzViewModel);

            if (ModelState.IsValid)
            {
                //J'ajoute le nouveau quiz dans la BDD
                _context.Add(quizz);
                await _context.SaveChangesAsync();

                //Je récupère toutes les questions qui n'ont pas encore été attribué à un Quiz en m'assurant 
                //de récupérer uniquement ceux avec la même techno et le même level. Et uniquement le nbQuestions indiqué dans le form.
                var getAllQuestions = _context.Questions
                .Where(q => q.QuizId == null && q.TechnologyId == quizzViewModel.TechnologyId && q.LevelId == quizzViewModel.LevelId)
                .Take(quizzViewModel.NbQuestions)
                .ToList();

                //Je rajoute une condition au cas où il n'y a pas assez de questions dans la BDD
                if (getAllQuestions.Count < quizzViewModel.NbQuestions)
                {
                    ModelState.AddModelError("NbQuestions", "Il n'y a pas suffisamment de questions dans la base de données pour créer ce quiz.");
                    ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name", quizzViewModel.LevelId);
                    ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name", quizzViewModel.TechnologyId);
                    return View("Create", quizzViewModel);
                }

                //Je lie les questions au quiz qui vient d'être créé
                foreach (var question in getAllQuestions)
                {
                    question.QuizId = quizz.QuizId;
                }

                //Je sauvegarde les modifications
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name", quizz.LevelId);
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name", quizz.TechnologyId);
            return View(quizz);
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
                Questions = quizzViewModel.Questions,
                NbQuestions = quizzViewModel.NbQuestions,
                DateCreation = DateTime.Now,
                ApplicationUser = quizzViewModel.ApplicationUser,
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
                Questions = quiz.Questions,
                NbQuestions = quiz.NbQuestions,
                DateCreation = DateTime.Now,
                ApplicationUser = quiz.ApplicationUser,
            };
            return quizzViewModel;
        }
    }
}
