using Microsoft.AspNetCore.Mvc;
using MyApp.Domain;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    public class HomeController : ControllerBase
    {
        ICreativeRepository creativeRepository;
        public HomeController(ICreativeRepository r)
        {
            creativeRepository = r;
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IEnumerable<Creative>> GetList()
        {
            return await creativeRepository.GetCreativeListAsync();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(Creative item)
        {
            creativeRepository.Post(item);

            return CreatedAtAction("Create", new { id = item.Id }, item);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Creative))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var creative = creativeRepository.Get(id);
            if (creative == null)
            {
                return NotFound();
            }

            return Ok(creative);
        }
    }
}
