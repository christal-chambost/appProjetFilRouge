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
    }
}
