using Microsoft.AspNetCore.Mvc;

namespace MyApp.Domain.Services
{
    public interface ICreativeCrudService
    {
        Task<IActionResult> Create(Creative creative);

        Task<IActionResult> GetById(int id);

        Task<IActionResult> Delete(int id);

        Task<IActionResult> Update(int id, Creative creative);
    }
}
