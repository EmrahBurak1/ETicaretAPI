using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    //Burada controller oluştururken new controller dan API Controller seçiyoruz.
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public IActionResult GetProducts()
        {
            return Ok();
        }
    }
}
