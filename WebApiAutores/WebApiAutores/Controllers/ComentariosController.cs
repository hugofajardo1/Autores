using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DataBase;
using WebApiAutores.DTOs;
using WebApiAutores.Entities;

namespace WebApiAutores.Controllers
{
    [Route("api/libros/{libroId:int}/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ComentariosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ComentarioDto>>> Get(int libroId)
        {
            var existe = await _context.Libros.AnyAsync(libroDB => libroDB.Id == libroId);
            if (!existe)
            {
                return NotFound();
            }

            var comentarios = await _context.Comentarios.Where(comentarioDb => comentarioDb.LibroId == libroId).ToListAsync();
            return _mapper.Map<List<ComentarioDto>>(comentarios);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int libroId, ComentarioCreacionDto comentarioCreacionDto)
        {
            var existe = await _context.Libros.AnyAsync(libroDB => libroDB.Id == libroId);
            if (!existe)
            {
                return NotFound();
            }

            var comentario = _mapper.Map<Comentario>(comentarioCreacionDto);
            comentario.LibroId = libroId;
            _context.Add(comentario);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
