using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DestinyAPI.Models
{
    [Table("Factions")]
    public class Faction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column(name: "Name", TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Column(name: "Description", TypeName = "varchar(256)")]
        [MaxLength(256)]
        public string? Description { get; set; }
    }
}
