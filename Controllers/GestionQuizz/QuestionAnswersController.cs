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
    public class QuestionAnswersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionAnswersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QuestionAnswers
        public async Task<IActionResult> Index()
        {
            var questionAnswerList = await _context.QuestionAnswers.Include(q => q.Question).ToListAsync();
          
            var listQuestionAnswerViewModel = new List<QuestionAnswerViewModel>();

            foreach (QuestionAnswer questionAnswer in questionAnswerList)
            {
                listQuestionAnswerViewModel.Add(CastToQuestionAnswerViewModel(questionAnswer));
            }
            return View(listQuestionAnswerViewModel);
        }

        // GET: QuestionAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.QuestionAnswers == null)
            {
                return NotFound();
            }

            var questionAnswer = await _context.QuestionAnswers
                .FirstOrDefaultAsync(m => m.QuestionAnswerId == id);
            if (questionAnswer == null)
            {
                return NotFound();
            }

            return View(questionAnswer);
        }

        // GET: QuestionAnswers/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Questionid", "Name");
            return View();
        }

        // POST: QuestionAnswers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionAnswerViewModel questionAnswerViewModel)
        {
            //var checkboxes = new List<QuestionAnswerViewModel>();

            var questionAnswer = CastToQuestionAnswer(questionAnswerViewModel);

            if (ModelState.IsValid)
            {
                _context.Add(questionAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Questionid", "Name", questionAnswer.QuestionId);
            return View(questionAnswer);
        }

        // GET: QuestionAnswers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var questionAnswerById = await _context.QuestionAnswers.FindAsync(id);
            if (questionAnswerById == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Questionid", "Name", questionAnswerById.QuestionId);
            return View(CastToQuestionAnswerViewModel(questionAnswerById));
        }

        // POST: QuestionAnswers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, QuestionAnswerViewModel questionAnswerViewModel)
        {

            var questionAnswerById = CastToQuestionAnswer(questionAnswerViewModel);

            if (id != questionAnswerViewModel.QuestionAnswerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionAnswerById);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionAnswerExists(questionAnswerViewModel.QuestionAnswerId))
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
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Questionid", "Name", questionAnswerById.QuestionId);
            return View(questionAnswerViewModel);
        }

        // GET: QuestionAnswers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var questionAnswerById = await _context.QuestionAnswers
                .FirstOrDefaultAsync(m => m.QuestionAnswerId == id);
            if (questionAnswerById == null)
            {
                return NotFound();
            }

            return View(CastToQuestionAnswerViewModel(questionAnswerById));
        }

        // POST: QuestionAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.QuestionAnswers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.QuestionAnswers'  is null.");
            }
            var questionAnswer = await _context.QuestionAnswers.FindAsync(id);
            if (questionAnswer != null)
            {
                _context.QuestionAnswers.Remove(questionAnswer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionAnswerExists(int id)
        {
            return (_context.QuestionAnswers?.Any(e => e.QuestionAnswerId == id)).GetValueOrDefault();
        }

        public QuestionAnswer CastToQuestionAnswer(QuestionAnswerViewModel questionAnswerViewModel)
        {
            var questionAnswer = new QuestionAnswer
            {
                QuestionAnswerId = questionAnswerViewModel.QuestionAnswerId,
                Name = questionAnswerViewModel.Name,
                IsCorrect = questionAnswerViewModel.IsCorrect,
                QuestionId = questionAnswerViewModel.QuestionId,
                Question = questionAnswerViewModel.Question,
            };
            return questionAnswer;

        }

        public QuestionAnswerViewModel CastToQuestionAnswerViewModel(QuestionAnswer questionAnswer)
        {
            var questionAnswerViewModel = new QuestionAnswerViewModel
            {
                QuestionAnswerId = questionAnswer.QuestionAnswerId,
                Name = questionAnswer.Name,
                IsCorrect = questionAnswer.IsCorrect,
                QuestionId = questionAnswer.QuestionId,
                Question = questionAnswer.Question,
            };
            return questionAnswerViewModel;

        }
    }
}
