using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Shop.Application;
using Shop.Application.ViewModels;
using Shop.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ProductViewModel Product { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
        private readonly ILogger<IndexModel> _logger;
        private ApplicationDbContext _ctx;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext ctx)
        {  
            _logger = logger;
            _ctx = ctx;
        }

        public void OnGet() {

            Products = new GetProducts(_ctx).Do();
        }

        public async Task<IActionResult> OnPost()
        {
            await new CreateProduct(_ctx).Do(Product);
            return RedirectToPage("Index");
        }
    }
}
