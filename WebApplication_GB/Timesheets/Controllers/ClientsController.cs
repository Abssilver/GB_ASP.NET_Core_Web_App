using System.Threading.Tasks;
using BusinessLogic.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheets.Requests;

namespace Timesheets.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public sealed class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IClientService _service;

        public ClientsController(
            ILogger<ClientsController> logger,
            IClientService service)
        {
            _logger = logger;
            _logger.LogDebug(1, $"Logger встроен в {this.GetType()}");
            _service = service;
        }
        
        /// <summary>
        /// Производит регистрацию клиента (Создание записи в БД)
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST url:port/api/clients/register
        /// 
        ///     Body: { "Client": "null" }
        ///
        /// </remarks>
        /// <param name="request">Данные запроса по клиенту, который подлежит регистрации</param>
        /// <returns>None</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterClientRequest request)
        {
            _logger.LogInformation(
                $"Register client. Client id:{request.Client.Id}");

            await _service.CreateAsync(request.Client);
            return Ok();
        }
        
        /// <summary>
        /// Производит возврат информации по клиенту. Поиск по имени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET url:port/api/clients/get_by_name
        ///
        ///     Body: { "ClientName": "name" }
        ///     
        /// </remarks>
        /// <param name="request">Данные запроса по имени клиента</param>
        /// <returns>Данные по клиенту</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpGet("get_by_name")]
        public async Task<GetClientByNameResponse> GetClientByName([FromBody] GetClientByNameRequest request)
        {
            _logger.LogInformation($"Getting client by name: {request.ClientName}");
            var response = await _service.GetEntityByNameAsync(request.ClientName);
            return await Task.FromResult(new GetClientByNameResponse { Client = response });
        }

        /// <summary>
        /// Производит возврат информации по выбранному клиенту. Поиск по идентификатору
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET url:port/api/clients/get_by_id
        /// 
        ///     Body: { "ClientId": "1" }
        /// 
        /// </remarks>
        /// <param name="request">Данные запроса по идентификатору клиента</param>
        /// <returns>Данные по выбранному клиенту</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpGet("get_by_id")]
        public async Task<GetClientByIdResponse> GetClientById([FromBody] GetClientByIdRequest request)
        {
            _logger.LogInformation($"Getting client by id: {request.ClientId}");
            var response = await _service.GetEntityByIdAsync(request.ClientId.Value);
            return await Task.FromResult(new GetClientByIdResponse { Client = response });
        }

        /// <summary>
        /// Производит возврат информации по клиентам в заданном диапазоне
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET url:port/api/clients/get_with_pagination
        ///
        ///     Body: { "PageNumber": "1", "ElementsPerPage": "5" }
        /// 
        /// </remarks>
        /// <param name="request">Данные запроса по диапазону поиска</param>
        /// <returns>Данные по клиентам в заданном диапазоне</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpGet("get_with_pagination")]
        public async Task<GetClientWithPaginationResponse> GetClientsWithPagination(
            [FromBody] GetClientsWithPaginationRequest request)
        {
            _logger.LogInformation(
                $"Getting clients with pagination. Page: {request.PageNumber}, Elements : {request.ElementsPerPage}");
            var response = 
                await _service.GetEntitiesAsync(request.PageNumber.Value, request.ElementsPerPage.Value);
            return await Task.FromResult(new GetClientWithPaginationResponse { Clients = response });
        }
        
        
        /// <summary>
        /// Производит обновление информации по выбранному клиенту
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     UPDATE url:port/api/clients/update
        ///
        ///     Body: { "Client": "null" }
        ///  
        /// </remarks>
        /// <param name="request">Обновленные данные клиента</param>
        /// <returns>None</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateClientById([FromBody] UpdateClientRequest request)
        {
            _logger.LogInformation(
                $"Updating client with id: {request.Client.Id}");
            await _service.UpdateAsync(request.Client);
            return Ok();
        }

        /// <summary>
        /// Производит удаление клиента (удаление из БД)
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     DELETE url:port/api/clients/delete
        ///
        ///     Body: { "ClientId": "id" }
        /// 
        /// </remarks>
        /// <param name="request">Данные запроса по клиенту, подлежащий удалению</param>
        /// <returns>None</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteClient([FromBody] DeleteClientRequest request)
        {
            _logger.LogInformation(
                $"Delete client. Client id:{request.ClientId}");
            await _service.DeleteAsync(request.ClientId.Value);
            return Ok();
        }
    }
}