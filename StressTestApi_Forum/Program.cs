using StressTestApi_Forum.Middleware;
using RobotsTxt;
using StressTestApi_Forum;
using StressTestApi_Forum.Services.FakeJitter;
using StressTestApi_Forum.Services.Users;
using StressTestApi_Forum.Services.Posts;
using StressTestApi_Forum.Services;
using StressTestApi_Forum.Services.Efcore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFakeJitter();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapperMappings();
builder.AddForumDBContext();

builder.Services.AddScoped<IRobotsTxtProvider, RobotTxtProvider>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IPostService,PostService>();

var app = builder.Build();

await app.CheckDbMigrationsAsync();

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
