using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.DomainModel;

namespace Infrastructure
{
    public class PageRepository : IPageRepository
    {
        private ApplicationDbContext db;

        public PageRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public Task<List<Page>> GetPageListAsync()
        {
            return db.Pages.ToListAsync();
        }

        public async Task Post(Page page)
        {
            await db.Pages.AddAsync(page);
            await db.SaveChangesAsync();
        }

        public Task<Page?> Get(int pageId)
        {
            return db.Pages.FirstOrDefaultAsync(x => x.Id == pageId);
        }

        public async Task<Page> Patch(int pageId, Page page)
        {
            var current = await db.Pages.FindAsync(pageId);
            if (current == null)
                return current;

            page.Id = current.Id;

            db.Entry(current).CurrentValues.SetValues(page);
            await db.SaveChangesAsync();

            return current;
        }

        public async Task Delete(Page page)
        {
            db.Pages.Remove(page);
            await db.SaveChangesAsync();
        }

        public string HtmlCorrector (string content)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);
            string res;

            content = content.Replace("<!DOCTYPE html>", "");

            var htmlHtml = htmlDoc.DocumentNode.SelectSingleNode("//html");

            var htmlHead = htmlDoc.DocumentNode.SelectSingleNode("//head");

            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body");

            if (htmlHtml == null)
                if (htmlHead == null && htmlBody == null)
                    res = "<!DOCTYPE html>\n" + "<html>\n" + "<head></head>\n" + "<body>\n" + content + "\n</body>" + "\n</html>";
                else
                {
                    if (htmlHead == null)
                        htmlHead = htmlDoc.CreateElement("head");
                    if (htmlBody == null)
                        htmlBody = htmlDoc.CreateElement("body");

                    res = "<!DOCTYPE html>\n" + "<html>\n" + htmlHead.OuterHtml + "\n" + htmlBody.OuterHtml + "\n</html>";
                }
            else
                res = "<!DOCTYPE html>\n" + htmlHtml.OuterHtml;

            return res;
        }
    }
}
