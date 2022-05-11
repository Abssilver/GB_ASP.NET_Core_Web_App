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
    public sealed class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeeService _service;

        public EmployeesController(
            ILogger<EmployeesController> logger,
            IEmployeeService service)
        {
            _logger = logger;
            _logger.LogDebug(1, $"Logger встроен в {this.GetType()}");
            _service = service;
        }
        
        /// <summary>
        /// Производит регистрацию работника (Создание записи в БД)
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST url:port/api/employees/register
        /// 
        ///     Body: { "Employee": "null" }
        ///
        /// </remarks>
        /// <param name="request">Данные запроса по работнику, который подлежит регистрации</param>
        /// <returns>None</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeRequest request)
        {
            _logger.LogInformation(
                $"Register employee. Employee id:{request.Employee.Id}");

            await _service.CreateAsync(request.Employee);
            return Ok();
        }
        
        /// <summary>
        /// Производит возврат информации по работнику. Поиск по имени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET url:port/api/employees/get_by_name
        ///
        ///     Body: { "EmployeeName": "name" }
        ///     
        /// </remarks>
        /// <param name="request">Данные запроса по имени работника</param>
        /// <returns>Данные по работнику</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpGet("get_by_name")]
        public async Task<GetEmployeeByNameResponse> GetEmployeeByName([FromBody] GetEmployeeByNameRequest request)
        {
            _logger.LogInformation($"Getting employee by name: {request.EmployeeName}");
            var response = await _service.GetEntityByNameAsync(request.EmployeeName);
            return await Task.FromResult(new GetEmployeeByNameResponse { Employee = response });
        }

        /// <summary>
        /// Производит возврат информации по выбранному работнику. Поиск по идентификатору
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET url:port/api/employees/get_by_id
        /// 
        ///     Body: { "EmployeeId": "1" }
        /// 
        /// </remarks>
        /// <param name="request">Данные запроса по идентификатору работника</param>
        /// <returns>Данные по выбранному работнику</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpGet("get_by_id")]
        public async Task<GetEmployeeByIdResponse> GetEmployeeById([FromBody] GetEmployeeByIdRequest request)
        {
            _logger.LogInformation($"Getting Employee by id: {request.EmployeeId}");
            var response = await _service.GetEntityByIdAsync(request.EmployeeId);
            return await Task.FromResult(new GetEmployeeByIdResponse { Employee = response });
        }

        /// <summary>
        /// Производит возврат информации по работникам в заданном диапазоне
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET url:port/api/employees/get_with_pagination
        ///
        ///     Body: { "PageNumber": "1", "ElementsPerPage": "5" }
        /// 
        /// </remarks>
        /// <param name="request">Данные запроса по диапазону поиска</param>
        /// <returns>Данные по работникам в заданном диапазоне</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpGet("get_with_pagination")]
        public async Task<GetEmployeeWithPaginationResponse> GetEmployeesWithPagination(
            [FromBody] GetEmployeesWithPaginationRequest request)
        {
            _logger.LogInformation(
                $"Getting Employee with pagination. Page: {request.PageNumber}, Elements : {request.ElementsPerPage}");
            var response = 
                await _service.GetEntitiesAsync(request.PageNumber, request.ElementsPerPage);
            return await Task.FromResult(new GetEmployeeWithPaginationResponse { Employees = response });
        }
        
        
        /// <summary>
        /// Производит обновление информации по выбранному работнику
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     UPDATE url:port/api/employees/update
        ///
        ///     Body: { "Employee": "null" }
        ///  
        /// </remarks>
        /// <param name="request">Обновленные данные работника</param>
        /// <returns>None</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateEmployeeById([FromBody] UpdateEmployeeRequest request)
        {
            _logger.LogInformation(
                $"Updating Employee with id: {request.Employee.Id}");
            await _service.UpdateAsync(request.Employee);
            return Ok();
        }

        /// <summary>
        /// Производит удаление работника (удаление из БД)
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     DELETE url:port/api/employees/delete
        ///
        ///     Body: { "EmployeeId": "id" }
        /// 
        /// </remarks>
        /// <param name="request">Данные запроса по работнику, подлежащий удалению</param>
        /// <returns>None</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteEmployee([FromBody] DeleteEmployeeRequest request)
        {
            _logger.LogInformation(
                $"Delete Employee. Employee id:{request.EmployeeId}");
            await _service.DeleteAsync(request.EmployeeId.Value);
            return Ok();
        }
    }
}