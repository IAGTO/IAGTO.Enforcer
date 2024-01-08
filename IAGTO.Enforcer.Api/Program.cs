using IAGTO.Enforcer.Api;

using Microsoft.Extensions.DependencyInjection;

using Rsk.Enforcer;
using Rsk.Enforcer.Remote.Hosting;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var token = builder.Configuration.GetValue<string>("Enforcer:Token");

builder.Services.AddHttpClient<HttpUserClient>(config =>
{
    config.BaseAddress = new Uri("https://iagto-api.dev.iagto.net/membership/");

    config.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
});

builder.Services.AddEnforcer(builder.Configuration.GetValue<string>("Enforcer:Licensee")!, 
    builder.Configuration.GetValue<string>("Enforcer:LicenseKey")!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseEnforcerRemoteHosting();

app.MapControllers();

app.Run();
