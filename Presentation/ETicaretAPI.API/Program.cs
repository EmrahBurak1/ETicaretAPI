using ETicaretAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

//Uygulama aya�a kalkt���nda IOC container aya�a kalkacak. Bu IOC container persistence i�erisinde ServiceRegistration i�erisinde bulunan AddPersistenceServices methodu ile applicationdaki IProductService'e kar��l�k persistencetaki ProductService'i ekleyecektir. 
builder.Services.AddPersistenceServices(); //Bunu biz ekledik. IOC Container ile haberle�ebilmek i�in. Persistence i�erisine ServiceRegistration s�n�f� ekledik.

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
