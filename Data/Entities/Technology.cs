using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Entities
{
	[Table("technologies")]
	public class Technology
	{
		[Key]
		[Column("technology_id")]
		public int TechnologyId { get; set; }

		[Required]
		[Column("name", TypeName = "varchar(200)")]
		public string Name { get; set; } = null!;

		public virtual ICollection<Quiz> Quizzes { get; set; } = null!;

		public virtual ICollection<Question> Questions { get; set; } = null!;
	}
}
