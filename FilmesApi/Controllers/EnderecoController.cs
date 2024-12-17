using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {

        private FilmeContext _context;
        private IMapper _mapper;

        public EnderecoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaEnderecoPorId), new { id = endereco.Id }, endereco);
        }

        [HttpGet]
        public IEnumerable<ReadEnderecoDto> BuscarEnderecos([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos.Skip(skip).Take(take));
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaEnderecoPorId(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

            if (endereco != null)
            {
                ReadCinemaDto enderecoDto = _mapper.Map<ReadCinemaDto>(endereco);
                return Ok(enderecoDto);
            }
            return NotFound();
        }


        [HttpPut("{id}")]
        public IActionResult AtualizaEnderecoCompleto(int id, [FromBody] AtualizarEnderecoDto enderecoDto)
        {
            var endereco = _context.Enderecos.FirstOrDefault(
                endereco => endereco.Id == id);
            if (endereco == null) return NotFound();
            _mapper.Map(enderecoDto, endereco);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarEnderecoParcial(int id, JsonPatchDocument<AtualizarEnderecoDto> patch)
        {
            var endereco = _context.Enderecos.FirstOrDefault(
                 endereco => endereco.Id == id);
            if (endereco == null) return NotFound();

            var enderecoParaAtualizar = _mapper.Map<AtualizarEnderecoDto>(endereco);

            patch.ApplyTo(enderecoParaAtualizar, ModelState);

            if (!TryValidateModel(enderecoParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(enderecoParaAtualizar, endereco);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCinema(int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(
                 endereco => endereco.Id == id);
            if (endereco == null) return NotFound();
            _context.Remove(endereco);
            _context.SaveChanges();
            return NoContent();


        }

    }






