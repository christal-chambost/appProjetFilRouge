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
    public class QuestionTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QuestionTypes
        public async Task<IActionResult> Index()
        {
            var questionTypeList = await _context.QuestionTypes.ToListAsync();

            var listQuestionTypesViewModel = new List<QuestionTypeViewModel>();

            foreach (QuestionType questionType in questionTypeList)
            {
                listQuestionTypesViewModel.Add(CastToQuestionTypeViewModel(questionType));
            }
            
            return View(listQuestionTypesViewModel);
        }

        // GET: QuestionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.QuestionTypes == null)
            {
                return NotFound();
            }

            var questionType = await _context.QuestionTypes
                .FirstOrDefaultAsync(m => m.QuestionTypeId == id);
            if (questionType == null)
            {
                return NotFound();
            }

            return View(questionType);
        }

        // GET: QuestionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuestionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionTypeViewModel questionTypeViewModel)
        {

            var questionType = CastToQuestionType(questionTypeViewModel);

            if (ModelState.IsValid)
            {
                _context.Add(questionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionType);
        }

        // GET: QuestionTypes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var questionTypeById = await _context.QuestionTypes.FindAsync(id);
            if (questionTypeById == null)
            {
                return NotFound();
            }
            return View(CastToQuestionTypeViewModel(questionTypeById));
        }

        // POST: QuestionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, QuestionTypeViewModel questionTypeViewModel)
        {
            var questionTypeById = CastToQuestionType(questionTypeViewModel);

            if (id != questionTypeViewModel.QuestionTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionTypeById);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionTypeExists(questionTypeViewModel.QuestionTypeId))
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
            return View(questionTypeViewModel);
        }

        // GET: QuestionTypes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var questionTypeById = await _context.QuestionTypes
                .FirstOrDefaultAsync(m => m.QuestionTypeId == id);
            if (questionTypeById == null)
            {
                return NotFound();
            }

            return View(CastToQuestionTypeViewModel(questionTypeById));
        }

        // POST: QuestionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (_context.QuestionTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.QuestionTypes'  is null.");
            }

            var questionTypeById = await _context.QuestionTypes.FindAsync(id);

            if (questionTypeById != null)
            {
                _context.QuestionTypes.Remove(questionTypeById);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionTypeExists(int id)
        {
          return (_context.QuestionTypes?.Any(e => e.QuestionTypeId == id)).GetValueOrDefault();
        }

        public QuestionType CastToQuestionType(QuestionTypeViewModel questionTypeViewModel)
        {
            var questionType = new QuestionType
            {
                Name = questionTypeViewModel.Name,
                QuestionTypeId = questionTypeViewModel.QuestionTypeId,
                Questions = questionTypeViewModel.Questions,

            };
            return questionType;

        }

        public QuestionTypeViewModel CastToQuestionTypeViewModel(QuestionType questionType)
        {
            var questionTypeViewModel = new QuestionTypeViewModel
            {
                Name = questionType.Name,
                QuestionTypeId = questionType.QuestionTypeId,
                Questions = questionType.Questions,

            };
            return questionTypeViewModel;

        }
    }
}
