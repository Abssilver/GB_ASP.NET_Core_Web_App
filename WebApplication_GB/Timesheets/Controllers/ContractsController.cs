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

        public ContractsController(ILogger<ContractsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {this.GetType()}");
        }
        
        //http://localhost:51685/api/сontracts/register
        //Body: { "ContractId": "1" }
        /// <summary>
        /// Производит регистрацию контракта (Создание записи в БД)
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST url:port/api/сontracts/register
        /// 
        ///     Body: { "ContractId": "1" }
        ///
        /// </remarks>
        /// <param name="request">Данные запроса по контракту, который подлежит регистрации</param>
        /// <returns>None</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        [HttpPost("register")]
        public IActionResult RegisterContract([FromBody] RegisterContractRequest request)
        {
            
            _logger.LogInformation(
                $"Register contract. Contract id:{request.ContractId}");

            return Ok();
        }
        
        /// <summary>
        /// Производит возврат информации по контрактам
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET url:port/api/сontracts/get_all
        ///
        /// </remarks>
        /// <returns>Данные по контрактам</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        [HttpGet("get_all")]
        public IActionResult GetAllContracts()
        {
            _logger.LogInformation($"Getting all contracts");

            return Ok("You got ok, for now...");
        }
        
        /// <summary>
        /// Производит возврат информации по выбранному контракту
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET url:port/api/сontracts/get/1
        /// 
        /// </remarks>
        /// <param name="contractId">Id зарегистрированного котракта</param>
        /// <returns>Данные по выбранному контракту</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        [HttpGet("get/{contractId}")]
        public IActionResult GetContractById([FromRoute] int contractId)
        {
            _logger.LogInformation($"Getting contract: {contractId}");

            return Ok("You got ok, for now...");
        }

        /// <summary>
        /// Производит обновление информации по выбранному контракту
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     UPDATE url:port/api/сontracts/get?contractId=1&newValue=value
        /// 
        /// </remarks>
        /// <param name="contractId">Id зарегистрированного контракта</param>
        /// <param name="newValue">Новая информация по контракту</param>
        /// <returns>None</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        [HttpPut("update")]
        public IActionResult UpdateContractById([FromQuery] int contractId, [FromQuery] string newValue)
        {
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
        public IActionResult DeleteContract([FromBody] DeleteContractRequest request)
        {
            _logger.LogInformation(
                $"Delete contract. Contract id:{request.ContractId}");
            
            return Ok();
        }
    }
}