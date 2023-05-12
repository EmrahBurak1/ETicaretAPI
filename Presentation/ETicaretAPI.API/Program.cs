using ETicaretAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

//Uygulama ayaða kalktýðýnda IOC container ayaða kalkacak. Bu IOC container persistence içerisinde ServiceRegistration içerisinde bulunan AddPersistenceServices methodu ile applicationdaki IProductService'e karþýlýk persistencetaki ProductService'i ekleyecektir. 
builder.Services.AddPersistenceServices(); //Bunu biz ekledik. IOC Container ile haberleþebilmek için. Persistence içerisine ServiceRegistration sýnýfý ekledik.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
