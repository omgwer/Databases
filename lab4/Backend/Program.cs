using Entity;
using Entity.Service;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Service;
using Service;
using Service.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<CourseDbContext>( t =>
{
   // t.UseNpgsql( builder.Configuration.GetConnectionString( "DefaultConnection" ), b => b.MigrationsAssembly("Infrastructure") );
    t.UseNpgsql( builder.Configuration.GetConnectionString( "DefaultConnection"),  b => b.MigrationsAssembly("Infrastructure"));
} );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Тут добавить сервис в DI
builder.Services.AddScoped<DbContext, CourseDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICourseEnrollmentRepository, CourseEnrollmentRepository>();
builder.Services.AddScoped<ICourseModuleRepository, CourseModuleRepository>();
builder.Services.AddScoped<ICourseModuleStatusRepository, CourseModuleStatusRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseStatusRepository, CourseStatusRepository>();
builder.Services.AddScoped<ICourseEnrollmentService, CourseEnrollmentService>();
builder.Services.AddScoped<ICourseModuleService, CourseModuleService>();
builder.Services.AddScoped<ICourseModuleStatusService, CourseModuleStatusService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseStatusService, CourseStatusService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();