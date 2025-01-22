using PokemonPc.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database MongoDB Configuration
builder.Services.ConfigureMongoDB(builder.Configuration);

builder.Services.AddAplicationServices();

// Configurar os Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usa HTTPS
// app.UseHttpsRedirection();

// Configurar as rotas e os controllers
app.UseRouting();
app.MapControllers();

app.Run();
