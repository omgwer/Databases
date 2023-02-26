using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddCors( options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins( "http://localhost:44435" )
                .AllowAnyHeader()
                .AllowAnyMethod();
        } );
} );

builder.Services.AddControllers().AddJsonOptions( options => options.JsonSerializerOptions.IncludeFields = true );

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

app.UseCors( builder =>
{
    builder
        .WithOrigins( "localhost:44435" )
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
} );

app.Run();