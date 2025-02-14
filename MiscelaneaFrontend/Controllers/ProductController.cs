using Microsoft.AspNetCore.Mvc;
using MiscelaneaFrontend.Models;
using MiscelaneaFrontend.Service;

namespace MiscelaneaFrontend.Controllers
{
    public class ProductController : Controller
    {
        public  IActionResult Index()
        {
            var service = new ProductService();
            var response  =   service.GetProductsAsync();
            ViewData["productList"] = response.Result.Item2;            
            Console.WriteLine("adsdasd");
            return View();
        }

        public IActionResult RegisterProduct()
        {
            return View("RegisterProduct");
        }

        [HttpPost]
        public IActionResult RegisterProduct(ProductDTO product)
        {
            ProductService service = new ProductService();
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            ModelState.Clear();
            var response = service.PostProductAsync(product);
            ViewData["codeRegisterProduct"] = response.Result;
            return View(product);
        }

      
    }
}

