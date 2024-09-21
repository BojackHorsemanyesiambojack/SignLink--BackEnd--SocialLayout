using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SignLinkAPI.Context;
using SignLinkAPI.Services;
using SignLinkAPI.Utils;

var builder = WebApplication.CreateBuilder(args);
Env.Load();
string ConnectionString = DbConstructor.GetStringConnection(
    Environment.GetEnvironmentVariable("DBUSERLAYOUT_HOST"),
    Environment.GetEnvironmentVariable("DBUSERLAYOUT_PASS"),
    Environment.GetEnvironmentVariable("DBUSERLAYOUT_OWN"),
    Convert.ToInt32(Environment.GetEnvironmentVariable("DBUSERLAYOUT_PORT")),
    Environment.GetEnvironmentVariable("DBUSERLAYOUT_NAME")
    );
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
builder.Services.AddScoped<Auth>();
builder.Services.AddDbContext<UserLayoutDb>(options => options.UseNpgsql(ConnectionString));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(("AllowAll"));
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();