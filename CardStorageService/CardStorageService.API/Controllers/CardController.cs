using CardStorageService.Abstractions.Interfaces.Repositories;
using CardStorageService.Abstractions.Models;
using CardStorageService.Abstractions.Requests;
using CardStorageService.Abstractions.ResponseTools;
using CardStorageService.Core;
using CardStorageService.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CardStorageService.API.Controllers
{
    // ToDo добавить автомаппер
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly ICardRepositoryService _cardRepository;

        public CardController(ILogger<CardController> logger, ICardRepositoryService cardRepository)
        {
            _logger = logger;
            _cardRepository = cardRepository;
        }

        [HttpGet("GetAllByClientId/{id}")]
        public async Task<Result<List<CardDto>>> GetClientCards(Guid id)
        {
            try
            {
                var cards = await _cardRepository.GetByClientIdAsync(id);
                var result = cards.Select(card => new CardDto()
                {
                    Id = card.Id,
                    CardNo = card.CardNo,
                    ClientId = card.ClientId,
                    ExpDate = card.ExpDate,
                    Name = card.Name
                }).ToList();

                return Result<List<CardDto>>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка получения карт пользователя с id [{id}]");
                return Result<List<CardDto>>.Fail(this, ex.Message, ex);
            };
        }

        [HttpGet(Constants.ControllerMethods.Load)]
        public async Task<Result<CardDto>> Load(Guid id)
        {
            try
            {
                var card = await _cardRepository.GetByIdAsync(id);
                var result = new CardDto()
                {
                    Id = card.Id,
                    ClientId = card.ClientId,
                    CardNo = card.CardNo,
                    Name = card.Name,
                    ExpDate = card.ExpDate
                };

                return Result<CardDto>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка получения карты по id [{id}]");
                return Result<CardDto>.Fail(this, ex.Message, ex);
            }
        }

        [HttpGet(Constants.ControllerMethods.List)]
        public async Task<Result<List<CardDto>>> List()
        {
            try
            {
                var cards = await _cardRepository.GetAllAsync();
                var result = cards.Select(card => new CardDto()
                {
                    Id = card.Id,
                    CardNo = card.CardNo,
                    ClientId = card.ClientId,
                    ExpDate = card.ExpDate,
                    Name = card.Name
                }).ToList();

                return Result<List<CardDto>>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка получения карт");
                return Result<List<CardDto>>.Fail(this, ex.Message, ex);
            };
        }

        [HttpPost(Constants.ControllerMethods.Create)]
        public async Task<Result<CardDto>> Create(CardCreateRequest data)
        {
            try
            {
                var card = await _cardRepository.CreateAsync(new Card()
                {
                    ClientId = data.ClientId,
                    CardNo = data.CardNo,
                    CVV2 = data.CVV2,
                    Name = data.Name,
                    ExpDate = data.ExpDate
                });
                var result = new CardDto()
                {
                    Id = card.Id,
                    ClientId = card.ClientId,
                    CardNo = card.CardNo,
                    Name = card.Name,
                    ExpDate = card.ExpDate
                };

                return Result<CardDto>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания карты");
                return Result<CardDto>.Fail(this, ex.Message, ex);
            }
            
        }

        // update не выполнен намеренно. Обновление данных банковской карты заблокировано. Только перевыпуск.

        [HttpDelete(Constants.ControllerMethods.Delete)]
        public async Task<Result<CardDto>> Delete(Guid id)
        {
            try
            {
                var card = await _cardRepository.DeleteAsync(id);
                var result = new CardDto()
                {
                    Id = card.Id,
                    ClientId = card.ClientId,
                    CardNo = card.CardNo,
                    Name = card.Name,
                    ExpDate = card.ExpDate
                };

                return Result<CardDto>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка удаления карты по id [{id}]");
                return Result<CardDto>.Fail(this, ex.Message, ex);
            }
        }
    }
}