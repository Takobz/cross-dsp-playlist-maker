using System.Text.Json;
using CrossDSP.Infrastructure.Authentication.Google;
using CrossDSP.Infrastructure.ServiceDependencyInjection;
using CrossDSP.WEBAPI.ServiceDependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGoogleServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication()
    .AddGoogleAuthentication();

builder.Services.AddAuthorization(authROptions =>
{
    /*
    * Don't really need this but make authorization granular and I love it
    */
    authROptions.AddPolicy(GoogleOAuth2Defaults.HasGoogleAccessTokenPolicy, p =>
    {
        p.RequireClaim(GoogleOAuth2Defaults.GoogleOAuth2AccessTokenClaim);
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
