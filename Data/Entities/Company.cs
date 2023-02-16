using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppProjetFilRouge.Data.Entities
{
    [Table("companies")]
    public class Company
    {
        [Key]
        [Column("company_id")]
        public int CompanyId { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(500)]
        public string Name { get; set; } = null!;

    }
}
