using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Repositories;


namespace UdemyNLayerProject.Data.Repositories
{
    internal class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<Category> GetWithProductsByIdAsync(int categoryId)
        {
            return await _appDbContext.Categories.Include(x => x.Products).SingleOrDefaultAsync(x => x.Id == categoryId);
        }
    }
}
