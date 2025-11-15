using System.Text.Json;
using System.Text.Json.Serialization;
using CrossDSP.Infrastructure.Authentication.Google;
using CrossDSP.Infrastructure.Authentication.Spotify;
using CrossDSP.Infrastructure.ServiceDependencyInjection;
using CrossDSP.WEBAPI.ServiceDependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureDependencies();
builder.Services.AddGoogleServices(builder.Configuration);
builder.Services.AddSpotifyServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication()
    .AddGoogleAuthentication()
    .AddSpotifyAuthentication();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(GoogleOAuth2Defaults.HasGoogleAccessTokenPolicy, p =>
    {
        p.RequireClaim(GoogleOAuth2Defaults.GoogleOAuth2AccessTokenClaim);
    }
);

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(SpotifyOAuthDefaults.SpotifyUserPolicy, p =>
    {
        p.RequireClaim(SpotifyOAuthDefaults.SpotifyAccessTokenClaimKey);
        p.RequireClaim(SpotifyOAuthDefaults.SpotifyUserEntityIdClaimKey);
    }
);

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
