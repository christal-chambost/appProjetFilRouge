using AppProjetFilRouge.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace AppProjetFilRouge.Models
{
    public class AgentViewModel
    {
        [Key]
        [Column("Agent_id")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Nom d'utilisateur")]
        //[Column("name", TypeName = "varchar(256)")]
        public string? UserName { get; set; }

        [Required]
        [DisplayName("Email")]
        //[Column("name", TypeName = "varchar(256)")]
        public string? Email { get; set; }

        [Required]
        [DisplayName("Numero de téléphone")]
        //[Column("name", TypeName = "varchar(256)")]
        public string? PhoneNumber { get; set; }

        [Required]
        [DisplayName("Prénom")]
        //[Column("name", TypeName = "varchar(75)")]
        public string? FirstName { get; set; }

        [Required]
        [DisplayName("Nom de Famille")]
        //[Column("name", TypeName = "varchar(75)")]
        public string? LastName { get; set; }
    }
}
