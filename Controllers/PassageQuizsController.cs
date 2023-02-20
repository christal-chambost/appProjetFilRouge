using AppProjetFilRouge.Data;
using AppProjetFilRouge.Data.Entities;
using AppProjetFilRouge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppProjetFilRouge.Controllers
{
    public class PassageQuizsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public PassageQuizsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/Passage/{quizId}/{questionId?}")]
        public async Task<IActionResult> Index(int quizId, int? questionId)
        {

            //var data = repository.GetPassageData(id, questionId);

            var quizById = await _context.Quizzes
                .Include(q => q.Level)
                .Include(q => q.Technology)
                .Include(q => q.Questions.Where(q => q.Questionid == questionId))
                    .ThenInclude(q => q.QuestionAnswers)
                .FirstOrDefaultAsync(q => q.QuizId == quizId!);


            if (quizId == null || questionId == null)
            {
                return NotFound();
            }

            //return View(data);
            return View(CastToQuizzViewModel(quizById));
        }

        [HttpPost]
        [Route("/Passage/{quizId}/{questionId}")]
        public IActionResult Index(int quizId, int questionId/*, IFormCollection input*/)
        {
            //var data = repository.GetPassageData(id, questionId);
            //Pour chaque réponse on va regarde dans la form collection si c'est check ou pas.
            //var responseIds = data.Reponses.Where(reponseId => input.ContainsKey(reponseId.Id.ToString())).Select(a => a.Id);

            // TODO Sauvegarde en base
            // myRepo.SaveAnswers(id, questionId, reponsesChoisies);

            // Redirect vers la questions suivante :
            //if (data.NextQuestionId == null)
            //{
            //    return Content("Résultats");
            //}

            //return RedirectToAction("Index", new { id, questionId = data.NextQuestionId });
            return View();
        }


        public PassageQuizViewModel CastToQuizzViewModel(Quiz quiz)
        {
           /* var listQuizzViewModel = new List<QuestionViewModel>();

            var questionViewModel = new QuestionViewModel
            {
                Name = quiz.Questions.FirstOrDefault().Name,
                Questionid = quiz.Questions.FirstOrDefault().Questionid,


            };

            listQuizzViewModel.Add(questionViewModel);*/


            var quizViewModel = new QuizzViewModel
            {
                Name = quiz.Name,
                QuizId = quiz.QuizId,
                Technology = quiz.Technology,
                TechnologyId = quiz.TechnologyId,
                LevelId = quiz.LevelId,
                Level = quiz.Level,
                Questions = quiz.Questions,
                NbQuestions = quiz.NbQuestions,
            };

            List<QuestionAnswerViewModel> answerViewModelList= new List<QuestionAnswerViewModel>();

            foreach (var questionAnswer in quiz.Questions.First().QuestionAnswers)
            {
                

                var questionAnswerViewModel = new QuestionAnswerViewModel
                {
                    QuestionAnswerId = questionAnswer.QuestionAnswerId,
                    Name = questionAnswer.Name,
                    QuestionId = questionAnswer.QuestionId,
                    IsCorrect = questionAnswer.IsCorrect,
                };

                answerViewModelList.Add(questionAnswerViewModel);
            }

            var passageQuizViewModel = new PassageQuizViewModel
            {
                Quizz = quizViewModel,
                Answers = answerViewModelList,
                NumeroCourant = 0,
            };

            return passageQuizViewModel;
        }
    }
}
