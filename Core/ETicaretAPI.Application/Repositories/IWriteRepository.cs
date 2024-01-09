using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        //Ekleme, silme operasyonları vs. burada yapılır.

        Task<bool> AddAsync(T model); //Ekleme işlemi için kullanılır. Gelen model ne ise onun eklenmesi için kullanılır. Bool olma nedeni eklediysek sonucu true ya da false dönücez bu nedenle bool yapılır.

        Task<bool> AddRangeAsync(List<T> datas);//Birden fazla veri eklemek istersek örneğin bir koleksiyon geldi onu da bu şekilde list olarak ekliyoruz. model yerine birden fazla data geldiği için datas yazabiliriz.

        bool Remove(T model); //Veri silme işlemi için bunu kullanabiliriz.
        bool RemoveRange(List<T> datas); //Burada da çoklu silme işlemini yapabiliriz.
        Task<bool> RemoveAsync(string id); //İd ile de veriyi bu şekilde silebiliriz.

        bool Update(T model); //Update fonksiyonu asenkron olmadığı için task olarak tanımlamamıza gerek yok.

        Task<int> SaveAsync(); //Yapılan işlemler sonucunda savechanges'ı kullanabilmemiz için bu fonksiyonu kullanıcaz.
    }
}
