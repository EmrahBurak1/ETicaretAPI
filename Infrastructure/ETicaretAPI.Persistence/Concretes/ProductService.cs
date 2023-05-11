using ETicaretAPI.Application.Abstraction;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        //Normalde burada veritabanından ürünler çekilerek geridöndürülür veritabanından çekiyormuş gibi list oluşturup geridöndürüyoruz. Burada c#'ın bir özelliği olan targettype özelliğini kullanarak bir koleksiyon oluşturuyoruz.
        public List<Product> GetProduct()
            => new()
            {
                new() {Id = Guid.NewGuid(), Name = "Product1", Price = 100, Stock = 10}, //Guid türünde bir nesne yaratmak için guid.newguid demek gerekiyor. Diğerleri price ve stock doğrudan girilebilir. Her bir Guid.NewGuid() fonksiyonu farklı bir unique değer üretir.
                new() {Id = Guid.NewGuid(), Name = "Product2", Price = 200, Stock = 10},
                new() {Id = Guid.NewGuid(), Name = "Product3", Price = 300, Stock = 10},
                new() {Id = Guid.NewGuid(), Name = "Product4", Price = 400, Stock = 10},
                new() {Id = Guid.NewGuid(), Name = "Product5", Price = 500, Stock = 10}
            };
    }
}
