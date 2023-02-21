using AppProjetFilRouge.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AppProjetFilRouge.Models
{
    public class QuizzViewModel
    {
        [Key]
        [Column("quiz_id")]
        public int QuizId { get; set; }

        [Required]
        [DisplayName("Nom du Quiz")]
        [Column("name", TypeName = "varchar(500)")]
        public string? Name { get; set; }

        [Required]
        [DisplayName("Technologie")]
        [Column("name", TypeName = "varchar(500)")]
        [ForeignKey(nameof(TechnologyId))]
        public int TechnologyId { get; set; }


        public Technology? Technology { get; set; } 

        [ForeignKey(nameof(LevelId))]

        [Required]
        [DisplayName("Niveaux")]
        [Column("name", TypeName = "varchar(500)")]
        public int LevelId { get; set; }

        public Level? Level { get; set; }

        [Required]
        [DisplayName("Nombre de questions")]
        public int NbQuestions { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? DateCreation { get; set; }

        public ICollection<Question>? Questions { get; set; }

        public int ResultQuiz { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

    }
}
