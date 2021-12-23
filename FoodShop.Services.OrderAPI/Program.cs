using AutoMapper;
using FoodShop.Services.OrderAPI;
using FoodShop.Services.OrderAPI.DBContext;
using FoodShop.Services.OrderAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers();
//SD.AzureBusConnection = configuration["ConnectionStrings:AzureBus"];
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//builder.Services.AddSingleton<IMessageBus, AzureServiceBusMessageBus>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Dev")));
builder.Services.AddMvc(options => options.ModelValidatorProviders.Clear());
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
        options.RequireClaim("scope", "mango");
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
