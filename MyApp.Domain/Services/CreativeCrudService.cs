using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services
{
    public class CreativeCrudService : ControllerBase, ICreativeCrudService
    {
        private readonly ICreativeRepository creativeRepository;

        public CreativeCrudService(ICreativeRepository repository)
        {
            this.creativeRepository = repository;
        }

        public async Task<IActionResult> Create(Creative page)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                await this.creativeRepository.Create(page);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
                {
                    return this.BadRequest("This name is already taken");
                }
                else
                {
                    return this.BadRequest("Unhandled error");
                }
            }

            return this.Ok(page);
        }

        public async Task<IActionResult> GetById(int pageId)
        {
            var page = await this.creativeRepository.Get(pageId);

            if (page == null)
            {
                return this.NotFound($"Page vith id: {pageId} not found");
            }

            return this.Ok(page);
        }

        public async Task<IActionResult> Delete(int pageId)
        {
            var page = await this.creativeRepository.Get(pageId);

            if (page == null)
            {
                return this.NotFound($"Page vith id: {pageId} not found");
            }

            await this.creativeRepository.Delete(page);

            return this.Ok("Item deleted");
        }

        public async Task<IActionResult> Update(int pageId, Creative page)
        {
            var oldPage = await this.creativeRepository.Get(pageId);

            if (oldPage == null)
            {
                return this.NotFound($"Page vith id: {pageId} not found");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                await this.creativeRepository.Update(oldPage, page);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
                {
                    return this.BadRequest("This name is already taken");
                }
                else
                {
                    return this.BadRequest("Unhandled error");
                }
            }

            return this.Ok(page);
        }
    }
}
