using ETicaretAPI.Application.Abstraction;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
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
        public async Task Get() //Buraya Task yazmamızın sebebi ServiceRegistration.cs içerisinde AddScoped ile tanımlama yapmış olmamızdan kaynaklı. Task ile ilgili repository beklenecek ve sonraki method tetiklenirken context nesnesi dispose edilmeden ilgili işlemler yapılmış olacak. Eğer buraya task yazmazsak void olarak bırakırsak bu sefer swagger ile api tetiklendiğinde kodda dispose hatası ile karşılaşıyoruz. Yani controller void dönüş tipindeyken repository beklemiyor o yüzden ekleme işlemi yapmadan repositoryi dispose ediyor.
        {
            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, CreatedDate = DateTime.UtcNow, Stock = 10},
            //    new() { Id = Guid.NewGuid(), Name = "Product 2", Price = 50, CreatedDate = DateTime.UtcNow, Stock = 5},
            //    new() { Id = Guid.NewGuid(), Name = "Product 3", Price = 20, CreatedDate = DateTime.UtcNow, Stock = 2}

            //});
            //var count = await _productWriteRepository.SaveAsync();

            //Tracking işlemlerini test ediyoruz.
            Product p = await _productReadRepository.GetByIdAsync("bd57d74c-0b81-42e8-9a6d-61382c064940", false); //false parametresi ile tracking kapatılırsa gelecek olan datayı track etme anlamına gelir. Yani biz bunun takip edilmesini bunun üzerinde yapılacak değişiklerin herhangi bir şekilde sorguya dönüştürülmesini istemiyorsak false olarak işaretliyoruz. Bundan sonra ne kadar değişik yaparsak yapalım. Ne kadar savechanges fonksiyonunu tetiklersek tetikleyelim ilgili değişiklikler fiziksel veri tabanına yansıtılmıcaktır çünkü artık ilgili sorgu neticesinde context üzerinden gelen data tracking mekanizmasında takip edilmeyecektir. Read repositorylerde bu optimizasyon önemli çünkü ürünler yorumlar listelencek ve bunlar üzerinde değiştirme yapılmayacak.        
            p.Name = "Mehmet"; //Name'ini ahmet olarak değiştirdik.
            await _productWriteRepository.SaveAsync(); //Not: read repository ile çektiğimiz veriyi write repository ile yazabiliyoruz aynı instance'ı kullanmış oluyor scope özelliğinden dolayı.
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
