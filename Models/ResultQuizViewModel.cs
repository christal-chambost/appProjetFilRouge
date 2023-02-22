using AppProjetFilRouge.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AppProjetFilRouge.Models
{
    public class ResultQuizViewModel
    {
        public int UserAnswerId { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public bool? IsCorrect { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Quiz Quiz { get; set; }
    }
}
