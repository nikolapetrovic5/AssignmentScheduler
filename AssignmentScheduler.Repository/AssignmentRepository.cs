using Dapper;
using AssignmentScheduler.Entity;
using AssignmentScheduler.Repository.Factories.Interfaces;
using AssignmentScheduler.Repository.Interfaces;

namespace AssignmentScheduler.Repository;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public AssignmentRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> AddAsync(Assignment entity)
    {
        var query = @"
INSERT INTO Assignment (Title, Description, CreatedDate, StartDate, EndDate, UserId, Status)
VALUES
(@Title, @Description, @CreatedDate, @StartDate, @EndDate, @UserId, @Status);";
        using var connection = _connectionFactory.Create();

        var rowsAffected = await connection.ExecuteAsync(query,
            new
            { 
                entity.Title, entity.Description, entity.CreatedDate,
                entity.StartDate, entity.EndDate, entity.UserId,
                Status = entity.Status.ToString()
            });
        return rowsAffected == 1;
    }

    public async Task<Assignment> GetAsync(Guid id)
    {
        var query = "SELECT * FROM User WHERE Id = @Id;";
        using var connection = _connectionFactory.Create();

        return await connection.QueryFirstOrDefaultAsync<Assignment>(query, new { Id = id.ToString() });
    }

    public async Task<IEnumerable<Assignment>> GetAsync()
    {
        var query = "SELECT * FROM Assignment;";
        using var connection = _connectionFactory.Create();

        return await connection.QueryAsync<Assignment>(query);
    }

    public async Task<bool> RemoveAsync(Guid id)
    {
        var query = "DELETE FROM Assignment WHERE Id = @Id;";
        using var connection = _connectionFactory.Create();

        var rowsAffected = await connection.ExecuteAsync(query, new { Id = id.ToString() });
        return rowsAffected == 1;
    }

    public async Task<bool> UpdateAsync(Assignment entity)
    {
        var query = @"
UPDATE Assignment
SET Title = @Title,
Description = @Description,
CreatedDate = @CreatedDate,
StartDate = @StartDate,
EndDate = @EndDate,
UserId = @UserId,
Status = @Status;
WHERE Id = @Id";
        using var connection = _connectionFactory.Create();

        var rowsAffected = await connection.ExecuteAsync(query, entity);
        return rowsAffected == 1;
    }
}
