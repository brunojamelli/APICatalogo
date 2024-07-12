using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;
[Table("Usuario")]
public class Usuario
{   
    [Key]
    public int UsuarioID { get; set; }

    [Required]
    [StringLength(120)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(130)]
    public string? Email { get; set; }

    [Required]
    [StringLength(300)]
    public string? Senha { get; set; }
    
}