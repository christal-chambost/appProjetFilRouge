using AppProjetFilRouge.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppProjetFilRouge.Data.Entities
{
	[Table("userAnswer")]
	public class UserAnswer
	{
		[Key]
		[Column("userAnswer_id")]
		public int UserAnswerId { get; set; }
        //public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; } = null!;

        //public virtual ICollection<Question> Questions { get; set; } = null!;

        [ForeignKey(nameof(QuestionId))]
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        //public ApplicationUser ApplicationUser { get; set; }
    }
}
