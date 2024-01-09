using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories //Not: Repositories içerisine her bir entity için klasör oluşturduk ama entitylerle aynı ismi verdiğimiz için aşağıda Parametreleri veremedik. Bunun nedeni namespace ile çakışıyor olması. O yüzden buradaki .Customer namespace'i sildik.
{
    public interface ICustomerReadRepository : IReadRepository<Customer> //Artık her entity'nin interface'ini oluşturduk ve bunları IReadRepository'den türettik. Artık burada her bir entity için kullanacağımızdan generic bir yapı yerine doğrudan içine parametrik olarak entity ismi verebiliyoruz.
    {
    }
}
