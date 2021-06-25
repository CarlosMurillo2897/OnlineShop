using Shop.Application.ViewModels;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    public class UpdateProduct
    {
        private ApplicationDbContext _context;
        public UpdateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductViewModel> Do(ProductViewModel vm)
        {
            await _context.SaveChangesAsync();
            return new ProductViewModel();
        }
    }
}
