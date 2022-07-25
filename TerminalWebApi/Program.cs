using Microsoft.EntityFrameworkCore;
using TerminalWebApi.API;
using Terminal.Interfaces;
using Terminal;
using TerminalDB;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
builder.Services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
builder.Services.AddScoped(typeof(ITerminal), typeof(Terminal.Terminal));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
ProductsApi.ConfigureAPI(app);
TerminalAPI.ConfigureAPI(app);

app.Run();