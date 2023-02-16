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
            var questionsList = await _context.Questions.Include(q => q.Level).Include(q => q.QuestionType).Include(q => q.Quiz).Include(q => q.Technology).ToListAsync();

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
                //.Include(q => q.QuestionAnswer)
                .Include(q => q.QuestionType)
                .Include(q => q.Quiz)
                .Include(q => q.Technology)
                //.Include(q => q.UserAnswer)
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name", questionViewModel.LevelId);
            //ViewData["QuestionAnswerId"] = new SelectList(_context.QuestionAnswers, "QuestionAnswerId", "Name", question.QuestionAnswerId);
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "QuestionTypeId", "Name", questionViewModel.QuestionTypeId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "QuizId", "Name", questionViewModel.QuizId);
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name", questionViewModel.TechnologyId);
            //ViewData["UserAnswerId"] = new SelectList(_context.UserAnswers, "UserAnswerId", "UserAnswerId", question.UserAnswerId);
            return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name", question.LevelId);
            //ViewData["QuestionAnswerId"] = new SelectList(_context.QuestionAnswers, "QuestionAnswerId", "Name", question.QuestionAnswerId);
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "QuestionTypeId", "Name", question.QuestionTypeId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "QuizId", "Name", question.QuizId);
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name", question.TechnologyId);
            //ViewData["UserAnswerId"] = new SelectList(_context.UserAnswers, "UserAnswerId", "UserAnswerId", question.UserAnswerId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Questionid,Name,QuestionAnswerId,LevelId,TechnologyId,QuizId,QuestionTypeId,UserAnswerId")] Question question)
        {
            if (id != question.Questionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Questionid))
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
            ViewData["LevelId"] = new SelectList(_context.Levels, "LevelId", "Name", question.LevelId);
            //ViewData["QuestionAnswerId"] = new SelectList(_context.QuestionAnswers, "QuestionAnswerId", "Name", question.QuestionAnswerId);
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "QuestionTypeId", "Name", question.QuestionTypeId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "QuizId", "Name", question.QuizId);
            ViewData["TechnologyId"] = new SelectList(_context.Technologies, "TechnologyId", "Name", question.TechnologyId);
            //ViewData["UserAnswerId"] = new SelectList(_context.UserAnswers, "UserAnswerId", "UserAnswerId", question.UserAnswerId);
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Level)
                //.Include(q => q.QuestionAnswer)
                .Include(q => q.QuestionType)
                .Include(q => q.Quiz)
                .Include(q => q.Technology)
                //.Include(q => q.UserAnswer)
                .FirstOrDefaultAsync(m => m.Questionid == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
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
            var question = await _context.Questions.FindAsync(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
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
                LevelId = questionViewModel.LevelId,
                QuestionTypeId = questionViewModel.QuestionTypeId,
                QuizId = questionViewModel.QuizId,
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
                LevelId = question.LevelId,
                QuestionTypeId = question.QuestionTypeId,
                QuizId = question.QuizId,

            };
            return questionViewModel;

        }
    }
}
