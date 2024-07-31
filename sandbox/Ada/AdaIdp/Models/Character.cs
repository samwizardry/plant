using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdaIdp.Models;

[Table("characters")]
public class Character
{
    [Column("id")]
    [Key]
    [Required]
    public int Id { get; set; }

    [Column("name")]
    [Required]
    public string Name { get; set; } = null!;
}
