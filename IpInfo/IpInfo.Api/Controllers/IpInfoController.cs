using IpInfo.Domain.Interfaces;
using IpInfo.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace IpInfo.Api.Controllers
{
    [Route("api/ip-info")]
    [ApiController]
    public class IpInfoController : ControllerBase
    {
        private readonly IDataService _dataService;


        public IpInfoController(IDataService dataService, 
            IConnectionAdressConfig connectionAdressConfig)
        {
            _dataService = dataService;
        }




        /// <summary>
        /// Получение данных об ip адресе
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Пример запроса:
        ///  
        /// "GET https://localhost:portNumber/api/ip-info/161.185.160.93"
        ///
        /// "https://localhost:portNumber" заменить на необходимое. 
        ///   
        /// </remarks>
        /// <response code="200">Если данные были получены</response>
        /// <response code="400">Если формат ip не верен</response>
        /// <response code="500">Если произошла внутренняя ошибка</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{ip}")]
        public async Task<IActionResult> GetIpInfo(string ip)
        {
            try 
            {

                var data = await _dataService.GetDataAsync(ip);

                if (data.IsSuccess)
                {
                    await _dataService.SaveDataAsync(data.Data, ip);
                    return Ok(data.Data);
                }


                if (data.ErrorCode == 1) { return BadRequest(data.ErrorMessage); }

                else return StatusCode(500, data.ErrorMessage);
            }
            catch(Exception ex) 
            {

                return StatusCode(500);
            }


        }
    }
}
