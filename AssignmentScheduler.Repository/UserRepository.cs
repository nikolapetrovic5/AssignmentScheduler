using AssignmentScheduler.Entity;
using AssignmentScheduler.Repository.Interfaces;
using Dapper;
using AssignmentScheduler.Repository.Factories.Interfaces;

namespace AssignmentScheduler.Repository;

public class UserRepository : IUserRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public UserRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> AddAsync(User entity)
    {
        var query = "INSERT INTO [dbo].[User] (Email, Password, CreatedDate) VALUES (@Email, @Password, @CreatedDate);";
        using var connection = _connectionFactory.Create();

        var rowsAffected = await connection.ExecuteAsync(query, new { entity.Email, entity.Password, entity.CreatedDate });
        return rowsAffected == 1;
    }

    public async Task<User> GetAsync(Guid id)
    {
        var query = "SELECT * FROM [dbo].[User] WHERE Id = @Id;";
        using var connection = _connectionFactory.Create();

        return await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id.ToString() });
    }

    public async Task<IEnumerable<User>> GetAsync()
    {
        var query = "SELECT * FROM [dbo].[User]";
        using var connection = _connectionFactory.Create();

        return await connection.QueryAsync<User>(query);
    }

    public async Task<bool> RemoveAsync(Guid id)
    {
        var query = "DELETE FROM [dbo].[User] WHERE Id = @Id;";
        using var connection = _connectionFactory.Create();

        var rowsAffected = await connection.ExecuteAsync(query, new { Id = id.ToString() });
        return rowsAffected == 1;
    }

    public async Task<bool> UpdateAsync(User entity)
    {
        var query = "UPDATE [dbo].[User] SET Email = @Email, Password = @Password, CreatedDate = @CreatedDate;";
        using var connection = _connectionFactory.Create();

        var rowsAffected = await connection.ExecuteAsync(query, new { entity.Email, entity.Password, entity.CreatedDate });
        return rowsAffected == 1;
    }
}
