using Api.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(150, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
        [DisplayName("Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "O campo {0} é inválido.")]
        [DataNascimento]
        [DisplayName("Data Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? CPF { get; set; }

        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }

    }
}
