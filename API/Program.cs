using API;
using API.Data;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc.GetConfiguration());

builder.Services.AddLoggerServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(
    options =>
        {
            options.UseNpgsql(builder.Configuration.GetValue<string>("DB_CONNECTION_STRING"));
        },
    ServiceLifetime.Transient);

builder.Services.AddScoped<IGostsService, GostsService>();
builder.Services.AddScoped<IIndexer, Indexer>();
builder.Services.AddScoped<ISearch, Search>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
