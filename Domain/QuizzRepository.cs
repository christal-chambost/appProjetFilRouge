using AppProjetFilRouge.Models;

namespace AppProjetFilRouge.Domain
{
	public class QuizzRepository : IQuizzRepository
	{
		public PassageViewModel GetPassageData(int quizzId, int? questionId)
		{
			switch (questionId)
			{
				case null:
				case 1:
					return new PassageViewModel
					{
						QuestionId = 1,
						QuizzId = quizzId,
						NextQuestionId = 2,
						TotalQuestions = 3,
						NumeroCourant = 1,
						Reponses = new List<ReponseViewModel>
						{
							new ReponseViewModel { Id = 67, IsCheched = false, Content = "Question 1 Choix 1"},
							new ReponseViewModel { Id = 68, IsCheched = false, Content = "Question 1 Choix 2"},
							new ReponseViewModel { Id = 69, IsCheched = false, Content = "Question 1 Choix 3"},
							new ReponseViewModel { Id = 70, IsCheched = false, Content = "Question 1 Choix 4"},
						}
					};
				case 2:
					return new PassageViewModel
					{
						QuestionId = 2,
						NextQuestionId = 3,
						QuizzId = quizzId,
						TotalQuestions = 3,
						NumeroCourant = 2,
						Reponses = new List<ReponseViewModel>
						{
							new ReponseViewModel { Id = 80, IsCheched = false, Content = "Question 2 Choix 1"},
							new ReponseViewModel { Id = 81, IsCheched = false, Content = "Question 2 Choix 2"},
							new ReponseViewModel { Id = 82, IsCheched = false, Content = "Question 2 Choix 3"},
							new ReponseViewModel { Id = 83, IsCheched = false, Content = "Question 2 Choix 4"},
						}
					};
				case 3:
					return new PassageViewModel
					{
						QuestionId = 3,
						QuizzId = quizzId,
						NextQuestionId = null,
						TotalQuestions = 3,
						NumeroCourant = 3,
						Reponses = new List<ReponseViewModel>
						{
							new ReponseViewModel { Id = 90, IsCheched = false, Content = "Question 3 Choix 1"},
							new ReponseViewModel { Id = 91, IsCheched = false, Content = "Question 3 Choix 2"},
							new ReponseViewModel { Id = 92, IsCheched = false, Content = "Question 3 Choix 3"},
							new ReponseViewModel { Id = 93, IsCheched = false, Content = "Question 3 Choix 4"},
						}
					};
				default:
					throw new IndexOutOfRangeException();
			}
		}
	}
}
