using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class Order :BaseEntity
    {
        public int CustomerId { get; set; } //Bu yapılanmada aşağıdaki customer'a ait bir Id kolonu koyulabilir. Eğer koyulmazsa Entityframework kendisi otomatik olarak koyar. Kendimiz yönetmek istiyorsak bu şekilde yazabiliriz. Bunun anlamı customer ile ilişkilendirmek için bir id kolonu gerekmesinden kaynaklı.
        public string Description { get; set; }
        public string Address { get; set; }
        public ICollection<Product> Products { get; set; } //Order ile product arasında çoka çok ilişki var yani örneğin iki ayrı ürünü aynı anda sipariş verebiliyoruz. Birden fazla ürün tek siparişte olabiliyor. Tam tersi durumda da benzer ürünleri başkaları da verebilir yani bir ürün birden fazla siparişte bulunabiliyor. Bu nedenle aralarında çoka çok ilişki var diyebiliriz.
                                                           //Biz bu durumda order ile product atasında bir ilişki kurmamız gerekiyor. Bire çok ilişki üzerinden örnekleyecek olursak çoktan 1'e bir collection referansı veriliyor yani bir property ile ICollection referansı veriliyor. Çoka çok ilişkide de benzer şekilde hem order tarafına hem de product tarafına property ile bu ICollection referansları verilir. Burada verilen referansın anlamı bir order'ın birden fazla product'ı olduğunu ifade ediyor. Sadece burada yapıp product içerisinde referans tanımlamazsak bu bire çok ilişki var anlamına gelir.

        public Customer Customer { get; set; } //Order olarak sadece bir tane customer'ı olabileceği için burada customer tekil olarak tanımlanır. İsmi de tekil olarak verilir. Entityler arasındaki ilişki görselinde daha açıklayıcı.
    }
}
