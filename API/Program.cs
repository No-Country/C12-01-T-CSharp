using API.DataAccess;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.Text;
using API.Identity;
using API.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
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

<<<<<<< HEAD
=======


builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(UserRoles.Admin, Policies.AdminPolicy());
    config.AddPolicy(UserRoles.User, Policies.UserPolicy());
});


>>>>>>> bd583be7027f76480ca5c4d74d8ee2d4028e7679
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

<<<<<<< HEAD
builder.Services.AddAuthorization();
=======
>>>>>>> bd583be7027f76480ca5c4d74d8ee2d4028e7679


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

app.UseStaticFiles();

<<<<<<< HEAD
 
app.UseCors("CorsPolicy");
=======

app.UseCors(MyAllowSpecificOrigins);
>>>>>>> bd583be7027f76480ca5c4d74d8ee2d4028e7679

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


//Create the database and run the migration at runtime
/*This code allows to get access to a Scope service inside our program class where we do not have the ability to inject a servce*/
//using var scope = app.Services.CreateScope();
//var services = scope.ServiceProvider;
//var logger = services.GetRequiredService<ILogger<Program>>();

//// Get services related to identity
//var identityContext = services.GetRequiredService<AppIdentityDbContext>();

//try
//{
//    //Seeding data to identity
//    await identityContext.Database.MigrateAsync();
//}
//catch (Exception ex)
//{
//    logger.LogError(ex, "An error occurred during migration");
//}

app.Run();
