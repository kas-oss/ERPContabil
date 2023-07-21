using DFe.Settings;
using DFe.RegistersExtensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Configuração do Consul
var serviceSettings = configuration.GetSection("ServiceSettings").Get<ServiceSettings>();

builder.Services.AddConsulSettings(serviceSettings);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

app.UseConsul(serviceSettings);

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run("http://localhost:5001");