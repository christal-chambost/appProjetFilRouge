using AppProjetFilRouge.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppProjetFilRouge.Models
{
    public class QuestionAnswerViewModel
    {
        [Key]
        [Column("questionanswer_id")]
        public int QuestionAnswerId { get; set; }

        [Required]
        [Column("text", TypeName = "varchar(5000)")]
        public string? Name { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }

}
