using LogisticaEnvios.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//clientes
builder.Services.AddDbContext<ClientesContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LogisticaCS")));
//tipo de producto
builder.Services.AddDbContext<TipoProductoContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LogisticaCS")));
//Bodega
builder.Services.AddDbContext<BodegaContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LogisticaCS")));
//Puerto
builder.Services.AddDbContext<PuertoContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LogisticaCS")));
//Plan de entrega
builder.Services.AddDbContext<PlanDeEntregaContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LogisticaCS")));
//Envio terrestre
builder.Services.AddDbContext<EnvioTerrestreContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LogisticaCS")));
//Envio maritimo
builder.Services.AddDbContext<EnvioMaritimoContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LogisticaCS")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS seguro: solo permitir el frontend autorizado
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:8000") // dominio del frontend
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("FrontendPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();