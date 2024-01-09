using ETicaretAPI.Application.Abstraction;
using ETicaretAPI.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    //Burada controller oluştururken new controller dan API Controller seçiyoruz.
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        //Bunları test amaçlı yaptık
        //private readonly IProductService _productService;

        //public ProductsController(IProductService productService) //Artık IOC Containerda productService interface referansına karşılık bize persistencedaki concrete sınıfın nesnesi gelir.
        //{
        //    _productService = productService;
        //}
        //[HttpGet]
        //public IActionResult GetProducts() //Burada Persistence içerisinde bulunan productservice içindeki metgodları tetikleyebilmek için action method oluşturuyoruz.
        //{
        //    var products = _productService.GetProduct();
        //    return Ok(products);
        //}

        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public async void Get()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, CreatedDate = DateTime.UtcNow, Stock = 10},
                new() { Id = Guid.NewGuid(), Name = "Product 2", Price = 50, CreatedDate = DateTime.UtcNow, Stock = 5},
                new() { Id = Guid.NewGuid(), Name = "Product 3", Price = 20, CreatedDate = DateTime.UtcNow, Stock = 2}

            });
            var count = await _productWriteRepository.SaveAsync();
        }
    }
}
