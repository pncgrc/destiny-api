using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DestinyAPI.Models
{
    [Table("Weapons")]
    public class Weapon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column(name: "Name", TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(name: "FactionId")]
        public int FactionId { get; set; }

        [ForeignKey("FactionId")]
        public Faction? Faction { get; set; }

        [Column(name: "Damage", TypeName = "decimal(5,2)")]
        [Precision(5, 2)]
        [Range(0, 999.99)]
        public decimal Damage { get; set; }
    }
}
