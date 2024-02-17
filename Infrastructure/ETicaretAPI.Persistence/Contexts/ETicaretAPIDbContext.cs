using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Common;
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

        //Productcontroller içerisinde detayları yazan intercepter ile bir işin başlangıç ve bitiş süresiinin arasına girerek otomatik doldurulması gereken alanlar varsa bunları doldurabiliyoruz. Örneğin createdate veya updatedate gibi.
        //Her savechanges tetiklendiğinde insert ve update edilen tüm datalara erişim üzerinlerinde istediğimiz değişiklikleri yapabiliyoruz.
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //Yapılan değişiklikle changetracker nesnesi ile yakalanabilir.Entityler üzerinden yapılan değişikliklerin ya da yeni eklenen verilerin yakalanmasını sağlayan propertylerdir. Update operasyonlarında track edilen verileri yakalayıp elde etmemizi sağlar.
            var datas = ChangeTracker.Entries<BaseEntity>(); //Entries içerisine yazılan baseentity ile tüm entitylere erişebiliyoruz.

            foreach (var data in datas)
            {
               _ = data.State switch //Not: Discard yapılanmasıyla bu şekilde _ koyarak herhangi bir atama yapılmamasını isteyebiliyoruz. Çünkü ihtiyacımız yok. Return ile herhangi bir değer döndürmemize gerek yok.
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow, //Örneğin entitymizde bir ekleme işlemi yapıldıysa burada kontrol edebiliyoruz
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow //Eğer update işlemi yapıldıysa burada kontrol ediyoruz.
                };
            }
            
            
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
