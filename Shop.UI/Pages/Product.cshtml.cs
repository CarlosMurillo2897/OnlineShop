using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Database;
using Shop.Application.Products;
using Shop.Application.Cart;

namespace Shop.UI.Pages
{
    public class ProductModel : PageModel
    {
        private ApplicationDbContext _ctx;
        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }
        public GetProduct.ProductViewModel Product { get; set; }
        
        public ProductModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult OnGet(string name)
        {
            Product = new GetProduct(_ctx).Do(name.Replace("--", " "));
            if(Product == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            new AddToCart(HttpContext.Session).Do(CartViewModel);

            return RedirectToPage("Cart");
        }
    }
}
