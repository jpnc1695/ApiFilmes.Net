using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos
{
    public class AtualizarEnderecoDto
    {
        [Required(ErrorMessage = "O Logradouro é obrigatório.")]
        public string Logradouro { get; set; }
        public int Numero { get; set; }
    }
}
