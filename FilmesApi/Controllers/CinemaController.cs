using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Controllers;

    [ApiController]
    [Route("[controller]")]

    public class CinemaController: ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;   

        public CinemaController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaCinemaPorId), new { id = cinema.Id }, cinema);
        }

        [HttpGet]
        public IEnumerable<ReadCinemaDto> BuscarCinemas([FromQuery] int? enderecoId = null)
        {

            if (enderecoId == null) { 
                return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList()); 
            }
                return _mapper.Map<List<ReadCinemaDto>>(
                _context.Cinemas.FromSqlRaw($"SELECT Id, Nome, EnderecoId FROM cinemas WHERE cinemas.EnderecoId = {enderecoId}").ToList());

            
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaCinemaPorId(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if(cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return Ok(cinemaDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCinemaCompleto(int id, [FromBody] AtualizarCinemaDto cinemaDto)
        {
            var cinema = _context.Cinemas.FirstOrDefault(
                cinema => cinema.Id == id);
            if (cinema == null) return NotFound();
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarCinemaParcial(int id, JsonPatchDocument<AtualizarCinemaDto> patch)
        {
            var cinema = _context.Cinemas.FirstOrDefault(
                 cinema => cinema.Id == id);
            if (cinema == null) return NotFound();

            var cinemaParaAtualizar = _mapper.Map<AtualizarCinemaDto>(cinema);

            patch.ApplyTo(cinemaParaAtualizar, ModelState);

            if (!TryValidateModel(cinemaParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(cinemaParaAtualizar, cinema);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCinema(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(
                 cinema => cinema.Id == id);
            if (cinema == null) return NotFound();
            _context.Remove(cinema);
            _context.SaveChanges();
            return NoContent();


        }

    }

