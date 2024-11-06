using Infraestructura;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

////////////////////////////////////////////////
///connection
builder.Services.AddControllers();
var connection = builder.Configuration["ConnectionStrings:BaseDatos"];
ConexionDB.CadenaConexion = connection;
builder.Services.AddDbContext<Conexion>(
    options =>
    {
        options.UseLazyLoadingProxies();
        options.UseSqlServer(connection);
    });

////////////////////////////////////////////////

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
