using AppProjetFilRouge.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppProjetFilRouge.Models
{
    public class LevelViewModel
    {
        [Key]
        [Column("level_id")]
        public int LevelId { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(200)")]
        public string? Name { get; set; }

        public virtual ICollection<Quiz>? Quizzes { get; set; }

        public virtual ICollection<Question>? Questions { get; set; }
    }
}
