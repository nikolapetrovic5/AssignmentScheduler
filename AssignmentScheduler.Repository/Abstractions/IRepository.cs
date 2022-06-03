namespace AssignmentScheduler.Repository.Abstractions;

public interface IRepository<T>
{
    Task<bool> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> RemoveAsync(Guid id);
    Task<T> GetAsync(Guid id);
    Task<IEnumerable<T>> GetAsync();
}
