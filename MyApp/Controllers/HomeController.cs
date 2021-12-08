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
        public Task<List<Creative>> GetList()
        {
            return creativeRepository.GetCreativeListAsync();
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
        [Route("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Creative))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int creativeId)
        {
            var creative = await creativeRepository.Get(creativeId);
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
        public async Task<IActionResult> Delete(int creativeId)
        {
            var creative = await creativeRepository.Get(creativeId);

            if (creative == null)
                return NotFound();

            await creativeRepository.Delete(creative);

            return Ok();
        }

        [HttpPut]
        [Route("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status205ResetContent, Type = typeof(Creative))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int creativeId, Creative creative)
        {
            var item = await creativeRepository.Patch(creativeId, creative);
            
            if(item == null)
                return NotFound();

            return Ok(creative);
        }
    }
}
