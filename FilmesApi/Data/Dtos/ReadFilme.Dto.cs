﻿
namespace FilmesApi.Data.Dtos
{
    public class ReadFilmeDto
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public int Duracao { get; set; }
        public string Diretor { get; set; }
        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
        public ICollection<ReadSessaoDto> Sessoes { get; set; }


    }
}
