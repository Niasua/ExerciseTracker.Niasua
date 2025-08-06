using ExerciseTracker.Niasua.Models;
using System.Data;
using Dapper;

namespace ExerciseTracker.Niasua.Repositories;

public class ExerciseRepositoryDapper : IExerciseRepository
{
    private readonly IDbConnection _connection;

    public ExerciseRepositoryDapper(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task AddAsync(Exercise exercise)
    {
        string sql = @"INSERT INTO Exercises (DateStart, DateEnd, Duration, Comments)
                       VALUES (@DateStart, @DateEnd, @Duration, @Comments)";
        await _connection.ExecuteAsync(sql, exercise);
    }

    public async Task DeleteAsync(int id)
    {
        string sql = @"DELETE FROM Exercises WHERE Id = @Id";
        await _connection.ExecuteAsync(sql, new { Id = id });
    }

    public async Task<List<Exercise>> GetAllAsync()
    {
        string sql = "SELECT * FROM Exercises";
        var result = await _connection.QueryAsync<Exercise>(sql);
        return result.ToList();
    }

    public async Task<Exercise?> GetByIdAsync(int id)
    {
        string sql = @"SELECT * FROM Exercises WHERE Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<Exercise>(sql, new { Id = id });
    }

    public async Task UpdateAsync(Exercise exercise)
    {
        string sql = @"UPDATE Exercises
                        SET DateStart = @DateStart,
                            DateEnd = @DateEnd,
                            Duration = @Duration,
                            Comments = @Comments
                        WHERE Id = @Id";
        await _connection.ExecuteAsync(sql, exercise);
    }
}
