using ETicaretAPI.Application.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    //Burada controller oluştururken new controller dan API Controller seçiyoruz.
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService) //Artık IOC Containerda productService interface referansına karşılık bize persistencedaki concrete sınıfın nesnesi gelir.
        {
            _productService = productService;
        }
        [HttpGet]
        public IActionResult GetProducts() //Burada Persistence içerisinde bulunan productservice içindeki metgodları tetikleyebilmek için action method oluşturuyoruz.
        {
            var products = _productService.GetProduct();
            return Ok(products);
        }
    }
}
