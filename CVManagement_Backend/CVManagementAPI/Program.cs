using CvManagement.Extention;
using CVManagementAPI.Models;
using CVManagementAPI.Repository;
using CVManagementAPI.Services;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection");


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var jwtConfig = builder.Configuration.GetSection("JwtConfig");
var secretKeyString = jwtConfig["SecretKey"];
var issuer = jwtConfig["Issuer"];
var audience = jwtConfig["Audience"];
var tokenExpiryInDays = Convert.ToDouble(jwtConfig["TokenExpiryInDays"]);


builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyString)),
            ClockSkew = TimeSpan.Zero
        };
    });


builder.Services.AddSwaggerGen(option =>
{
    //option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    //{
    //    In = ParameterLocation.Header,
    //    Name = "Authorization",
    //    Type = SecuritySchemeType.ApiKey,
    //});
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
    //   option.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddDbContext<CVManagementDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("CVManagementDb")));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<AppUser>().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CVManagementDbContext>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
//builder.Services.AddScoped(typeof(ColumnRelationshipRepository));
builder.Services.AddScoped<ServiceFactory>();
builder.Services.AddScoped<RepositoryFactory>();
//builder.Services.AddScoped<ICacheService>(option => new CacheService(redisConnectionString)); builder.Services.AddScoped<CVService>();
// builder.Services.AddScoped<CVService>();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

//hangfire
builder.Services.AddHangfire((sp, config) =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("CVManagementDb"));
});
builder.Services.AddHangfireServer();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredUniqueChars = 0;
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await SeedData.InitializeAsync(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseCors();


app.MapIdentityApi<AppUser>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.UseHangfireDashboard("/test/job-dashboard", new DashboardOptions
{
    DashboardTitle = "Hangfire Job Demo Application",
    DarkModeEnabled = false
});

app.ConfigureCustomExceptionMiddleware();

app.Run();
