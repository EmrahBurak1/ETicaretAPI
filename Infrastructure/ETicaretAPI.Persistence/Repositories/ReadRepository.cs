﻿using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity //IReadRepository bizden bir T istiyor bunu burada kullanabilmek için de ReadRepository'ye generic olarak T yazmamız gerekir. Daha sonra da bunun bir class olduğunu belirtmeliyiz.
    {
        private readonly ETicaretAPIDbContext _context; //IOC containerdan geleceği için bu şekilde ekliyoruz. IOC containerdan gelmesi için de ReadRepository'nin IOCContainer'a eklenmesi gerekiyor.
        //Daha sonra interfacedeki methodları buraya otomatik implemente edebiliriz.

        public ReadRepository(ETicaretAPIDbContext context) //Constructure'ı otomatik oluşturabiliyoruz. 
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>(); //DbSet'in T türünde olacağı belli. Bizim context'imiz içinde T türünde bir nesne yok örneğin customer veya product türünde nesneler var ama generic yapı kurmak istediğimiz için T yazmamız gerekiyor. Bunu da ORM de bulunan set ile yapıyoruz.

        public IQueryable<T> GetAll(bool tracking = true)
        //Not: Artık tracking ile çalışacağımız için aşağıdaki yöntemi yorum yaptık.
        // => Table; //GetAll la T'ye uygun veritabanında ne varsa bize getirsin istiyorduk. Bunu da kolayca Table yazarak yapabiliriz. Çünkü Table dediğimiz aslında DbSet oluyor. DbSet te IQueryable olduğundan dolayı (Verdiğimiz T ye uygun) doğrudan tü mverileri bize getirir.
        {
            var query = Table.AsQueryable();
            if (!tracking) //Burada eğer track edilmesini istemiyorsak bu şekilde gelen dataların track işlemini kesiyoruz.
            {
                query = query.AsNoTracking(); //Tracking false olursa bu şekilde track işlemini koparabiliyoruz. Oluşabilecek yavaşlamanın vs önüne geçebilmek için false yapılabilir.
            }
            return query;
        }
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        // => Table.Where(method); //Burada da where özelliğini kullanıyoruz. Asenkron çalışmadığı için ismin sonuna async eklemedik. İçerisine de üstte tanımladığımız method alias'ı vermemiz yeterli.
        {
            var query = Table.Where(method);
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        //  => await Table.FirstOrDefaultAsync(method); //FirstorDefault Async olanı seçiyoruz bu sayede asenkron çalışmış oluyor. İçerisine method yazmamız yeterli. Ayrıca bize T döndürdüğünden dolayı başına da await ve yukarısına da async yazmamız gerekiyor. Burada asenkron olarak veri bekleniyor yani await ile bekliyor daha sonra veri geldiğinde T türünde veri döndürüyor. 
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = Table.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(method);
        }
        public async Task<T> GetByIdAsync(string id, bool tracking = true) 
         //Generic yapılanmalardsa id gibi değersel çalışmak istiyorsak o değeri temsil eden bir arayüz veya sınıf tasarlamamız gerekiyor. Biz bunu BaseEntity içerisinde yapabiliriz. Buna işaretleyici (marker) pattern denir. Bunun içinde IReporsitory interfase'ine gitip bu interface'i class yerine baseentity'den türeyecek şekilde yazarız. Sonuçta baseentity de bir class. Aynı işlemler IRead ve IWrite repository'ye de yapılır.
         //=> await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id)); //Burada artık baseentity den türediği için içinde id barındırmak zorunda ve burada o id yi kullanabiliyoruz. Data yerine herhangi bir şey yazabiliriz bizim o andaki verimizi temsil eder. Daha sonra string olan id mizi guid'e çevirip kullanabiliyoruz.  Kullanıdığımızda orm find methodunu desteklemiyorsa marker design pattern'ı kullanılabilir. Ama bizim ef find methodunu desteklediği için aşağıda buna uygun yapıyı kullanacağız. 
         // => await Table.FindAsync(Guid.Parse(id)); //Methodda id yi string olarak veriyoruz fakat burada da Guid.Parse ile convert etmemiz gerekiyor yoksa hata verir.
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = Table.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id)); //Tracking yapısında findasync olmadığı için firstordefault ile marker pattern üzerinden id ye erişip sorgulama sağlanır. 
        }
     }
}
