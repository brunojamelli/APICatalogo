using System.ComponentModel.DataAnnotations;

public class ProdutoDTOUpdateRequest : IValidatableObject
{
    [Range(1,999,ErrorMessage = "O valor deve estar entre 1 e 999")]
    public float Estoque {get; set;}

    public DateTime DataCadastro {get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(DataCadastro.Date <= DateTime.Now.Date){
            yield return new ValidationResult("A Data não pode ser maior que a atual", 
            new [] {nameof(this.DataCadastro)} );    
        }
    }
}