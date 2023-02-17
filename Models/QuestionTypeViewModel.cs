using AppProjetFilRouge.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppProjetFilRouge.Models
{
    public class QuestionTypeViewModel
    {
        [Key]
        [Column("questiontype_id")]
        public int QuestionTypeId { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(500)")]
        public string? Name { get; set; }

        public virtual ICollection<Question>? Questions { get; set; }
    }
}
