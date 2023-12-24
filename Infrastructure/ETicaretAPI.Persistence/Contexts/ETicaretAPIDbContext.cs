using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace ETicaretAPI.Persistence.Contexts
{
    //Context sınıfı Veritabanına karşılık gelen yani veritabanının kod kısmındaki karşılığı olarak düşünülebilir.Kod kısmında veritabanı modellenmek isteniyorsa bir tane context sınıfı olmak zorunda. Bu da Dbcontextten kalıtım alır. Dbcontext ef core dan geliyor.
    public class ETicaretAPIDbContext : DbContext //Entityframework orm'ini kullanacağımızdan dolayı veritabanına karşılık gelen context nesnesinin dbcontext sınıfından türemesi gerektiğini söyler. Dbcontext mikrosoft entityframework core kütüphanesi içerisinden gelir bu nedenle install etmek gerekir.
    {
        //IOC container'a biz bu dbcontext'i veriyoruz ordan talep ederken belirli ayarlarda gelmesini istiyorsak  bu ayarların constructor'da bildirilmesi gerekiyor.
        public ETicaretAPIDbContext(DbContextOptions options) : base(options) //DbContextOptions parametresi alan bu parametreyi base'e gönderen bir constuctor oluşturuyoruz. Bu constructor IOC container'da doldurulacak. Bunu IOC container'a koymazsak süreçte hata alırız.
        {
        }

        //Entityler dbcontextte dbset olarak veriliyor. Bir veritabanı olacak bu veritabanının tabloları da bu dbsetteki bu entitylerin formatında tablolarım olacak anlamına gelir.
        public Microsoft.EntityFrameworkCore.DbSet<Product> Products { get; set; } //Veritabanını temsil eden bu dbcontextte sanki veritabanı içinde Product formatında veri olacağını bildirmiş olduk ve adının Products olacağını belirtmiş olduk. Yani product entitysine bakarsak name, stock vs kolonlarına sahip tablo olacak anlamına gelir.
        public Microsoft.EntityFrameworkCore.DbSet<Order> Orders { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Customer> Customers { get; set; }

    }
}
