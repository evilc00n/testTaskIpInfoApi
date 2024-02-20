using IpInfo.Domain.Interfaces;
using IpInfo.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace IpInfo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IpInfoController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IConnectionAdressConfig _connectionAdressConfig;

        public IpInfoController(IDataService dataService, 
            IConnectionAdressConfig connectionAdressConfig)
        {
            _dataService = dataService;
            _connectionAdressConfig = connectionAdressConfig;
        }




        /// <summary>
        /// Получение данных об ip адресе
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Sample request:
        ///  
        ///     GET
        ///     {
        ///         "ip": "127.0.0.1"
        ///     }
        /// </remarks>
        /// <response code="200">Если данные были получены</response>
        /// <response code="400">Если данные не были получены</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{ip}")]
        public async Task<IActionResult> GetIpInfo(string ip)
        {
            string uri = string.Format(_connectionAdressConfig.ConnectionString, ip);



            var data = await _dataService.GetDataAsync(uri);
            await _dataService.SaveDataAsync(data.Data, ip);
            if (data.IsSuccess)
            {
                return Ok(data.Data);
            }

            return BadRequest(data);
        }
    }
}
