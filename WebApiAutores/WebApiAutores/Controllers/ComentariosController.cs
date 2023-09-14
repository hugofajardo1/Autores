using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiAutores.DataBase;
using WebApiAutores.DTOs;

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
            this._context = context;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(int libroId, ComentarioCreacionDto comentarioCreacionDto)
        {

        }
    }
}
