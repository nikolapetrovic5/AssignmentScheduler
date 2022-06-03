using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using AssignmentScheduler.Repository.Factories.Interfaces;

namespace AssignmentScheduler.Repository.Factories;

public class ConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration _configuration;
    private SqlConnection connection;

    public ConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;

        if (connection == null)
            connection = new SqlConnection(_configuration.GetConnectionString("Default"));
    }

    public SqlConnection Create() => connection;
}
