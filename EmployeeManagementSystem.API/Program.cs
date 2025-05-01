using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Domain;
using System.Reflection;
using EmployeeManagementSystem.Repository;
using EmployeeManagementSystem.API.Helpers;
using EmployeeManagementSystem.Common.UnitOfWork;
using EmployeeManagementSystem.Data.DTOs.AdminUser;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using DepartmentManagementSystem.Repository;
using FluentValidation;
using EmployeeManagementSystem.MediatR.PipeLineBehavior;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

//Default user tanımladım, ilk başta bu gerekli, oturum açma işlemleri tamamlandığında kaldırılabilir
var defaultUserId = builder.Configuration["DefaultUser:DefaultUserId"];
builder.Services.AddScoped(c => new AdminUserDto() { Id = defaultUserId });

// Connection string'i al
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext'i ekle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// MediatR yapılanması
var assembly = AppDomain.CurrentDomain.Load("EmployeeManagementSystem.MediatR");
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(assembly);
});

//Validation ayarları - Burda DependencyInjection'dan yararlanarak tüm validasyon kurallarını tek bir yerden yönetebiliyorum. her biri için ayrı ayrı ayar yapmamıza gerek kalmıyor
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssemblies(Enumerable.Repeat(assembly, 1));

// Repository ve diğer servisler
builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// JWT Bearer Token Kodları
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });

//Controller'larda yazdığımız açıklama metninin swaggerda gözükmesini sağlayan kod parçası
builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    c.SwaggerDoc("v1", new() { Title = "Your API", Version = "v1" });
    // 🔐 Bearer Token ayarları
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer 12345abcdef'"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



// Serilog'u kullanmaya başla
builder.Host.UseSerilog();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));


// CORS politikası tanımla
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("https://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


var app = builder.Build();

// CORS'u middleware olarak ekle
app.UseCors("AllowAngularApp");

// Middleware Serilog için eklendi
app.UseSerilogRequestLogging();

// Uygulama başlarken veritabanını otomatik oluştur
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Serilog yapılandırması
Serilog.Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true },
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
    .CreateLogger();


app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
