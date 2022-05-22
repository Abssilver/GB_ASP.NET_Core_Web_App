using System.Threading.Tasks;
using BusinessLogic.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheets.Requests;
using Timesheets.Requests.Extensions;
using Timesheets.Validation;

namespace Timesheets.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public sealed class ContractsController : ControllerBase
    {
        private readonly ILogger<ContractsController> _logger;
        private readonly IContractService _service;
        private readonly IRegisterContractRequestValidationService _registerValidationService;
        private readonly IGetContractByNameRequestValidationService _getByNameValidationService;
        private readonly IGetContractByIdRequestValidationService _getByIdValidationService;
        private readonly IGetContractsWithPaginationRequestValidationService _getWithPaginationValidationService;
        private readonly IUpdateContractRequestValidationService _updateValidationService;
        private readonly IDeleteContractRequestValidationService _deleteValidationService;

        public ContractsController(
            ILogger<ContractsController> logger,
            IContractService service,
            IRegisterContractRequestValidationService registerValidationService,
            IGetContractByNameRequestValidationService getByNameValidationService,
            IGetContractByIdRequestValidationService getByIdValidationService,
            IGetContractsWithPaginationRequestValidationService getWithPaginationValidationService,
            IUpdateContractRequestValidationService updateValidationService,
            IDeleteContractRequestValidationService deleteValidationService)
        {
            _logger = logger;
            _logger.LogDebug(1, $"Logger встроен в {this.GetType()}");
            _service = service;

            _registerValidationService = registerValidationService;
            _getByNameValidationService = getByNameValidationService;
            _getByIdValidationService = getByIdValidationService;
            _getWithPaginationValidationService = getWithPaginationValidationService;
            _updateValidationService = updateValidationService;
            _deleteValidationService = deleteValidationService;
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
        /// <response code="401">Пользователь не прошел аутентификацию</response> 
        [HttpPost("register")]
        public async Task<IActionResult> RegisterContract([FromBody] RegisterContractRequest request)
        {
            var failures = _registerValidationService.ValidateEntity(request);
            if (failures.Count > 0)
            {
                return BadRequest(failures);
            }
            
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
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpGet("get_by_name")]
        public async Task<GetContractByNameResponse> GetContractByName([FromBody] GetContractByNameRequest request)
        {
            var failures = _getByNameValidationService.ValidateEntity(request);
            if (failures.Count > 0)
            {
                return new GetContractByNameResponse { Contract = null }.Failure(failures);
            }

            _logger.LogInformation($"Getting contract by name: {request.ContractName}");
            var response = await _service.GetEntityByNameAsync(request.ContractName);
            return new GetContractByNameResponse { Contract = response }.Success();
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
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpGet("get_by_id")]
        public async Task<GetContractByIdResponse> GetContractById([FromBody] GetContractByIdRequest request)
        {
            var failures = _getByIdValidationService.ValidateEntity(request);
            if (failures.Count > 0)
            {
                return new GetContractByIdResponse { Contract = null }.Failure(failures);
            }
            
            _logger.LogInformation($"Getting contract by id: {request.ContractId.Value}");
            var response = await _service.GetEntityByIdAsync(request.ContractId.Value);
            return new GetContractByIdResponse { Contract = response }.Success();
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
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpGet("get_with_pagination")]
        public async Task<GetContractWithPaginationResponse> GetContractsWithPagination(
            [FromBody] GetContractsWithPaginationRequest request)
        {
            var failures = _getWithPaginationValidationService.ValidateEntity(request);
            if (failures.Count > 0)
            {
                return new GetContractWithPaginationResponse { Contracts = null }.Failure(failures);
            }
            
            _logger.LogInformation(
                $"Getting contract with pagination. Page: {request.PageNumber.Value}, Elements : {request.ElementsPerPage.Value}");
            var response = 
                await _service.GetEntitiesAsync(request.PageNumber.Value, request.ElementsPerPage.Value);
            return new GetContractWithPaginationResponse { Contracts = response }.Success();
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
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateContractById([FromBody] UpdateContractRequest request)
        {
            var failures = _updateValidationService.ValidateEntity(request);
            if (failures.Count > 0)
            {
                return BadRequest(failures);
            }
            
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
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteContract([FromBody] DeleteContractRequest request)
        {
            var failures = _deleteValidationService.ValidateEntity(request);
            if (failures.Count > 0)
            {
                return BadRequest(failures);
            }
            
            _logger.LogInformation(
                $"Delete contract. Contract id:{request.ContractId.Value}");
            await _service.DeleteAsync(request.ContractId.Value);
            return Ok();
        }
    }
}