using ExerciseTracker.Niasua.Controllers;
using ExerciseTracker.Niasua.Data;
using ExerciseTracker.Niasua.Repositories;
using ExerciseTracker.Niasua.Services;
using ExerciseTracker.Niasua.UI.Menus;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var services = new ServiceCollection();

services.AddSingleton<IConfiguration>(config);

//services.AddDbContext<ExerciseContext>(options =>
//    options.UseSqlServer(config.GetConnectionString("DefaultConnection"))); // <--- EF

services.AddTransient<IDbConnection>(sp => // <--- Dapper
{
    var connectionString = config.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString);
});

//services.AddScoped<IExerciseRepository, ExerciseRepository>(); // <--- EF
services.AddScoped<IExerciseRepository, ExerciseRepositoryDapper>(); // <--- Dapper
services.AddScoped<ExerciseService>();
services.AddScoped<ExerciseController>();

var serviceProvider = services.BuildServiceProvider();

var controller = serviceProvider.GetRequiredService<ExerciseController>();

await MainMenu.Show(controller);

