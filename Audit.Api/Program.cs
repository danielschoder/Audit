using Audit.Api.EndPoints;
using Audit.Api.Extensions;
using Audit.Application.Handlers.QueryHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureJsonOptions();
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddRabbitMqMassTransit(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetVersion).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCorsPolicy();

var app = builder.Build();

app.UseCors("AllowAllOrigins");
app.MapAliveEndpoints();
app.MapDbContentChangesEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
