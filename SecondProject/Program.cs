using Domain.Additional;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
void  ConfigureServices(IServiceCollection services)
{
    app.UseHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";
            var response = new HealthCheckResponse
            {
                Status = report.Status.ToString(),
                HealthChecks = report.Entries.Select(x => new IndividualHealthCheckResponse
                {
                    Component = x.Key,
                    Status = x.Value.Status.ToString(),
                    Descriptino = x.Value.Description


                }),
                HealthChechDuration = report.TotalDuration
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    });

    app.UseHealthChecks("/healthUI", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    });
}
app.UseResponseCaching();

app.Run();
