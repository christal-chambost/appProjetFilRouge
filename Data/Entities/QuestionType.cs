using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Entities
{
	[Table("questionTypes")]
	public class QuestionType
	{
		[Key]
		[Column("questiontype_id")]
		public int QuestionTypeId { get; set; }

		[Required]
		[Column("name", TypeName = "varchar(500)")]
		public string Name { get; set; } = null!;

		public virtual ICollection<Question> Questions { get; set; } = null!;
	}
}
