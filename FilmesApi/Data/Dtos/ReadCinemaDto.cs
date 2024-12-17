namespace FilmesApi.Data.Dtos
{
    public class ReadCinemaDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
        public ReadEnderecoDto Endereco { get; set; }
        public ICollection<ReadSessaoDto> Sessoes { get; set; } 

    }
}
