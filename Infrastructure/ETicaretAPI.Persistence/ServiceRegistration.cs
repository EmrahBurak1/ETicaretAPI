using ETicaretAPI.Application.Abstraction;
using ETicaretAPI.Persistence.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence
{
    public static class ServiceRegistration //Extention fonksiyonlar bu şekilde registration methodunun içerisinde tanımlanır. Bunların görevi örneğin API katmanı içerisindeki ProductsController dan Application katmanındaki IProductservice e bir istek atılır bu service ile de Presistence katmanındaki ProductService den veriler alınır. Bizim presentation katmanımızda IOC Container bulunuyor. Buradan verilen istekle hangi methodların çalıştırılacağı registration sınıfı içerisinde yapılıyor.
    {
        public static void AddPersistenceServices(this IServiceCollection services) //AddPersistence ile IOC Container'a eklemiş oluyoruz. IOC containerın nerede olduğunu da program.cs içerisinde builder.services üstüne gelirsen IServiceCollection olduğunu görürüz yani bizim IOC Containerımız bu oluyor. Bu service üzerinden ilgili servislerimizi entegre edebiliriz.
        {
            //Artık burada program.cs içerisinde service ile neye erişebiliyorsan burada da erişebilicez.
            services.AddSingleton<IProductService, ProductService>(); 
        }
    }
}
