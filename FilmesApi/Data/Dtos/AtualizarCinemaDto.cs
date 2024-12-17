﻿using System.ComponentModel.DataAnnotations;


namespace FilmesApi.Data.Dtos
{
    public class AtualizarCinemaDto
    {
        [Required(ErrorMessage = "O nome do cinema é obrigatório.")]
        public string Nome { get; set; }
    }
}
