using Microsoft.AspNetCore.Mvc;
using WebApiAutores.Entities;

namespace WebApiAutores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Autor>> Get() 
        {
            return new List<Autor>()
            {
                new Autor(){ Id = 1, Name="Hugo" },
                new Autor(){ Id = 2, Name="Jorge" }
            };

        }
    }
}
