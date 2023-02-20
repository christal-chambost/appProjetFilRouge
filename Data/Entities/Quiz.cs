using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppProjetFilRouge.Data.Entities
{
	[Table("quizzes")]
	public class Quiz
	{
		[Key]
		[Column("quiz_id")]
		public int QuizId { get; set; }

		[Required]
		[Column("name", TypeName = "varchar(500)")]
		public string Name { get; set; } = null!;

		[ForeignKey(nameof(TechnologyId))]
		public int TechnologyId { get; set; }

		public Technology Technology { get; set; } = null!;

		[ForeignKey(nameof(LevelId))]
		public int LevelId { get; set; }

		public Level Level { get; set; } = null!;

		public int NbQuestions { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? DateCreation { get; set; }

        public virtual ICollection<Question>? Questions { get; set; }
        //public ApplicationUser ApplicationUserId { get; set; }
    }
}
