namespace Business.Models
{
    public class Usuario
    {   
        public int Id { get; set; }

        public string? NomeCompleto;
        public bool Excluido { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string? CPF { get; set; }
    }
}
