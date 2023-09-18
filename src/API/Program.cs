using DOMAIN.Interfaces.Repositories;
using DOMAIN.Interfaces.Services;
using DOMAIN.Services;
using INFRA.Database.Context;
using INFRA.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("api-financeira-cubos")));

/* DI services */
builder.Services.AddScoped<IPeopleService, PeopleService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

/* DI repositories */
builder.Services.AddScoped<IPeopleRepository, PeoplesRepository>();
builder.Services.AddScoped<ICardsRepository, CardRepository>();
builder.Services.AddScoped<IAccountRepository, AccountsRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();



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