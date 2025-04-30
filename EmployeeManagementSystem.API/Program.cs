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
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Microsoft.Extensions.Hosting;
using DepartmentManagementSystem.Repository;
using EmployeeManagementSystem.Helper;

var builder = WebApplication.CreateBuilder(args);

// Connection string'i al
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Default user tanýmladým, ilk baþta bu gerekli, oturum açma iþlemleri tamamlandýðýnda kaldýrýlabilir
var defaultUserId = builder.Configuration["DefaultUser:DefaultUserId"];
builder.Services.AddScoped(c => new AdminUserDto() { Id = defaultUserId });

// DbContext'i ekle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// MediatR yapýlandýrmasý
var assembly = AppDomain.CurrentDomain.Load("EmployeeManagementSystem.MediatR");

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(assembly);
});

// Repository ve diðer servisler
builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// JWT Bearer Token Kodlarý
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

//Controller'larda yazdýðýmýz açýklama metninin swaggerda gözükmesini saðlayan kod parçasý
builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Serilog yapýlandýrmasý
Serilog.Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext() 
    .WriteTo.Console() 
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true },
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information) 
    .CreateLogger();

// Serilog'u kullanmaya baþla
builder.Host.UseSerilog();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));





// CORS politikasý tanýmla
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});






var app = builder.Build();


// CORS'u middleware olarak ekle
app.UseCors("AllowAngularApp");


// Middleware Serilog için eklendi
app.UseSerilogRequestLogging();

// Uygulama baþlarken veritabanýný otomatik oluþtur
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
