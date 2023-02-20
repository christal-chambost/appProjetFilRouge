namespace AppProjetFilRouge.Models
{
	public class PassageViewModel
	{
		public int QuizzId { get; set; }
		public string QuizzName { get; set; }
		public int QuestionId { get; set; }
		public int? NextQuestionId { get; set; }
		public int NumeroCourant { get; set; }
		public int TotalQuestions { get; set; }
		public List<ReponseViewModel> Reponses { get; set; }
	}
}