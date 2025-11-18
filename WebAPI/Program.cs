using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Services;
using ClassLibrary;
using WebAPI.Data.PollRepo;
using WebAPI.Data.VoteOptionRepo;
using WebAPI.Data.VoteRepo;
using WebAPI.Interfaces.PollInterfaces;
using WebAPI.Interfaces.VoteInterfaces;
using WebAPI.Interfaces.VoteOptionsInterfaces;
using WebAPI.Services.PollServices;
using WebAPI.Services.VoteOptionServices;
using WebAPI.Services.VoteServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContextFactory<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IPollsService, PollService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVoteOptionService, VoteOptionService>();
builder.Services.AddScoped<IVoteService, VoteService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IRepository<Polls>, PollRepository>();
builder.Services.AddScoped<IRepository<Users>, UserRepository>();
builder.Services.AddScoped<IRepository<VoteOptions>, VoteOptionRepository>();
builder.Services.AddScoped<IRepository<Votes>, VoteRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.Run();
