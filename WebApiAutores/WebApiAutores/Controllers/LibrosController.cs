using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DataBase;
using WebApiAutores.DTOs;
using WebApiAutores.Entities;

namespace WebApiAutores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDto>> Get(int id)
        {
            var libro =  await _context.Libros.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<LibroDto>(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDto libroCreacionDto)
        {
            //var existeAutor = await _context.Autores.AnyAsync(x => x.Id == libro.AutorId);

            //if (!existeAutor)
            //{
            //    return BadRequest($"No existe autor de Id: {libro.AutorId}");
            //}

            var libro = _mapper.Map<Libro>(libroCreacionDto);

            _context.Add(libro);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
