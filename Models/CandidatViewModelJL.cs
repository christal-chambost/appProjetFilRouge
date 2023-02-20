using AppProjetFilRouge.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace AppProjetFilRouge.Models
{
    public class CandidatViewModelJL
    {

        [Key]
        [Column("Candidat_id")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Non d'utilisateur")]
        //[Column("name", TypeName = "varchar(256)")]
        public string? UserName { get; set; }

        [Required]
        [DisplayName("Normalisation Nom d'utilisateur")]
        //[Column("name", TypeName = "varchar(256)")]
        public string? NormalizedUserName { get; set; }

        [Required]
        [DisplayName("Email")]
        //[Column("name", TypeName = "varchar(256)")]
        public string? Email { get; set; }

        [Required]
        [DisplayName("Normalisation Email")]
        //[Column("name", TypeName = "varchar(256)")]
        public string? NormalizedEmail { get; set; }

        [Required]
        [DisplayName("Email Confirmé")]
        public bool? EmailConfirmed { get; set; }

        [Required]
        [DisplayName("Password Hash")]
        //[Column("name", TypeName = "varchar(256)")]
        public string? PasswordHash { get; set; }

        [Required]
        [DisplayName("Security Stamp")]
        //[Column("name", TypeName = "varchar(256)")]
        public string? SecurityStamp { get; set; }

        [Required]
        [DisplayName("Concurrency Stamp")]
        //[Column("name", TypeName = "varchar(256)")]
        public string? ConcurrencyStamp { get; set; }

        [Required]
        [DisplayName("Numero de téléphone")]
        //[Column("name", TypeName = "varchar(256)")]
        public string? PhoneNumber { get; set; }

        [Required]
        [DisplayName("Confirmation du numero de téléphone")]
        public bool? PhoneNumberConfirmed { get; set; }

        [Required]
        [DisplayName("TwoFactorEnabled")]
        public bool? TwoFactorEnabled { get; set; }

        [Required]
        [DisplayName("LockoutEnd")]
        public DateTime? BLockoutEnd { get; set; }

        [Required]
        [DisplayName("LockoutEnabled")]
        public bool? LockoutEnabled { get; set; }

        [Required]
        [DisplayName("AccessFailedCount")]
        public int? AccessFailedCount { get; set; }

        [Required]
        [DisplayName("Date de naissance")]
        public DateTime? ABirthDate { get; set; }

        [Required]
        [DisplayName("Prénom")]
        //[Column("name", TypeName = "varchar(75)")]
        public string? FirstName { get; set; }

        [Required]
        [DisplayName("HandleBy")]
        //[Column("name", TypeName = "varchar(450)")]
        public string? HandleBy { get; set; }

        [Required]
        [DisplayName("Nom de Famille")]
        //[Column("name", TypeName = "varchar(75)")]
        public string? LastName { get; set; }

        [Required]
        [DisplayName("Discriminator")]
        //[Column("name", TypeName = "varchar(256)")]
        public string? Discriminator { get; set; }

    }

}




