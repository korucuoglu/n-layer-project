using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Repositories;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Core.UnitOfWorks;

namespace UdemyNLayerProject.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IRepository<Product> repository):base(unitOfWork, repository)
        {

        }

        public async Task<Product> GetWithCatByIdAsync(int ProductId)
        {
            // biz Service içerisideki UoF'ü bilerek public yaptık. Bu sayede buradan erişebileceğiz.

            return await _unitOfWork.Products.GetWithCategoryByIdAsync(ProductId);
        }
    }
}
