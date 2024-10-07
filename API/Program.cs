<<<<<<< HEAD
using Application.Abstraction;
using Application.Mapper;
using Application.Services;
using Application.Tokens;
using Core.Models;
=======
using Application.AdditionalLogic;
using Application.Services;
using Application.Tokens;
using Core.Abstraction;
using Core.Mapper;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using EventsTP.Middleware;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using DataAccess;
<<<<<<< HEAD
using DataAccess.RepoUOW;
using EventsTP.Validators;
=======
using DataAccess.Entities;
using DataAccess.RepoUOW;
using DataAccess.Validators;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79

var builder = WebApplication.CreateBuilder(args);

const string allowReactApp = "AllowReactApp";

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowReactApp,
        policy => policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

builder.Services.AddDbContext<TaskContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DataBase")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ITokenService,TokenService>();

builder.Services.AddControllers();

<<<<<<< HEAD
builder.Services.AddTransient<IValidator<Event>, EventValidator>();
builder.Services.AddTransient<IValidator<Participant>, ParticipantValidator>();
=======
builder.Services.AddTransient<IValidator<EventEntity>, EventValidator>();
builder.Services.AddTransient<IValidator<ParticipantEntity>, ParticipantValidator>();
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79

builder.Services.AddApiAuthentication(builder.Configuration);

var app = builder.Build();

app.UseCors(allowReactApp);

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "EventsTP API v1");
        options.OAuthClientId("react");
    });
    
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();