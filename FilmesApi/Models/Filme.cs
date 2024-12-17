using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

public class Filme

{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O título do filme é obrigatório")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O Gênero do filme é obrigatório")]
    [MaxLength(50, ErrorMessage = "O gênero não pode conter mais de 50 caracteres")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "A Duração do filme é obrigatória")]
    [Range(10, 500, ErrorMessage ="O filme deve ter entre 10 e 500 minutos")]
    public int Duracao { get; set; }

    [Required(ErrorMessage = "O Diretor do filme é obrigatório")]
    public string Diretor { get; set; }
    public virtual ICollection<Sessao> Sessoes { get; set; }
}
