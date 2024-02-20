using IpInfo.Application.Resources;
using IpInfo.Domain.Enum;
using IpInfo.Domain.Interfaces.Repositories;
using IpInfo.Domain.Interfaces.Services;
using IpInfo.Domain.Models;
using IpInfo.Domain.Result;
using Serilog;


namespace IpInfo.Application.Services
{
    public class DataService : IDataService
    {
        private readonly IBaseRepository<IpInfoEntity> _IpInfoRepository;
        private readonly IHttpApiClient _httpApiClient;
        private readonly ILogger _logger;

        public DataService(IBaseRepository<IpInfoEntity> ipInfoRepository, 
            IHttpApiClient httpApiClient, ILogger logger)
        {
            _IpInfoRepository = ipInfoRepository 
                ?? throw new ArgumentNullException(nameof(ipInfoRepository));
            _httpApiClient = httpApiClient 
                ?? throw new ArgumentNullException(nameof(_httpApiClient));
            _logger = logger 
                ?? throw new ArgumentNullException(nameof(logger));
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
                    InfoData = Data
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

        /// <inheritdoc />
        public async Task<BaseResult<string>> GetDataAsync(string uri)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uri))
                {
                    throw new ArgumentNullException(nameof(uri));
                }


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




    }
}
