using Shop.Application.ViewModels;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Shop.Application
{
    public class CreateProduct
    {
        private ApplicationDbContext _context;
        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Do(ProductViewModel vm)
        {
            _context.Products.Add(new Product {
                Name = vm.Name,
                Description = vm.Description,
                Value = Convert.ToDecimal(vm.Value)
            });

            await _context.SaveChangesAsync();
        }
    }
}
