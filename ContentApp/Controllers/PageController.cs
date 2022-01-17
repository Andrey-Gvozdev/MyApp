using ContentApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ContentApp.Controllers
{
    [ApiController]
    [Route("api/page")]
    [Produces("application/json")]
    public class PageController : Controller
    {
        private readonly IPageRepository pageRepository;

        public PageController(IPageRepository pageRepository)
        {
            this.pageRepository = pageRepository;
        }

        [HttpGet]
        public Task<List<Page>> Get()
        {
            return this.pageRepository.GetListAsync();
        }
    }
}
