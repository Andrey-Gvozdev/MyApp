namespace MyApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.DomainModel;

    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    public class HomeController : ControllerBase
    {
        private readonly IPageRepository pageRepository;

        public HomeController(IPageRepository r)
        {
            this.pageRepository = r;
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<List<Page>> GetPageList()
        {
            return await this.pageRepository.GetPageListAsync();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePage(Page page)
        {
            page.Content = this.pageRepository.HtmlCorrector(page.Content);
            await this.pageRepository.Post(page);

            return this.Ok(page);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Page))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPagesById(int pageId)
        {
            var page = await this.pageRepository.Get(pageId);
            if (page == null)
            {
                return this.NotFound();
            }

            return this.Ok(page);
        }

        [HttpDelete]
        [Route("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Page))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePage(int pageId)
        {
            var page = await this.pageRepository.Get(pageId);

            if (page == null)
            {
                return this.NotFound();
            }

            await this.pageRepository.Delete(page);

            return this.Ok();
        }

        [HttpPut]
        [Route("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status205ResetContent, Type = typeof(Page))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePage(int pageId, Page page)
        {
            var item = await this.pageRepository.Patch(pageId, page);

            return item == null ? this.NotFound() : this.Ok(page);
        }
    }
