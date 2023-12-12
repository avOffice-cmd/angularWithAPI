using Application;
using Application.DTOs.CourseDTOs;
using Application.DTOs.StudentDTOs;
using Application.Validators;
using FluentValidation;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);



Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
      //.WriteTo.Console()
      .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Minute)
      .CreateLogger();

builder.Logging.AddSerilog();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// registering DI for AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Register Validator 
builder.Services.AddScoped<IValidator<AddStudentDTO>, StudentValidator>();


// Creating the context object
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(
        builder.
        Configuration.GetConnectionString("homeConnString"),
        builder => builder.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)),
        ServiceLifetime.Transient
    );


// Dependency injection for the context object
builder.Services.AddTransient<IApplicationDBContext>(
    provider => provider.GetRequiredService<ApplicationDBContext>()
);

// service for type 'MediatR.ISender' has been registered.
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
