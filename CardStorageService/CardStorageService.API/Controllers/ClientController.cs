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
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientRepositoryService _clientRepository;

        public ClientController(ILogger<ClientController> logger, IClientRepositoryService clientRepository)
        {
            _logger = logger;
            _clientRepository = clientRepository;
        }

        [HttpGet(Constants.ControllerMethods.Load)]
        public async Task<Result<ClientDto>> Load(Guid id)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(id);
                var result = new ClientDto()
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    SecondName = client.SecondName,
                    Patronymic = client.Patronymic
                };
                return Result<ClientDto>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка получения пользователя по id [{id}]");
                return Result<ClientDto>.Fail(this, ex.Message, ex);
            };
        }

        [HttpGet(Constants.ControllerMethods.List)]
        public async Task<Result<List<ClientDto>>> List()
        {
            try
            {
                var clients = await _clientRepository.GetAllAsync();
                var result = clients.Select(client => new ClientDto()
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    SecondName = client.SecondName,
                    Patronymic = client.Patronymic
                }).ToList();

                return Result<List<ClientDto>>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка создания пользователя");
                return Result<List<ClientDto>>.Fail(this, ex.Message, ex);
            };

        }

        [HttpPost(Constants.ControllerMethods.Create)]
        public async Task<Result<ClientDto>> Create([FromBody] ClientCreateRequest data)
        {
            try
            {
                var client = await _clientRepository.CreateAsync(new Client()
                {
                    FirstName = data.FirstName,
                    SecondName = data.SecondName,
                    Patronymic = data.Patronymic
                });
                var result = new ClientDto()
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    SecondName = client.SecondName,
                    Patronymic = client.Patronymic
                };
                return Result<ClientDto>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка создания пользователя");
                return Result<ClientDto>.Fail(this, ex.Message, ex);
            };
        }

        [HttpPut(Constants.ControllerMethods.Update)]
        public async Task<Result<ClientDto>> Update([FromBody] ClientUpdateRequest data)
        {
            try
            {
                var client = await _clientRepository.UpdateAsync(new Client()
                {
                    Id = data.Id,
                    FirstName = data.FirstName,
                    SecondName = data.SecondName,
                    Patronymic = data.Patronymic
                });
                var result = new ClientDto()
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    SecondName = client.SecondName,
                    Patronymic = client.Patronymic
                };
                return Result<ClientDto>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка обновления параметров пользователя");
                return Result<ClientDto>.Fail(this, ex.Message, ex);
            };
        }

        [HttpDelete(Constants.ControllerMethods.Delete)]
        public async Task<Result<ClientDto>> Delete(Guid id)
        {
            try
            {
                var client = await _clientRepository.DeleteAsync(id);
                var result = new ClientDto()
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    SecondName = client.SecondName,
                    Patronymic = client.Patronymic
                };
                return Result<ClientDto>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка удаления пользователя по id [{id}]");
                return Result<ClientDto>.Fail(this, ex.Message, ex);
            };
        }
    }
}