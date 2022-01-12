using System.ComponentModel.DataAnnotations;
using System.Text;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace Infrastructure;

public class DeleteSnippetValidation : IDeleteSnippetValidation
{
    private readonly ApplicationDbContext db;

    public DeleteSnippetValidation(ApplicationDbContext context)
    {
        this.db = context;
    }

    public void ValidationSnippet(Snippet snippet)
    {
        var current = this.db.PagesSnippets.Where(pc => pc.SnippetId == snippet.Id).ToList();

        if (current != null)
        {
            var message = new StringBuilder("This snippet is used in some pages: ", 60);

            foreach (var item in current)
            {
                message.Append(item.SnippetId);
                message.Append(' ');
            }

            throw new ValidationException(message.ToString());
        }
    }
}