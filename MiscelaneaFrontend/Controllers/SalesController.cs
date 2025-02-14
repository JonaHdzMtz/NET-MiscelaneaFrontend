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
            var stock = DeserializeTempData(stockJson);
            var wishListJson = TempData.Peek("wishList") as string;
            var cart = wishListJson != null ? DeserializeTempData(wishListJson) : null;
            if (stock != null)
            {
                var itemSelected = stock.Find(x => x.IdProduct == int.Parse(pas));
                if (cart != null) //EXISTE UN CARRITO
                {
                    if (itemSelected != null) cart.Add(itemSelected);
                    TempData["wishList"] = JsonSerializer.Serialize(cart);
                    TempData.Keep("wishList");
                }
                else //PRIMER PRODUCTO EN CARRITO
                {
                    List<ItemCart> newCart = new List<ItemCart>();
                    ItemCart newItemCart = new ItemCart()
                    {
                        IdItemCart = itemSelected.IdProduct,
                        ProductName= itemSelected.ProductName,
                        PriceUnit = (decimal)itemSelected.Price ,
                        Stock = 1,
                        Subtotal = (decimal)itemSelected.Price
                    };
                    newCart.Add(newItemCart);
                    TempData["wishList"] = JsonSerializer.Serialize(newCart);
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
