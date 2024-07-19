using DAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Services.Diamonds;
using Services.Charge;
using Services.Products;
using Services.Users;
using Services.Utility;
using System.Text;
using Repository.Products;
using Repository.Users;
using Repository.Diamonds;
using Repository.Charge;
using Repository.Utility;
using Services.EmailServices;
using Services.OtherServices;
using Serilog;
using Repository.Shippings;
using Services.ShippingService;
using Services.Admin;
using Microsoft.AspNetCore.Authorization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<DIAMOND_DBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .EnableSensitiveDataLogging()); //allow seeing sensitive information in logging

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDiamondRepository, DiamondRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ISizeRepository, SizeRepository>();
builder.Services.AddScoped<IMetaltypeRepository, MetaltypeRepository>();
builder.Services.AddScoped<ICoverSizeRepository, CoverSizeRepository>();
builder.Services.AddScoped<ICoverMetaltypeRepository, CoverMetaltypeRepository>();
builder.Services.AddScoped<ICoverRepository, CoverRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IVnPayRepository, VnPayRepository>();
builder.Services.AddScoped<IPaypalRepository, PaypalRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ISaleStaffRepository, SaleStaffRepository>();
builder.Services.AddScoped<IDeliveryStaffRepository, DeliveryStaffRepository>();
builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<IShippingRepository, ShippingRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<ICoverInventoryRepository, CoverInventoryRepository>();
//builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();

builder.Services.AddScoped<ICoverMetaltypeService, CoverMetaltypeService>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IDiamondService, DiamondService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<IMetaltypeService, MetaltypeService>();
builder.Services.AddScoped<ICoverSizeService, CoverSizeService>();
builder.Services.AddScoped<ICoverService, CoverService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailRelatedService, EmailRelatedService>();
builder.Services.AddScoped<ITOSService, TOSService>();
builder.Services.AddScoped<CalculatorService ,CalculatorService>();
builder.Services.AddScoped<Ivnpay, VnPay>();
builder.Services.AddScoped<IPaypalService, PaypalService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IDisableService, DisableService>();
builder.Services.AddScoped<ICoverInventoryService, CoverInventoryService>();

builder.Services.AddScoped<IShippingService, ShippingService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IAdminService, AdminService>();
//builder.Services.AddScoped<IWishlistService, WishlistService>();


//Custom policy

builder.Services.AddSingleton<IAuthorizationHandler, CustomJwtHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomJwtPolicy", policy =>
        policy.Requirements.Add(new CustomJwtRequirement()));
});
//this is logger using Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);

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
//Add session for product customize
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.
/*app.UseCors("AllowSpecificOrigin");*/

app.UseSerilogRequestLogging();

app.UseSession();

app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
