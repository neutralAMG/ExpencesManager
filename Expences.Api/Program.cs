using Expences.Aplication.Contracts;
using Expences.Aplication.Services;
using Expences.Infraestrocture.Context;
using Expences.Infraestrocture.Interfaces;
using Expences.Infraestrocture.Repositories;
using Expences.Infraestrocture.Utils.PasswordHasher;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add context
builder.Services.AddDbContext<DbAppContext>(optopns => 
optopns.UseSqlServer(builder.Configuration.GetConnectionString("DbAppContext")));

//Add Repositories
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IExpencesRepository, ExpencesRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
//Add services
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IExpencesService, ExpencesService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IOTimeout = TimeSpan.FromMinutes(70);
});
//Config cor
builder.Services.AddCors( options =>{
    options.AddPolicy("App", op => {

        op.WithOrigins("https://localhost:7226");
        op.AllowAnyHeader();
        op.AllowAnyMethod();
        
     }
    );

});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSession();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("App");

app.Run();
