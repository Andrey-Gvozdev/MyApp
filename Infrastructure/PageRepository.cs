namespace Infrastructure;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.DomainModel;

    public class PageRepository : IPageRepository
    {
        private readonly ApplicationDbContext db;

        public PageRepository(ApplicationDbContext context)
        {
            this.db = context;
        }

        public Task<List<Page>> GetPageListAsync()
        {
            return this.db.Pages.ToListAsync();
        }

        public async Task Post(Page page)
        {
            await this.db.Pages.AddAsync(page);
            await this.db.SaveChangesAsync();
        }

        public Task<Page> Get(int pageId)
        {
            return this.db.Pages.FirstOrDefaultAsync(x => x.Id == pageId);
        }

        public async Task<Page> Patch(int pageId, Page page)
        {
            var current = await this.db.Pages.FindAsync(pageId);
            if (current == null)
            {
                return current;
            }

            page.Id = current.Id;

            this.db.Entry(current).CurrentValues.SetValues(page);
            await this.db.SaveChangesAsync();

            return current;
        }

        public async Task Delete(Page page)
        {
            this.db.Pages.Remove(page);
            await this.db.SaveChangesAsync();
        }

        public string HtmlCorrector(string content)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);
            string res;

            content = content.Replace("<!DOCTYPE html>", string.Empty);

            var htmlHtml = htmlDoc.DocumentNode.SelectSingleNode("//html");

            var htmlHead = htmlDoc.DocumentNode.SelectSingleNode("//head");

            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body");

            if (htmlHtml == null)
            {
                if (htmlHead == null && htmlBody == null)
                {
                    res = "<!DOCTYPE html>\n" + "<html>\n" + "<head></head>\n" + "<body>\n" + content + "\n</body>" + "\n</html>";
                }
                else
                {
                    if (htmlHead == null)
                    {
                        htmlHead = htmlDoc.CreateElement("head");
                    }

                    if (htmlBody == null)
                    {
                        htmlBody = htmlDoc.CreateElement("body");
                    }

                    res = "<!DOCTYPE html>\n" + "<html>\n" + htmlHead.OuterHtml + "\n" + htmlBody.OuterHtml + "\n</html>";
                }
            }
            else
            {
                res = "<!DOCTYPE html>\n" + htmlHtml.OuterHtml;
            }

            return res;
        }
    }