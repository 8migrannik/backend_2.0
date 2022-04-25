using System.Text.Json.Serialization;
using ScrumBoard.BLL.DTO;
using ScrumBoard.BLL.Interfaces;
using ScrumBoard.BLL.Services;
using ScrumBoard.DAL.Entities;
using ScrumBoard.DAL.Repositories;
using ScrumBoardLibrary.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// mine
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IRepository<Board>, BoardRepository>();
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();