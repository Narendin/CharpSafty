using CardStorageService.Abstractions.Interfaces.Repositories;
using CardStorageService.Core.Entities;
using CardStorageService.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CardStorageService.Abstractions.Services.Repositories
{
    // ToDo добавить валлидацию и логирование
    public class CardRepository : ICardRepositoryService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CardRepository> _logger;

        public CardRepository(AppDbContext context, ILogger<CardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Card> CreateAsync(Card data)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(client => client.Id == data.ClientId);
            if (client == null)
                throw new ArgumentOutOfRangeException($"Не найден пользователь с id [{data.ClientId}]");

            client.Cards.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<Card> DeleteAsync(Guid id)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(card => card.Id == id);
            if (card == null)
                throw new ArgumentOutOfRangeException($"Не найдена карта с id [{id}]");

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<IList<Card>> GetAllAsync()
        {
            return _context.Cards.ToList();
        }

        public async Task<IList<Card>> GetByClientIdAsync(Guid id)
        {
            var client = await _context.Clients.Include(client => client.Cards).FirstOrDefaultAsync(client => client.Id == id);
            if (client == null)
                throw new ArgumentOutOfRangeException($"Не найден пользователь с id [{id}]");

            return client.Cards.ToList();
        }

        public async Task<Card> GetByIdAsync(Guid id)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(card => card.Id == id);
            if (card == null)
                throw new ArgumentOutOfRangeException($"Не найдена карта с id [{id}]");

            return card;
        }

        public async Task<Card> UpdateAsync(Card data)
        {
            throw new InvalidOperationException("Не допускается обновление данных карты. Необходим перевыпуск.");
        }
    }
}