# ETicaretAPI
-Proje .Net Core 6 ve Angular 15 ile geliştirildi. 

-Mimari olarak onion architecture kullanıldı. Mimarinin detaylı anlatımı "Onion Architecture.png" içerisinde var.

-Solution içerisinde oluşturulan core katmanı ana, çekirdek katmandır. Domain ve Application (repository, interfaces,services) katmanlarını içerir.

-Proje içerisine katmanlar dahil edilirken gelen isimlendirme kuralı olarak önce solution ismi daha sonra nokta ile birlikte katmanın adı yazılır. Örneğin ETicaretAPI.Domain

-Katmanlar oluşturulurken presentation yani sunum katmanı dışındakiler Class library projesi olarak oluşturulur. Presentation katmanı da istenilen uygulamaya göre MVC, Web Application, API, Android vs gibi farklı projeler olabilir.

-Solution içerisine oluşturulan Infrastructure katmanı hem hem infrastructure hem de persistence katmanları için kullanılır. Gelen olarak servisler ile ilgili işlemler bu katmanda yapılır.

-Entitiyler domain katmanında oluşturulur. Eğer her bir entity de ortak kullanılacak propertyler varsa common adlı alt klasör oluşturulup bunun içerisine dahil edilebilir. Örneğin product entity si içerisinde id vs yi tanımlamak yerine oluşturduğumuz baseEntityden kalıtım alıyoruz.

-Oluşturduğumuz product entitysini diğer katmanlara soyutlayarak açmak için bir interface'e ihtiyacımız var bunu da Application katmanında yapıyoruz. Örneğin tüm productları getirecek bir fonksiyonumuz olacak bu fonksiyonun arayüzünü burada tanımlıyoruz.

-IProductService ismiyle oluşturduğumuz soyut katmanın bir de somutuna ihtiyacımız var. Applicationda oluşturulmuş soyutlanmış yapıların somutları da yani concretelerini yani bunları implemente eden sınıfları ya infrastructure ya da persistence'da oluşturucaz. Eğer veritabanıyla alakalıysa persistence, eğer dış servislerle alakalıysa infrastructure içerisinde yazılır.

-Product veritabanı ile alakalı olduğu için persistence katmanı içerisinde concrete sınıfı tanımladık ismiani de ProductService olarak verdik.
