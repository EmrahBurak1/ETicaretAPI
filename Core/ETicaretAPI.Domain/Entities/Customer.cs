using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        //Customer içerisinde 1'e çok ilişki var yani bir customer'ın birden fazla order'ı olabilir. Fakat bir order'ın birden fazla customer'ı olamaz. Entityler arasındaki ilişki görselinde daha açıklayıcı.
        public ICollection<Order> Orders { get; set; }
    }
}
