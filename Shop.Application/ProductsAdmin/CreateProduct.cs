using Shop.Application.ViewModels;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    public class CreateProduct
    {
        private ApplicationDbContext _context;
        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductViewModel> Do(ProductViewModel vm)
        {
            var product = new Product
            {
                Name = vm.Name,
                Description = vm.Description,
                Value = Convert.ToDecimal(vm.Value)
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync();
            vm.Id = product.Id;

            return vm;
        }
    }
}
