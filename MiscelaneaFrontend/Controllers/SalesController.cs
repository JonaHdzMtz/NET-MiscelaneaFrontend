using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MiscelaneaFrontend.Models;
using MiscelaneaFrontend.Service;

namespace MiscelaneaFrontend.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CreateSale()
        {
            var service = new ProductService();
            var response = service.GetProductsAsync();
            TempData["wishList"] = null;
            TempData["productList"] = JsonSerializer.Serialize(response.Result.Item2);
            TempData.Keep("productList");
            return View("SaleForm");
        }
        [HttpPost]
        public IActionResult AddProduct(string pas)
        {
            var stockJson = TempData.Peek("productList") as string;
            var stock = JsonSerializer.Deserialize<List<ProductDTO>>(stockJson);

            var wishListJson = TempData.Peek("wishList") as string;
            var cart = wishListJson != null ?
                JsonSerializer.Deserialize<List<ItemCart>>(wishListJson) : new List<ItemCart>();

            if (stock != null)
            {
                var itemSelected = stock.Find(x => x.IdProduct == int.Parse(pas));

                if (itemSelected != null)
                {
                    var existingItem = cart.FirstOrDefault(x => x.IdItemCart == itemSelected.IdProduct);
                    if (existingItem != null)
                    {
                        // Si el producto ya está en el carrito, aumenta la cantidad y recalcula el subtotal
                        existingItem.Stock += 1;
                        existingItem.Subtotal = existingItem.Stock * existingItem.PriceUnit;
                    }
                    else
                    {
                        // Si es un producto nuevo en el carrito
                        cart.Add(new ItemCart()
                        {
                            IdItemCart = itemSelected.IdProduct,
                            ProductName = itemSelected.ProductName,
                            PriceUnit = (decimal)itemSelected.Price,
                            Stock = 1,
                            Subtotal = (decimal)itemSelected.Price
                        });
                    }

                    // Guarda el carrito actualizado
                    TempData["wishList"] = JsonSerializer.Serialize(cart);
                    TempData.Keep("wishList");
                }
            }

            return View("SaleForm");
        }



        public List<ProductDTO> DeserializeTempData(string jsonData)
        {
            return JsonSerializer.Deserialize<List<ProductDTO>>(jsonData);
        }
        public List<ProductDTO> DeserializeWishList(string jsonData)
        {
            return JsonSerializer.Deserialize<List<ProductDTO>>(jsonData);
        }
    }
}
