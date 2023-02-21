namespace AppProjetFilRouge.Models
{
    public class PassageQuizViewModel
    {
        public QuizzViewModel? Quizz { get; set; }

        public List<QuestionAnswerViewModel> Answers { get; set; }

        public QuestionViewModel? Question { get; set; }

        public UserAnswerViewModel UserAnswer { get; set; }

        public int? NextQuestionId { get; set; }

        public int NumeroCourant { get; set; }
    }
}
