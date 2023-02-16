using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Entities
{
    [Table("quizResult")]
    public class QuizResult
    {
        [Key]
        [Column("quizResult_id")]
        public int QuizResultId { get; set; }

        [Required]
        [Column("total")]
        public int Total { get; set; }
    }
}
