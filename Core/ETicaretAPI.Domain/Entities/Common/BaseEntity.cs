using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; } //Id yi guid veri tipinde tanımayabiliriz. Bu unique identify anlamına geliyor. Int olarakta kalabilir.

        public DateTime CreatedDate { get; set; }
    }
}
