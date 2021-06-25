using Shop.Application.ViewModels;
using Shop.Database;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.ProductsAdmin
{
    public class GetProducts
    {
        private ApplicationDbContext _ctx;

        public GetProducts(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<ProductViewModel> Do() => 
            _ctx.Products.ToList().Select( x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Value = x.Value.ToString(), // 1100.50 => 1,100.50
            });
    }
}
