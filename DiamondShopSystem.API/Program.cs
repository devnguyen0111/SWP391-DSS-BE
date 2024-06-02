using DAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Services;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DIAMOND_DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDiamondRepository, DiamondRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();


builder.Services.AddScoped<ISizeRepository, SizeRepository>();
builder.Services.AddScoped<IMetaltypeRepository, MetaltypeRepository>();
builder.Services.AddScoped<ICoverSizeRepository, CoverSizeRepository>();
builder.Services.AddScoped<ICoverMetaltypeRepository, CoverMetaltypeRepository>();
builder.Services.AddScoped<ICoverRepository, CoverRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IDiamondService, DiamondService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();


builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<IMetaltypeService, MetaltypeService>();
builder.Services.AddScoped<ICoverSizeService, CoverSizeService>();
builder.Services.AddScoped<ICoverService, CoverService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<CalculatorService ,CalculatorService>();
builder.Services.AddScoped<IVnPayRepository, VnPayRepository>();
builder.Services.AddScoped<IStripeRepository, StripeRepository>();
builder.Services.AddScoped<Ivnpay, VnPay>();
builder.Services.AddScoped<IStripeService, StripeService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Diamond Shop System API", Version = "v1" });

    // Add security definition for Bearer tokens
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
//cors test
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder
            .AllowAnyOrigin() // Specify the frontend URL
            .AllowAnyHeader()
            .AllowAnyMethod());
    //.AllowCredentials()); // Allow credentials if necessary
            //.AllowCredentials()); // Allow credentials if necessary

});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.
/*app.UseCors("AllowSpecificOrigin");*/
app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
