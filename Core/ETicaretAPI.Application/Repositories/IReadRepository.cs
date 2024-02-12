using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity //Burada da IReadRepository IRepository den türüyor ve IRepository T tipinde olduğu için IReadRepository de T tipinde tanımlamamız gerekiyor. Çünkü yine generic bir yapı kurmak istiyoruz. Ve bunun class olduğunu bildiriyoruz. Neden BaseEntity den türettiğimiz ReadRepository içindeki GetByIdAsync methodun açıklamasında yazıyor.
    {
        //Buradaki işlemler ORM de yapılabiliyor ama biz bu şekilde customize edebiliyoruz. 

        //Eğer sorgu üzerinde çalışmak istiyorsak IQueryable eğer in memory'de çalışmak istiyorsak enumarable kullanılır.
        //IQueryable da bizim yazmış olduğunumuz where şartları ya da select sorguları ilgili veritabanı sorgusuna eklenir.

        //Tracking performans optimizasyonu kapsamında tüm get methodlarına tracking ekledik. Tracking ile veritabanı işlemlerinin takibi yapılabiliyor. Bunun için de get methodlarına tracking parametreleri oluşturuluyor. Tracking true ise data varsayılan olarak  track edilerek gelsin anlamına geliyor. False ise track edilmeden datalar getirilsin anlamına gelir.
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true); //Örneğin burasını where şartı olarak kullanabiliriz. Yani GetWhere methodunu çağırırken içine istediğimiz koşulları yazabiliriz bunu da expression ile yapıyoruz. Func'ın anlamı özel tanımlı fonksiyona verilen şart ifadesi doğru olan (bool tanımladık) datalar sorgulanıp getirilcek anlamına gelir. En sonra yazan ise alias oluyor. method yerine farklı bir isim de yazabilirdik.
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);//Vermiş olduğumuz şarta uygun tekil sorgu oluşturmak istiyorsak T ile tanımlarız. Yani çoğunluk ifade eden iqueryable falan kullanılmaz. Şarta uygun olan ilk datayı getirir. Task olarak geri döneceğini belirtiyoruz. Ayrıca asenkron çalışacağı için name convention olarak async ifadesini de ismine ekliyoruz.
        Task<T> GetByIdAsync(string id, bool tracking = true); //Burada da verilen id li kayıt getirmek için kullanılır.
    }
}
