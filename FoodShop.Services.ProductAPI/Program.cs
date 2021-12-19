using AutoMapper;
using FoodShop.Services.ProductAPI;
using FoodShop.Services.ProductAPI.DBContext;
using FoodShop.Services.ProductAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Dev")));
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.Authority = "https://localhost:44345";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false
    };

});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", options =>
    {
        options.RequireAuthenticatedUser();
        options.RequireClaim("scope", "FoodShop");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
