using System.Threading.Tasks;
using BusinessLogic.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheets.Requests;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly ILogger<ContractsController> _logger;
        private readonly IContractService _service;

        public ContractsController(
            ILogger<ContractsController> logger,
            IContractService service)
        {
            _logger = logger;
            _logger.LogDebug(1, $"Loggger встроен в {this.GetType()}");
            _service = service;
        }
        
        /// <summary>
        /// Производит регистрацию контракта (Создание записи в БД)
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST url:port/api/сontracts/register
        /// 
        ///     Body: { "Contract": "null" }
        ///
        /// </remarks>
        /// <param name="request">Данные запроса по контракту, который подлежит регистрации</param>
        /// <returns>None</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterContract([FromBody] RegisterContractRequest request)
        {
            _logger.LogInformation(
                $"Register contract. Contract id:{request.Contract.Id}");

            await _service.CreateAsync(request.Contract);
            return Ok();
        }
        
        /// <summary>
        /// Производит возврат информации по контракту. Поиск по имени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET url:port/api/сontracts/get_by_name
        ///
        ///     Body: { "ContractName": "name" }
        ///     
        /// </remarks>
        /// <param name="request">Данные запроса по имени контракта</param>
        /// <returns>Данные по контракту</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        [HttpGet("get_by_name")]
        public async Task<GetContractByNameResponse> GetContractByName([FromBody] GetContractByNameRequest request)
        {
            _logger.LogInformation($"Getting contract by name: {request.ContractName}");
            var response = await _service.GetEntityByNameAsync(request.ContractName);
            return await Task.FromResult(new GetContractByNameResponse { Contract = response });
        }

        /// <summary>
        /// Производит возврат информации по выбранному контракту. Поиск по идентификатору
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET url:port/api/сontracts/get_by_id
        /// 
        ///     Body: { "ContractId": "1" }
        /// 
        /// </remarks>
        /// <param name="request">Данные запроса по идентификатору контракта</param>
        /// <returns>Данные по выбранному контракту</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        [HttpGet("get_by_id")]
        public async Task<GetContractByIdResponse> GetContractById([FromBody] GetContractByIdRequest request)
        {
            _logger.LogInformation($"Getting contract by id: {request.ContractId}");
            var response = await _service.GetEntityByIdAsync(request.ContractId);
            return await Task.FromResult(new GetContractByIdResponse { Contract = response });
        }

        /// <summary>
        /// Производит возврат информации по контрактам в заданном диапазоне
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET url:port/api/сontracts/get_with_pagination
        ///
        ///     Body: { "PageNumber": "1", "ElementsPerPage": "5" }
        /// 
        /// </remarks>
        /// <param name="request">Данные запроса по диапазону поиска</param>
        /// <returns>Данные по контрактам в заданном диапазоне</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        [HttpGet("get_with_pagination")]
        public async Task<GetContractWithPaginationResponse> GetContractsWithPagination(
            [FromBody] GetContractsWithPaginationRequest request)
        {
            _logger.LogInformation(
                $"Getting contract with pagination. Page: {request.PageNumber}, Elements : {request.ElementsPerPage}");
            var response = 
                await _service.GetEntitiesAsync(request.PageNumber, request.ElementsPerPage);
            return await Task.FromResult(new GetContractWithPaginationResponse { Contracts = response });
        }
        
        
        /// <summary>
        /// Производит обновление информации по выбранному контракту
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     UPDATE url:port/api/сontracts/update
        ///
        ///     Body: { "Contract": "null" }
        ///  
        /// </remarks>
        /// <param name="request">Обновленные данные контракта</param>
        /// <returns>None</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateContractById([FromBody] UpdateContractRequest request)
        {
            _logger.LogInformation(
                $"Updating contract with id: {request.Contract.Id}");
            await _service.UpdateAsync(request.Contract);
            return Ok();
        }

        /// <summary>
        /// Производит удаление контракта (удаление из БД)
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     DELETE url:port/api/сontracts/delete
        ///
        ///     Body: { "ContractId": "id" }
        /// 
        /// </remarks>
        /// <param name="request">Данные запроса по контракту, подлежащий удалению</param>
        /// <returns>None</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteContract([FromBody] DeleteContractRequest request)
        {
            _logger.LogInformation(
                $"Delete contract. Contract id:{request.ContractId}");
            await _service.DeleteAsync(request.ContractId);
            return Ok();
        }
    }
}