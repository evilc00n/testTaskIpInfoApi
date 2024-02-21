using IpInfo.Application.Resources;
using IpInfo.Domain.Enum;
using IpInfo.Domain.Interfaces;
using IpInfo.Domain.Interfaces.Repositories;
using IpInfo.Domain.Interfaces.Services;
using IpInfo.Domain.Models;
using IpInfo.Domain.Result;
using Serilog;
using System.Text.Json;


namespace IpInfo.Application.Services
{
    public class DataService : IDataService
    {
        private readonly IBaseRepository<IpInfoEntity> _IpInfoRepository;
        private readonly IHttpApiClient _httpApiClient;
        private readonly ILogger _logger;
        private readonly IConnectionAdressConfig _connectionAdressConfig;

        public DataService(IBaseRepository<IpInfoEntity> ipInfoRepository,
            IHttpApiClient httpApiClient, ILogger logger, 
            IConnectionAdressConfig connectionAdressConfig)
        {
            _IpInfoRepository = ipInfoRepository
                ?? throw new ArgumentNullException(nameof(ipInfoRepository));

            _httpApiClient = httpApiClient
                ?? throw new ArgumentNullException(nameof(_httpApiClient));

            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));

            _connectionAdressConfig = connectionAdressConfig
                ?? throw new ArgumentNullException(nameof(connectionAdressConfig));
        }

        /// <inheritdoc />
        public async Task<BaseResult> SaveDataAsync(string Data, string ip)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(Data))
                {
                    throw new ArgumentNullException(nameof(Data));
                } else if (string.IsNullOrWhiteSpace(ip))
                {
                    throw new ArgumentNullException(nameof(ip));
                }



                var ipInfo = new IpInfoEntity()
                {
                    IpAddress = ip,
                    InfoData = JsonDocument.Parse(Data)
                };
                await _IpInfoRepository.CreateAsync(ipInfo);
                return new BaseResult();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternarServerError
                };
            }
        }

        /// <inheritdoc/>
        public async Task<BaseResult<string>> GetDataAsync(string ip)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(ip))
                {
                    return new BaseResult<string>
                    {
                        ErrorMessage = ErrorMessage.InvalidIpFormat,
                        ErrorCode = (int)ErrorCodes.InvalidIpFormat
                    };
                }

                if (!IsIpAddress(ip))
                {
                    return new BaseResult<string>
                    {
                        ErrorMessage = ErrorMessage.InvalidIpFormat,
                        ErrorCode = (int)ErrorCodes.InvalidIpFormat
                    };
                }


                string uri = string.Format(_connectionAdressConfig.ConnectionString, ip);

                string result = await _httpApiClient.GetAsync(uri);    
                if (result == null)
                {
                    throw new ArgumentNullException(nameof(result));
                }

                return new BaseResult<string>()
                {
                    Data = result
                }; 

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<string>
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternarServerError
                };
            }
        }

        /// <summary>
        /// Проверка на валидность IP адреса
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsIpAddress(string input)
        {

            string[] splitValues = input.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            foreach (string r in splitValues)
            {
                if (r.Length > 1 && r.StartsWith("0"))
                {
                    return false;
                }

                if (!byte.TryParse(r, out tempForParsing))
                {
                    return false;
                }
            }

            return true;
        }




    }
}
