using Shop.Application.ViewModels;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Linq;
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
            var product = _context.Products.FirstOrDefault(x => x.Id == vm.Id);
            
            product.Name = vm.Name;
            product.Description = vm.Description;
            product.Value = Convert.ToDecimal(vm.Value);

            await _context.SaveChangesAsync();
            return vm;
        }
    }
}
