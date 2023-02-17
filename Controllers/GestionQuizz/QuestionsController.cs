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
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var questionsList = await _context.Questions
                .Include(q => q.Level)
                .Include(q => q.QuestionType)
                .Include(q => q.Quiz)
                .Include(q => q.Technology)
                .ToListAsync();

            var listQuestionsViewModel = new List<QuestionViewModel>();

            foreach (Question question in questionsList)
            {
                listQuestionsViewModel.Add(CastToQuestionViewModel(question));
            }

            return View(listQuestionsViewModel);
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Level)
                .Include(q => q.QuestionType)
                .Include(q => q.Quiz)
                .Include(q => q.Technology)
                .FirstOrDefaultAsync(m => m.Questionid == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name");
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "QuestionTypeId", "Name");
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "QuizId", "Name");
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionViewModel questionViewModel)
        {

            var question = CastToQuestion(questionViewModel);

            if (ModelState.IsValid)
            {
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "QuestionAnswers", new { id = questionViewModel.QuestionTypeId });
            }
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name", questionViewModel.LevelId);
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "QuestionTypeId", "Name", questionViewModel.QuestionTypeId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "QuizId", "Name", questionViewModel.QuizId);
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name", questionViewModel.TechnologyId);
            return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var questionById = await _context.Questions.FindAsync(id);
            if (questionById == null)
            {
                return NotFound();
            }
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name", questionById.LevelId);
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "QuestionTypeId", "Name", questionById.QuestionTypeId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "QuizId", "Name", questionById.QuizId);
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name", questionById.TechnologyId);
            return View(CastToQuestionViewModel(questionById));
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, QuestionViewModel questionViewModel)
        {

            var questionById = CastToQuestion(questionViewModel);

            if (id != questionViewModel.Questionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionById);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(questionViewModel.Questionid))
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
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name", questionViewModel.LevelId);
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "QuestionTypeId", "Name", questionViewModel.QuestionTypeId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "QuizId", "Name", questionViewModel.QuizId);
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name", questionViewModel.TechnologyId);
            return View(questionViewModel);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var questionById = await _context.Questions
                .Include(q => q.Level)
                .Include(q => q.QuestionType)
                .Include(q => q.Quiz)
                .Include(q => q.Technology)
                .FirstOrDefaultAsync(m => m.Questionid == id);
            if (questionById == null)
            {
                return NotFound();
            }

            return View(CastToQuestionViewModel(questionById));
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Questions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Questions'  is null.");
            }

            var questionById = await _context.Questions.FindAsync(id);

            if (questionById != null)
            {
                _context.Questions.Remove(questionById);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
          return (_context.Questions?.Any(e => e.Questionid == id)).GetValueOrDefault();
        }

        public Question CastToQuestion(QuestionViewModel questionViewModel)
        {
            var question = new Question
            {
                Name = questionViewModel.Name,
                Questionid = questionViewModel.Questionid,
                TechnologyId = questionViewModel.TechnologyId,
                Technology = questionViewModel.Technology,
                LevelId = questionViewModel.LevelId,
                Level = questionViewModel.Level,
                QuestionTypeId = questionViewModel.QuestionTypeId,
                QuestionType = questionViewModel.QuestionType,
                QuizId = questionViewModel.QuizId,
                Quiz = questionViewModel.Quiz,
            };
            return question;

        }

        public QuestionViewModel CastToQuestionViewModel(Question question)
        {
            var questionViewModel = new QuestionViewModel
            {
                Name = question.Name,
                Questionid = question.Questionid,
                TechnologyId = question.TechnologyId,
                Technology = question.Technology,
                LevelId = question.LevelId,
                Level = question.Level,
                QuestionTypeId = question.QuestionTypeId,
                QuestionType= question.QuestionType,
                QuizId = question.QuizId,
                Quiz = question.Quiz,

            };
            return questionViewModel;

        }
    }
}
