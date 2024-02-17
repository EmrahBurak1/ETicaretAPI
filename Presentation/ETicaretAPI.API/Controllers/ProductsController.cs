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

        readonly private IOrderWriteRepository _orderWriteRepository; //ProductController'ı test amaçlı oluşturduğumuz için deneme amaçlı Order'ı burada kullanıyoruz.
        readonly private IOrderReadRepository _orderReadRepository;

        readonly private ICustomerWriteRepository _customerWriteRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
            _orderReadRepository = orderReadRepository;
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
            //Product p = await _productReadRepository.GetByIdAsync("bd57d74c-0b81-42e8-9a6d-61382c064940", false); //false parametresi ile tracking kapatılırsa gelecek olan datayı track etme anlamına gelir. Yani biz bunun takip edilmesini bunun üzerinde yapılacak değişiklerin herhangi bir şekilde sorguya dönüştürülmesini istemiyorsak false olarak işaretliyoruz. Bundan sonra ne kadar değişik yaparsak yapalım. Ne kadar savechanges fonksiyonunu tetiklersek tetikleyelim ilgili değişiklikler fiziksel veri tabanına yansıtılmıcaktır çünkü artık ilgili sorgu neticesinde context üzerinden gelen data tracking mekanizmasında takip edilmeyecektir. Read repositorylerde bu optimizasyon önemli çünkü ürünler yorumlar listelencek ve bunlar üzerinde değiştirme yapılmayacak.        
            //p.Name = "Mehmet"; //Name'ini ahmet olarak değiştirdik.
            //await _productWriteRepository.SaveAsync(); //Not: read repository ile çektiğimiz veriyi write repository ile yazabiliyoruz aynı instance'ı kullanmış oluyor scope özelliğinden dolayı.

            //Yapılan bir işin başlangıç ve bitiş süresi var örneğin bir veri eklerken bu sürenin arasına girerek intercepter ekliyoruz. Ve bu intercapter ile de sabit olarak dolması gereken alanlarımız varsa onları ekleyebiliyoruz. Örneğin createdate alanı her zaman doldurulması gerekiyor bunun gibi bir sürü alan olabilir her defasında bunları tek tek doldurmak yerine intercapter oluşturarak otomatik bir şekilde doldurabiliyoruz.
            //await _productWriteRepository.AddAsync(new() { Name = "C Product", Price = 1.500F, Stock = 10, CreatedDate = DateTime.UtcNow });
            //await _productWriteRepository.SaveAsync();

            //var customerId = Guid.NewGuid(); //Order tablosuna ekleme yapmak için customer id foreign key olduğu için customer tablosuna da bir veri ekledik.
            //await _customerWriteRepository.AddAsync(new() { Id = customerId, Name = "Muhittin" });

            //await _orderWriteRepository.AddAsync(new() { Description = "bla bla bla", Address = "Ankara, Çankaya", CustomerId = customerId });
            //await _orderWriteRepository.AddAsync(new() { Description = "bla bla bla 2", Address = "Ankara, Pursaklar", CustomerId = customerId });
            //await _orderWriteRepository.SaveAsync();


            Order order = await _orderReadRepository.GetByIdAsync("ae1adce7-349e-4c33-8b9b-7bf86a9ff0fe");
            order.Address = "İstanbulll";
            await _orderWriteRepository.SaveAsync();
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    Product product = await _productReadRepository.GetByIdAsync(id);
        //    return Ok(product);
        //}
    }
}
