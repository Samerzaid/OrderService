using Microsoft.EntityFrameworkCore;
using OrderService;
using OrderService.Notifications;
using OrderService.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("OrderDB");

builder.Services.AddDbContext<OrderDbContext>(
    options => options.UseSqlServer(connectionString)
);
// Add services to the container
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddSingleton<OrderSubject>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<INotificationObserver, EmailNotification>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    dbContext.Database.Migrate();
}

using (var scope = app.Services.CreateScope())
{
    var subject = scope.ServiceProvider.GetRequiredService<OrderSubject>();
    var emailNotification = scope.ServiceProvider.GetRequiredService<INotificationObserver>();

    // Register the email notification observer
    subject.Attach(emailNotification);
    Console.WriteLine("EmailNotification observer attached.");
}

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();