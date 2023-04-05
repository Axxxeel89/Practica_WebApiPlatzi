using Practica_API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.UseCors(); //--> Este middleware nos permite poder configurar el quien puede usarla y quien no

app.UseAuthorization();

// app.UseWelcomePage(); //--> Nos permite agregar una pagina de Bienvenida apenas el usuario ingrese. 

app.UseTimeMiddleare();

app.MapControllers();

app.Run();
