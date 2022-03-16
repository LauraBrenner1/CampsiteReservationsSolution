global using CampsiteReservationsApi.Services;
using CampsiteReservationsApi;
using CampsiteReservationsApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

var audience = builder.Configuration.GetValue<string>("audience");
var authority = builder.Configuration.GetValue<string>("authority");
builder.Services.AddAuthForKeycloak(authority, audience, builder.Environment);
// Typed HttpClient 
builder.Services.AddHttpClient<OnCallApiService>( client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("onCallApi"));
});

builder.Services.AddDbContext<CampsiteReservationDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("camping"));
});
builder.Services.AddScoped<ILookupApiStatus, SqlServerStatus>();
builder.Services.AddScoped<IUpdateTheStatus, SqlServerStatus>();
builder.Services.AddSingleton<ISystemTime, SystemTime>();
builder.Services.AddTransient<ILookupOnCallDevelopers, RemoteOnCallDeveloperLookup>();
builder.Services.AddTransient<ICheckTheStatus, LocalStatusService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("http://keycloak.local/auth/realms/services-testing/protocol/openid-connect/auth")
            }
        },
        In = ParameterLocation.Header,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement { 
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            }
        },
        new string[] { }
    }
   });
    
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId(audience); // 
        c.OAuthRealm("services-testing");
    });
}

app.UseAuthentication(); // this has to be be before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }