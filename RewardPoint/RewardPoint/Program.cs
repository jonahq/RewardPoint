using RewardPoint.Core.Services;
using RewardPoint.Infrastructure.Services;
using RewardPoint.Core.Repositories;
using RewardPoint.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
//Dependency Injections
builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();
builder.Services.AddScoped<IRewardService, RewardServices>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
