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
        [Route("/Passage/{quizId}/{questionId}/{questionCourante}")]
        public async Task<IActionResult> Index(int quizId, int? questionId, int questionCourante = 0)
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
        [Route("/Passage/{quizId}/{questionId}/{questionCourante}")]
        public async Task<IActionResult> Index(int quizId, int questionId, PassageQuizViewModel passageQuizViewModel, int questionCourante = 0)
        {

            // Récupere le quizz actuel
            var quizz = await _context.Quizzes
                .Where(quizz => quizz.QuizId == quizId)
                .Include(q => q.Questions)
                .FirstOrDefaultAsync();

            // créer une liste d'id des questions
            var questionIdList = new List<int>();

            foreach (var item in quizz.Questions)
            {
                questionIdList.Add(item.Questionid);
            }

            
            if (questionCourante < questionIdList.Count - 1)
            {
                var quizQuestionAnswer = passageQuizViewModel;

                // Cherche dans la liste de réponse venant de la vue laquelle est cochée
                var isChecked = quizQuestionAnswer.Answers
                        .Where(a => a.IsChecked)
                        .Select(a => a.QuestionAnswerId)
                        .ToList();

                // Aller récupérer la bonne réponse en base. (prendre les quatres réponses possible) 
                var correctAnswer = _context.QuestionAnswers
                        .Where(q => q.QuestionId == questionId && q.IsCorrect == true)
                        .Select(q => q.QuestionAnswerId)
                        .ToList();

                // Comparer l'id des deux (la réponse cochée avec la réponse récupérée)
                var isCorrect = isChecked.SequenceEqual(correctAnswer);

                //Récupérer l'ID du user
                var quiz = _context.Quizzes
                    .Include(q => q.ApplicationUser)
                    .FirstOrDefault(q => q.QuizId == quizId);

                var userId = quiz.ApplicationUser.Id;

                //Récupérer toutes les questions rattachées au quiz
               // var questionsQuiz = _context.Questions.Where(q => q.QuizId == quizId);

                // Incrémenter Id de la question actuelle et rappeller la vue Index (get) en lui transmettant le numero de question

                // créer un objet userAnswer (rajouter une colonne isCorrect dans ton entité)
                var userAnswer = new UserAnswer
                {
                    QuestionId = questionId,
                    IsCorrect = isCorrect,
                    quizId = quizId,
                    Quiz = quiz,
                    ApplicationUser = quiz.ApplicationUser,
                };

                //_context.Add(userAnswer);
                await _context.UserAnswers.AddAsync(userAnswer);
                _context.SaveChangesAsync();

                questionCourante++;

                return RedirectToAction("Index", new { quizId = quizId, questionId = questionIdList[questionCourante], questionCourante = questionCourante });
            } 
            else
            {

                var resultQuiz = await CalculResultQuiz(quizId);

                var columnResultQuiz = await _context.Quizzes.Where(q => q.QuizId == quizId).FirstOrDefaultAsync();

                columnResultQuiz.ResultQuiz = resultQuiz;
                await _context.SaveChangesAsync();

                return View("EndQuiz");
            };

        }


        // Fonction pour calculer le résultat du quiz
        private async Task<int> CalculResultQuiz(int quizId)
        {
            int score = 0;

            var result = await _context.UserAnswers.Where(q => q.quizId == quizId).ToListAsync();


            foreach (var item in result)
            {
                if (item.IsCorrect == true)
                {
                    score++;
                }
            };

            return score;
        }

        public PassageQuizViewModel CastToQuizzViewModel(Quiz quiz)
        {
            var questionViewModel = new QuestionViewModel
            {
                Questionid = quiz.Questions.First().Questionid,
                Name = quiz.Questions.First().Name,
                QuestionAnswers= quiz.Questions.First().QuestionAnswers.ToList(),
            };

            var quizViewModel = new QuizzViewModel
            {
                Name = quiz.Name,
                QuizId = quiz.QuizId,
                Technology = quiz.Technology,
                TechnologyId = quiz.TechnologyId,
                LevelId = quiz.LevelId,
                Level = quiz.Level,
                NbQuestions = quiz.NbQuestions,
                ApplicationUser = quiz.ApplicationUser,
                ResultQuiz = quiz.ResultQuiz,
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
                    IsChecked = false,
                };

                answerViewModelList.Add(questionAnswerViewModel);
            }

            var passageQuizViewModel = new PassageQuizViewModel
            {
                Quizz = quizViewModel,
                Answers = answerViewModelList,
                NumeroCourant = 0,
                Question = questionViewModel,
                NextQuestionId = 0
            };

            return passageQuizViewModel;
        }
    }
}
