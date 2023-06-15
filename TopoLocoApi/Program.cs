using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TopoLocoApi.Models;
using TopoLocoApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();

builder.Services.AddDbContext<Sistem21TopolocoContext>(c => c.UseMySql("server=sistemas19.com;database=sistem21_topoloco;user=sistem21_topoloco;password=sistemas19_", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb")));

var app = builder.Build();

app.UseRouting();

app.MapControllers();
app.MapHub<TopoLocoHub>("/topolocoHub");
app.Run();
