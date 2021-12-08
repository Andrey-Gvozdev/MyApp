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

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IEnumerable<Creative>> GetList()
        {
            return await creativeRepository.GetCreativeListAsync();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(Creative creative)
        {
            await creativeRepository.Post(creative);

            return CreatedAtAction("Create", new { id = creative.Id }, creative);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Creative))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int crteativeId)
        {
            var creative = creativeRepository.Get(crteativeId);
            if (creative == null)
            {
                return NotFound();
            }

            return Ok(creative);
        }

        [HttpDelete]
        [Route("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Creative))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int creativeId)
        {
            Creative creative = creativeRepository.Get(creativeId);

            if (creative == null)
                return NotFound();

            creativeRepository.Delete(creativeId);

            return Ok(creative);
        }

        [HttpPut]
        [Route("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status205ResetContent, Type = typeof(Creative))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int creativeId, Creative creative)
        {
            creativeRepository.Patch(creativeId, creative);

            return Ok(creative);
        }
    }
}
