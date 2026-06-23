using Clean.Application.UseCase.Queries.Categories;
using Clean.Common.Exceptions;
using Clean.Common.Extentions;
using Clean.Dapper.DapperDatabaseContext;
using Clean.EntityFrameworkCore.DataBaseContext;
using Clean.Repository.Categories;
using Clean.Service.Categories;
using Clean.Service.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddRegisterRepository(typeof(CategoryRepository).Assembly);
builder.Services.AddRegisterService(typeof(CategoryService).Assembly);
builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<IAuthorizationHandler, ActiveUserHandler>();
//builder.Services.AddScoped<CleanDbContext>();

builder.Services.AddDbContext<CleanDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CleanDb")));

builder.Services.AddMediatR
    (cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetCategoryHandler).Assembly));

//jwt
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Custom Identity API",
        Version = "v1",
        Description = "Custom Identity with JWT Authentication (ASP.NET Core 9)"
    });

    // JWT Definition
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "True Format:\n\nBearer {token}"
    });

    // JWT Requirement
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthorization(policy =>
{
    policy.AddPolicy("AdminCustom", a =>
    {
        //a.Requirements.Add(new UserRequirement { RoleName = "Admin" });
        a.RequireRole(["Admin","SupperAdmin","SupperEmployee"]);
    });
});

//builder.Services.AddRequestTimeouts(options => {
//    options.DefaultPolicy =
//        new RequestTimeoutPolicy { Timeout = TimeSpan.FromMilliseconds(1500) };
//    options.AddPolicy("MyPolicy", TimeSpan.FromSeconds(2));
//});

builder.Services.AddRequestTimeouts(options =>
{
    options.DefaultPolicy = new RequestTimeoutPolicy
    {
        Timeout = TimeSpan.FromSeconds(2),
        TimeoutStatusCode = StatusCodes.Status503ServiceUnavailable
    };
});


//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(policy =>
//        policy.AllowAnyOrigin()
//              .AllowAnyHeader()
//              .AllowAnyMethod());
//});

var connectionString = builder.Configuration.GetConnectionString("CleanDb");

var columnOptions = new ColumnOptions
{
    AdditionalColumns = new Collection<SqlColumn>
    {
        new SqlColumn { ColumnName = "SourceContext", DataType = SqlDbType.NVarChar, DataLength = 512 },
    }
};

columnOptions.Store.Remove(StandardColumn.Properties);
columnOptions.Store.Add(StandardColumn.LogEvent); 

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true 
        },
        columnOptions: columnOptions
    )
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddRateLimiter(options =>
{
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.ContentType = "application/json";

        await context.HttpContext.Response.WriteAsJsonAsync(new
        {
            Message = ErrorMessage.RateLimitingError
        }, token);
    };

    options.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.PermitLimit = 3;
        opt.Window = TimeSpan.FromSeconds(10);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 0;
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

app.MapGet("/test-timeout", async (CancellationToken ct) =>
{
    await Task.Delay(10000, ct);
    return Results.Ok("Done");
})
.WithRequestTimeout(TimeSpan.FromSeconds(2));

app.Use(async (context, next) =>
{
    context.RequestAborted.Register(() =>
    {
        Console.WriteLine("REQUEST ABORTED");
    });

    await next();
});

app.UseRateLimiter();

app.UseRequestTimeouts();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
