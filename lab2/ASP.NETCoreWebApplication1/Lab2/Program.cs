using Lab2.DbWorker;
using Lab2.Model.Infrastructure.Data;
using Lab2.Model.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:44435")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.IncludeFields = true);

builder.Services.AddDbContext<StopOnTheRoadDbContext>(t =>
{
    t.UseNpgsql("Host=localhost; Database=Lab2; Username=postgres; Password=12345678; Port= 5432");
});

builder.Services.AddScoped<StopOnTheRoadService>();
builder.Services.AddScoped<RestrictionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.UseCors(builder =>
{
    builder
        .WithOrigins("localhost:44435")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.Run();