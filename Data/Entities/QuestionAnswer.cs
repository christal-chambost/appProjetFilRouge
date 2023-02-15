using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Entities
{
	[Table("questionAnswers")]
	public class QuestionAnswer
	{
		[Key]
		[Column("questionanswer_id")]
		public int QuestionAnswerId { get; set; }

		[Required]
		[Column("text", TypeName = "varchar(5000)")]
		public string Name { get; set; } = null!;
		// column type bit pour simuler le boolean pour isCorrect
		public bool IsCorrect { get; set; }

		public virtual ICollection<Question> Questions { get; set; }
	}
}
