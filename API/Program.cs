using API.DataAccess;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text;
using API.models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("LibraryOpenAPISpecification", new()
    {
        Title = "Mercado Libro API",
        Version = "1"
    });

    setupAction.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard JWT Authorization header. Example: \"bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    setupAction.OperationFilter<SecurityRequirementsOperationFilter>();



    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml ";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setupAction.IncludeXmlComments(xmlCommentsFullPath);
});

builder.Services.AddDbContext<BookCartContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddTransient<IOrderService, OrderDataAccessLayer>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IWishlistService, WishlistDataAccessLayer>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Identity
//builder.Services.AddDbContext<AppIdentityDbContext>(options =>
//{
//    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection"));
//});

//builder.Services.AddIdentity<AppUser, AppRole>(options =>
//    {
//        options.Password.RequiredLength = 5;
//        options.Password.RequireNonAlphanumeric = false;
//        options.Password.RequireUppercase = false;
//        options.Password.RequireLowercase = true;
//        options.Password.RequireDigit = false;
//        options.Password.RequiredUniqueChars = 3;
//    })
//    .AddEntityFrameworkStores<AppIdentityDbContext>()
//    .AddUserStore<UserStore<AppUser, AppRole, AppIdentityDbContext,int>>()
//    .AddRoleStore<RoleStore<AppRole, AppIdentityDbContext,int>>();

// JWT
//builder.Services.AddAuthentication(options =>
//    {
//        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    })
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters()
//        {
//            ValidateAudience = true,
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            ValidateIssuer = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//        };
//    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
            ClockSkew = TimeSpan.Zero // Override the default clock skew of 5 mins
        };

    });



builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(UserRoles.Admin, Policies.AdminPolicy());
    config.AddPolicy(UserRoles.User, Policies.UserPolicy());
});


builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
       policy =>
         {
            policy.WithOrigins("https://mercadolibro.vercel.app",
               "http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
         });
});

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction =>
    {
        setupAction.SwaggerEndpoint("/swagger/LibraryOpenAPISpecification/swagger.json", "MercadoLibroAPI");
        setupAction.RoutePrefix = string.Empty;
    });
}

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var logger = services.GetRequiredService<ILogger<Program>>();

app.UseStaticFiles();

 
app.UseCors("CorsPolicy");



app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();




app.Run();
