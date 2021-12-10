using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.DomainModel;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    public class HomeController : ControllerBase
    {
        IPageRepository pageRepository;
        public HomeController(IPageRepository r)
        {
            pageRepository = r;
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<List<Page>> GetPageList()
        {
            return await pageRepository.GetPageListAsync();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePage(Page page)
        {
            page.Content = pageRepository.HtmlCorrector(page.Content);
            await pageRepository.Post(page);

            return Ok(page);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Page))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPagesById(int pageId)
        {
            var page = await pageRepository.Get(pageId);
            if (page == null)
            {
                return NotFound();
            }

            return Ok(page);
        }

        [HttpDelete]
        [Route("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Page))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePage(int pageId)
        {
            var page = await pageRepository.Get(pageId);

            if (page == null)
                return NotFound();

            await pageRepository.Delete(page);

            return Ok();
        }

        [HttpPut]
        [Route("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status205ResetContent, Type = typeof(Page))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePage(int pageId, Page page)
        {
            var item = await pageRepository.Patch(pageId, page);
            
            if(item == null)
                return NotFound();

            return Ok(page);
        }
    }
}
