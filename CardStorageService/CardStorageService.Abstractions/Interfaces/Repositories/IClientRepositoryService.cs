using CardStorageService.Core.Entities;

namespace CardStorageService.Abstractions.Interfaces.Repositories
{
    public interface IClientRepositoryService : IRepository<Client, Guid>
    {
    }
}