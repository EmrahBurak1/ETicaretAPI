using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        //İnterface'i implemente ettikten sonra ilk yapacağımız iş context nesnemizi talep etmek onu da aşağıdaki şekilde yapabiliyoruz.
        readonly private ETicaretAPIDbContext _context;

        public WriteRepository(ETicaretAPIDbContext context)
        {
            _context = context;
        }

        public Microsoft.EntityFrameworkCore.DbSet<T> Table => _context.Set<T>(); //İlgili DbSet'i bu şekilde elde edebiliyoruz.

        public async Task<bool> AddAsync(T datas)
        {
           EntityEntry<T> entityEntry = await Table.AddAsync(datas); //Add işlemimizi bu şekilde yapabiliyoruz. Bu işlem bize bir EntityEntry döndüğü için türünü bu şekilde başına yazmamız gerekiyor. 
            return entityEntry.State == EntityState.Added; //Bu şekilde ekleme işlemi mi değil mi geriye değer döndürebiliyoruz.
        }

        public async Task<bool> AddRangeAsync(List<T> model)
        {
            await Table.AddRangeAsync(model);
            return true; //Herhangi bir değer dönmüyor o yüzden return true diyebiliriz.
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model); //Removeda asenkron kullanmadık çünkü asenkron bir fonksiyonu yok.
            return entityEntry.State == EntityState.Deleted;
        }

        public bool RemoveRange(List<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }

        public async Task<bool> RemoveAsync(string id) //Asynron bir yapılanma varsa task dönmek zorunda 
        {
            T model = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id)); //Burada silinecek datayı bulmuş olduk.
            return Remove(model);
        }

        public bool UpdateAsync(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public bool Update(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync() //Son olarakta savechanges yapmamız gerekiyor.
            => await _context.SaveChangesAsync();
    }
}
