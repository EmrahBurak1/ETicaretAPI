using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    //Burada da artık interfacelerin concretelerini oluşturuyoruz.
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository //Burada artık ICustomerReadRepository implemente edilmez. Öyle yapacaksak her şeyi repositpyler içinde yapmış oluruz o zaman bir mantığı olmaz. Zaten bizim ReadRepository diye bir sınıfımız var bunun içerisine hangi türden olduğunu belitrmemiz yeterli olacaktır. Bu sayede ReadRepository içerisinde ne kadar method varsa hepsi uygulanmış olacak. ICustomerReadRepository eklememizin nedeni de ICustomerReadRepository concrete nesnesinin abstractşınıdır yani soyut yapılanmasıdır. Yani biz dependenc injectiondan CustomerWriteRepository'yi talep ederken ICustomerReadRepository ile talep edicez.
    {
        public CustomerReadRepository(ETicaretAPIDbContext context) : base(context) //ReadRepository bizden context istiyor bu contexti de constructor oluşturulup onun içinde verilir.
        {
        }
    }
}
