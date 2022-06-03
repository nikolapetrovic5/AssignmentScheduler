namespace AssignmentScheduler.Repository.Factories.Abstractions;

public interface IFactory<T>
{
    public T Create();
}
