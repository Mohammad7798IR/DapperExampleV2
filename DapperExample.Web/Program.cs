using DapperExample.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Quartz;
using GettingStarted;
using DapperExample.DataAccess.Context;
using DapperExample.DataAccess.Jobs;

var builder = WebApplication.CreateBuilder(args);
IConfiguration _configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddQuartz(a =>
{
    var jobKey = new JobKey("GetAllJob");

    a.AddJob<GetAllJob>(a => a.WithIdentity(jobKey));

    a.UseMicrosoftDependencyInjectionJobFactory();

    a.AddTrigger(a => a
        .ForJob(jobKey)
        .StartNow()
        .WithIdentity("GetAllJob-trigger")
        .WithSimpleSchedule(b => b.WithIntervalInSeconds(30).RepeatForever()));

});


builder.Services.AddQuartzHostedService(a => a.WaitForJobsToComplete = true);

builder.Services.AddMassTransit(a =>
{

    a.AddConsumer<MessageConsumer>();

    a.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

//builder.Services.AddHostedService<Worker>();



builder.Services.AddDbContext<SqlDataAccess>(options =>
{
    options.UseSqlServer(_configuration.GetConnectionString("DefaultConnectionStrings"));
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
