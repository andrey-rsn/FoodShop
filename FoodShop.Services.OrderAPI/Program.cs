using AutoMapper;
using FoodShop.Services.OrderAPI;
using FoodShop.Services.OrderAPI.DBContext;
using FoodShop.Services.OrderAPI.Extensions;
using FoodShop.Services.OrderAPI.Messaging;
using FoodShop.Services.OrderAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("Dev")));

SD.AzureBusConnection = configuration.GetConnectionString("AzureBus");
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionBuilder.UseSqlServer(configuration.GetConnectionString("Dev"));

//builder.Services.AddHostedService<RabbitMQPaymentConsumer>();
//services.AddHostedService<RabbitMQCheckoutConsumer>();
builder.Services.AddSingleton(new OrderRepository(optionBuilder.Options,mapper));
builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();
builder.Services.AddSingleton<IMessageBus, AzureServiceBusMessageBus>();

//IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Dev")));
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddSingleton(mapper);
//builder.Services.AddSingleton(configuration);
//builder.Services.AddScoped<IOrderRepository,OrderRepository>();
//
//
//var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//optionBuilder.UseSqlServer(configuration.GetConnectionString("Dev"));
//
//builder.Services.AddSingleton(new OrderRepository(optionBuilder.Options,mapper));
//builder.Services.AddSingleton<IAzureServiceBusConsumer,AzureServiceBusConsumer>();
builder.Services.AddControllers();
//SD.AzureBusConnection = configuration["ConnectionStrings:AzureBus"];




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddMvc(options => options.ModelValidatorProviders.Clear());




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
app.UseAzureServiceBusConsumer();
app.Run();
