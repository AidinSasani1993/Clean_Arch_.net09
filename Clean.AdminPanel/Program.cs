using Clean.Application.UseCase.Queries.Categories;
using Clean.Common.Extentions;
using Clean.Dapper.DapperDatabaseContext;
using Clean.Domain.Entities.Users;
using Clean.EntityFrameworkCore.DataBaseContext;
using Clean.Repository.Categories;
using Clean.Service.Categories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddRegisterRepository(typeof(CategoryRepository).Assembly);
builder.Services.AddRegisterService(typeof(CategoryService).Assembly);
builder.Services.AddScoped<CleanDbContext>();
builder.Services.AddScoped<DapperContext>();

builder.Services.AddDbContext<CleanDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Clean")));

builder.Services.AddMediatR
    (cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetCategoryHandler).Assembly));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<CleanDbContext>() 
    .AddDefaultTokenProviders();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "This is my .NET 9 API with Swagger",
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
