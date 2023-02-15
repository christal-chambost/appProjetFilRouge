using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Entities
{
    [Table("quizzes")]
    public class Quiz
    {
        [Key]
        [Column("quiz_id")]
        public int QuizId { get; set; }
    }
}
