using CRMD.Api.Hubs;
using CRMD.Api.Session;
using CRMD.Application;
using CRMD.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CrmdPolicy",
        policy =>
        {
            policy
                .WithOrigins(
                "http://localhost:4200",
                "https://localhost:7262",
                "http://localhost:5145"
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

var connectionString =
    builder.Configuration.GetConnectionString("Postgres")!;

builder.Services
    .AddApplications()
    .AddInfrastructures(connectionString);


builder.Services.AddSingleton<SessionConnections>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CrmdPolicy");

app.UseAuthorization();

app.MapControllers();
app.MapHub<SendOrderHub>("/sendorder");

app.Run();
