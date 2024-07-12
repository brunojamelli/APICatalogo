using APICatalogo.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class UsuarioDTO
{
    // [JsonIgnore]
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