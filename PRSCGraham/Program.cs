using Microsoft.EntityFrameworkCore;
using PRSCGraham.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler =
      System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<PrsDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("PrsConnectionString"))
    );
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();// allows access to files like pics and HTML Defaults to wwwroot

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
