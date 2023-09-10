using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DataBase;
using WebApiAutores.DTOs;
using WebApiAutores.Entities;

namespace WebApiAutores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AutoresController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get() 
        {
            return await _context.Autores.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id==id);

            if (autor == null) { return NotFound(); }

            return autor;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Autor>> Get(string name)
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Name.Contains(name));

            if (autor == null) { return NotFound(); }

            return autor;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDto autorCreacionDto)
        {
            
            var existeAutorMismoNombre = await _context.Autores.AnyAsync(x => x.Name == autorCreacionDto.Name);
            
            if (existeAutorMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {autorCreacionDto.Name}");
            }

            var autor = _mapper.Map<Autor>(autorCreacionDto);

            _context.Add(autor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id) { return BadRequest(); }

            var existe = await _context.Autores.AnyAsync(x => x.Id == id);
            if (!existe) { return NotFound(); }

            _context.Update(autor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Autores.AnyAsync(x => x.Id == id);
            if (!existe) { return NotFound(); }

            _context.Remove(new Autor { Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
