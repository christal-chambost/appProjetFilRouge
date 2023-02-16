using AppProjetFilRouge.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppProjetFilRouge.Models
{
    public class QuizzViewModel
    {
        [Key]
        [Column("quiz_id")]
        public int QuizId { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(500)")]
        public string? Name { get; set; } 

        [ForeignKey(nameof(TechnologyId))]
        public int TechnologyId { get; set; }

        public Technology? Technology { get; set; } 

        [ForeignKey(nameof(LevelId))]
        public int LevelId { get; set; }

        public Level? Level { get; set; } 
    }
}
