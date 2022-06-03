using System.Data.SqlClient;
using AssignmentScheduler.Repository.Factories.Abstractions;

namespace AssignmentScheduler.Repository.Factories.Interfaces;

public interface IConnectionFactory : IFactory<SqlConnection>
{
}
