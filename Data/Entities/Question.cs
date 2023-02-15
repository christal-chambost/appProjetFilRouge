using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Entities
{
	[Table("questions")]
	public class Question
	{
		[Key]
		[Column("question_id")]
		public int Questionid { get; set; }

		[Required]
		[Column("name", TypeName = "varchar(1000)")]
		public string Name { get; set; } = null!;

		public int QuestionAnswerId { get; set; }
		public QuestionAnswer QuestionAnswer { get; set; } = null!;

		[ForeignKey(nameof(LevelId))]
		public int LevelId { get; set; }

		public Level Level { get; set; } = null!;

		[ForeignKey(nameof(TechnologyId))]
		public int TechnologyId { get; set; }

		public Technology Technology { get; set; } = null!;
	}
}
