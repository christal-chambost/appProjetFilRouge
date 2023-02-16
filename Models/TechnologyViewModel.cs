using AppProjetFilRouge.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppProjetFilRouge.Models
{
    public class TechnologyViewModel
    {
        [Key]
        [Column("technology_id")]
        public int TechnologyId { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(200)")]
        public string? Name { get; set; } 

        public ICollection<Quiz>? Quizzes { get; set; } 

        public ICollection<Question>? Questions { get; set; } 
    }
}
