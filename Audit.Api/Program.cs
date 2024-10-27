using Audit.Api.EndPoints;
using Audit.Api.Extensions;
using Audit.Application.Handlers.QueryHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureJsonOptions();
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddRabbitMqMassTransit(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetVersionQueryHandler).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAliveEndpoints();
app.MapDbContentChangesEndpoints();

app.UseHttpsRedirection();

app.Run();
