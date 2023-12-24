using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence
{
    static class Configuration
    {
        //Örneğin persistence içerisinde birkaç yerde connectionString kullanmak isteyelim bunun için her sınıfa ayrı ayrı aşağıdaki tanımlamaları yapmamız gerekir. Bunun yerine biz persistence projesi içerisinde Configuration sınıfı oluşturup tanımlamaları burada yapabiliriz.
        
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new(); //Connectionstring'i kod içerisine eklemektense appsettings.json dosyası içine ekledik. Burada kullanabilmek için de ConfigurationManager sınıfı kullanmak gerekiyor. Bunun için de Microsoft.Extensions.Configuration kütüphanesi kurulup burada kullanılır.
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ETicaretAPI.API")); //Burada da json dosyası presentation altında olduğu için dosya yolunu vermemiz gerekiyor. Şu an persistence içindeyiz iki kere önceki klasöre gelirsek ../ ile solution içine girmiş oluruz daha sonra Presentation ile ilgili klasöre gitmiş oluruz.
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("PostgreSQL");
            }
        }
    }
}
