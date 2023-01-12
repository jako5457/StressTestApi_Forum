using StressTestApi_Forum.Middleware;
using RobotsTxt;
using StressTestApi_Forum;
using StressTestApi_Forum.Services.FakeJitter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFakeJitter();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRobotsTxtProvider, RobotTxtProvider>();

var app = builder.Build();

app.UseRobotsTxt();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseFakeJitter();

app.MapControllers();

app.Run();
