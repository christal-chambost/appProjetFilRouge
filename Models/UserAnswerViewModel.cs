using AppProjetFilRouge.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppProjetFilRouge.Models
{
    public class UserAnswerViewModel
    {
        [Key]
        [Column("userAnswer_id")]
        public string UserAnswerId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public int QuestionId { get; set; }
        public Question? Question { get; set; }

        public bool IsCorrect { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
