﻿using ETicaretAPI.Application.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using ETicaretAPI.Persistence.Repositories;
using ETicaretAPI.Application.Repositories;

namespace ETicaretAPI.Persistence
{
    public static class ServiceRegistration //Extention fonksiyonlar bu şekilde registration methodunun içerisinde tanımlanır. Bunların görevi örneğin API katmanı içerisindeki ProductsController dan Application katmanındaki IProductservice e bir istek atılır bu service ile de Presistence katmanındaki ProductService den veriler alınır. Bizim presentation katmanımızda IOC Container bulunuyor. Buradan verilen istekle hangi methodların çalıştırılacağı registration sınıfı içerisinde yapılıyor.
    {
        public static void AddPersistenceServices(this IServiceCollection services) //AddPersistence ile IOC Container'a eklemiş oluyoruz. IOC containerın nerede olduğunu da program.cs içerisinde builder.services üstüne gelirsen IServiceCollection olduğunu görürüz yani bizim IOC Containerımız bu oluyor. Bu service üzerinden ilgili servislerimizi entegre edebiliriz.
        {

            //Artık burada program.cs içerisinde service ile neye erişebiliyorsan burada da erişebilicez.
            //services.AddSingleton<IProductService, ProductService>(); 

            //Buraya eklediğimiz her şey IOC container'a eklenmiş oluyor. Çünkü bu methodu program.cs içerisinde çağırıyoruz.
            //Aşağıda addcontext ile hangi veritabanına migrate edeceksek options ile onu seçmeliyiz. Yani mssql kullanıyorsak usemssql, plsql kullanıyorsak useolsql gibi seçenekler çıkması lazım biz postgresql kullanacağımız için persistence katmanına sağ tıklayıp nudget package'i açıp postgresql yazarak ef core postgresql kütüphanesini yüklüyoruz bunu da using ile projeye dahil ediyoruz.Postgresql'in ismi UseNpgsql budur.
            //Not: Scoped da her request için oluşturulan nesne iş bittikten sonra imha/dispose edilir.
            services.AddDbContext<ETicaretAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString)); //Use ile postgresql seçildikten sonra bizden connection string istiyor. Onu da internetten yazarak buluyoruz.Connecsiton string sitesine girip postgresql seçilerek postgresql'e uygun connection stringi alabiliriz. ETicaretAPIDb database ismi db ye hangi isimle kaydedileceğini belirler. Yani ETicaretAPIDb database'i oluşturacak. ServiceLifetime.Singleton'ı sonradan ekledik bunun amacı uygulamaya ait bir tane dbcontext oluşturmasını talep ediyoruz. Bunu yazmazsak controller içerisindeki constructorda birden fazla parametre verirsek swegger da patlıyor. Not: Sonradan daha düzgün bir yapıya çevirebilmek için daha doğrusu alternatif olarak AddScoped özelliği ile devam ettik. Ayrıca yazdığımız Servicelifetime.singleton kaldırıldı.

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>(); //Artık repository servislerimizi IOC Container'a eklememiz gerekiyor. Onu da bu şekilde yapabiliyoruz. Bunun anlamı ICustomerReadRepository istendiğinde biz CustomerReadRepository'yi döneceğiz.
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        }
    }
}
