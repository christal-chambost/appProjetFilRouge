namespace AppProjetFilRouge.Models
{
    public class PassageQuizViewModel
    {
        public QuizzViewModel Quizz { get; set; }

        public List<QuestionAnswerViewModel> Answers { get; set; }

        public int? NextQuestionId { get; set; }

        public int NumeroCourant { get; set; }
    }
}
