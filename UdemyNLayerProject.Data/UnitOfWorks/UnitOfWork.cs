using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Repositories;
using UdemyNLayerProject.Core.UnitOfWorks;
using UdemyNLayerProject.Data.Repositories;

namespace UdemyNLayerProject.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ProductRepository _productRepository;
        private CategoryRepository _categoryRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);

        /** 
         * Benden IProductRepository bekliyor. Ben zaten Product Repository'yi IProduct'tan türettiğim için bunu dönebilir. 
         * Eğer Product Repository'im varsa bunu al yok ise (null ise ) yeni bir Product Repository oluştur diyoruz. 
         * 
         * */

        public ICategoryRepository Categories => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);

        public void Commit() // savechanges => ben nerede "commit" çağırırsam veritabanına yansıyacak. 
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
