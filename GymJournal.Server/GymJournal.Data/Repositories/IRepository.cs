namespace GymJournal.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetById(Guid? guid, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
        Task<T> Add(T dto, CancellationToken cancellationToken = default);
		Task<T> Update(T dto, CancellationToken cancellationToken = default);
		Task Remove(Guid? id, CancellationToken cancellationToken = default);
        Task SaveChanges(CancellationToken cancellationToken = default);
    }
}