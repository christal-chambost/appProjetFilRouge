﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Entities
{
    [Table("questionAnswers")]
    public class QuestionAnswer
    {
        [Key]
        [Column("questionanswer_id")]
        public int QuestionAnswerId { get; set; }

        [Required]
        [Column("text", TypeName = "varchar(5000)")]
        public string Name { get; set; } = null!;

        public bool IsCorrect { get; set; }
    }
}
