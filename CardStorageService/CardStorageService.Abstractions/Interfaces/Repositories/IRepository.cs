namespace CardStorageService.Abstractions.Interfaces.Repositories
{
    public interface IRepository<T, TId> where T: class
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(TId id);
        Task<T> CreateAsync(T data);
        Task<T> UpdateAsync(T data);
        Task<T> DeleteAsync(TId id);
    }
}