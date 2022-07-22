using Microsoft.EntityFrameworkCore;
using TerminalWebApi.API;
using TerminalWebApi.DBLayer;
using Terminal.Interfaces;
using Terminal;


var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
builder.Services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
builder.Services.AddScoped(typeof(IPointOfSaleTerminal), typeof(PointOfSaleTerminal));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.MapGet("/test", (ApplicationContext db) => db.Products.ToList());

ProductsApi.ConfigureAPI(app);
TerminalAPI.ConfigureAPI(app);

app.Run();