using CardStorageService.Abstractions.Interfaces.Repositories;
using CardStorageService.Core.Entities;
using CardStorageService.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CardStorageService.Abstractions.Services.Repositories
{
    // ToDo добавить валидацию и логирование
    public class ClientRepository : IClientRepositoryService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(AppDbContext context, ILogger<ClientRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Client> CreateAsync(Client data)
        {
            await _context.Clients.AddAsync(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<Client> DeleteAsync(Guid id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(client => client.Id == id);
            if (client == null)
                throw new ArgumentOutOfRangeException($"Не найден пользователь с id [{id}]");

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<IList<Client>> GetAllAsync()
        {
            return _context.Clients.ToList();
        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(client => client.Id == id);
            if (client == null)
                throw new ArgumentOutOfRangeException($"Не найден пользователь с id [{id}]");

            return client;
        }

        public async Task<Client> UpdateAsync(Client data)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(client => client.Id == data.Id);
            if (client == null)
                throw new ArgumentOutOfRangeException($"Не найден пользователь с id [{data.Id}]");

            client.FirstName = data.FirstName;
            client.SecondName = data.SecondName;
            client.Patronymic = data.Patronymic;

            await _context.SaveChangesAsync();

            return client;
        }
    }
}