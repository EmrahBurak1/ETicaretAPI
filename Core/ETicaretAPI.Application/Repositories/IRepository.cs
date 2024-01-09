using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    public interface IRepository<T> where T: BaseEntity //Burada T yazmamızın nedeni generic bir yapı kurmak istememiz. Bu sayede ne verirsek içerde onun dbsetini oluşturabilir orderi product gibi. where T: class yapısında da bu T nin class olduğunu belirtmek gerekiyor çünkü burası struct olabilir, enum olabilir bunun gibi farklı şeyler olabilir. Bizim bunun sınırlarını belirlememiz gerekiyor.
    {
        DbSet<T> Table { get; }
    }
}
