
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints();
builder.Services.AddUseCases();
builder.Services.AddNotifications(builder.Configuration);

builder.Services.AddRepositories();
builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddConfigurations(builder.Configuration);
builder.Services.AddConsumers(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.SwaggerDoc("v1", new()
{
    Title = "Rent-Motorcycle-API",
    Description = "API to control customers rent"    
}));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();