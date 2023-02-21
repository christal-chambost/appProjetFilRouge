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

        public string? Name { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        public bool? IsCorrect { get; set; }

        public int quizId { get; set; }

        public Quiz Quiz { get; set; }  

        public ApplicationUser ApplicationUser { get; set; }
    }
}
