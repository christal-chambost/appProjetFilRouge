using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Entities
{
    [Table("levels")]
    public class Level
    {
        [Key]
        [Column("level_id")]
        public int LevelId { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(200)")]
        public string Name { get; set; } = null!;
    }
}
