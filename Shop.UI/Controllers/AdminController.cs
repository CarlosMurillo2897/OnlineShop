using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using Shop.Application.ViewModels;
using Shop.Database;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _ctx;

        public AdminController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("products")]
        public IActionResult GetProducts() => Ok(new GetProducts(_ctx).Do());

        [HttpGet("products/{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_ctx).Do(id));
        
        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductViewModel vm) => Ok(await new CreateProduct(_ctx).Do(vm));
        
        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id) => Ok(await new DeleteProduct(_ctx).Do(id));
        
        [HttpPut("products")]
        public async Task<IActionResult> UpdateProduct(ProductViewModel vm) => Ok(await new UpdateProduct(_ctx).Do(vm));
    }
}
