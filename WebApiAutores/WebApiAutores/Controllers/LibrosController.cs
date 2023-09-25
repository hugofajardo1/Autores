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
            var libro =  await _context.Libros
                .Include(libro => libro.AutorLibros)
                .ThenInclude(autorLibro => autorLibro.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);

            libro.AutorLibros = libro.AutorLibros.OrderBy(x => x.Orden).ToList();

            return _mapper.Map<LibroDto>(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDto libroCreacionDto)
        {

            if (libroCreacionDto.AutoresIds == null)
            {
                return BadRequest("No se pudde crear un libro sin autores");
            }

            var autores = await _context.Autores.Where(autorDB => libroCreacionDto.AutoresIds.Contains(autorDB.Id)).Select(x => x.Id).ToListAsync();

            if (libroCreacionDto.AutoresIds.Count != autores.Count) 
            { 
                return BadRequest("No existen todos los autores"); 
            }

            var libro = _mapper.Map<Libro>(libroCreacionDto);

            for (int i = 0; i < libro.AutorLibros.Count; i++)
            {
                libro.AutorLibros[i].Orden = i+1;
            }

            _context.Add(libro);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
