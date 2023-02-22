using AppProjetFilRouge.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AppProjetFilRouge.Models
{
    public class QuestionViewModel
    {
        [Key]
        [Column("question_id")]
        public int Questionid { get; set; }

        [Required]
        [DisplayName("Intitulé de la question")]
        [Column("name", TypeName = "varchar(1000)")]
        public string? Name { get; set; }

        //public int QuestionAnswerId { get; set; }
        //public QuestionAnswer QuestionAnswer { get; set; } = null!;

        [ForeignKey(nameof(LevelId))]
        [DisplayName("Niveau")]

        public int LevelId { get; set; }

        public Level? Level { get; set; }

        [ForeignKey(nameof(TechnologyId))]
        [DisplayName("Technologie")]

        public int TechnologyId { get; set; }

        public Technology? Technology { get; set; }


        [ForeignKey(nameof(QuizId))]
        [DisplayName("Rattaché au quiz")]

        public int? QuizId { get; set; }
        public Quiz? Quiz { get; set; }


        [ForeignKey(nameof(QuestionTypeId))]
        public int QuestionTypeId { get; set; }
        public QuestionType? QuestionType { get; set; }

        public string? CommentUser { get; set; } 

        public string? Correction { get; set; }

        public List<QuestionAnswer>? QuestionAnswers { get; set; }

    }
}
