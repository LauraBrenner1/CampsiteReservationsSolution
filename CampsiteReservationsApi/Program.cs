global using CampsiteReservationsApi.Services;
using CampsiteReservationsApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Typed HttpClient 
builder.Services.AddHttpClient<OnCallApiService>( client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("onCallApi"));
});

builder.Services.AddDbContext<CampsiteReservationDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("camping"));
});
builder.Services.AddScoped<ILookupApiStatus, SqlServerOnCallDeveloperLookup>();
builder.Services.AddSingleton<ISystemTime, SystemTime>();
builder.Services.AddTransient<ILookupOnCallDevelopers, RemoteOnCallDeveloperLookup>();
builder.Services.AddTransient<ICheckTheStatus, LocalStatusService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }