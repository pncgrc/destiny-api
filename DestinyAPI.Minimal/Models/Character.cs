using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DestinyAPI.Models
{
    [Table("Characters")]
    public class Character
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column(name: "Name", TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(name: "WeaponId")]
        public int WeaponId { get; set; }

        [ForeignKey("WeaponId")]
        public Weapon? Weapon { get; set; }
    }
}
