
using Newtonsoft.Json;
using RetrieveJWTToken.Models;
using System.Net.Http;
using System.Text;

namespace RetrieveJWTToken.Services
{
    public class CentralizedLoggerService : ICentralizedLoggerService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public CentralizedLoggerService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<int> WritingLogAsync(string strMessage, string strType)
        {
            try
            {
                var logData = new LogData
                {
                    log_event = strMessage,
                    log_eventtype = strType,
                    log_microservice = nameof(RetrieveJWTToken)
                };

                var client = httpClientFactory.CreateClient();
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri("http://localhost:8083/api/Logger");
                message.Content = new StringContent(JsonConvert.SerializeObject(logData), Encoding.UTF8, "application/json");
                message.Method = HttpMethod.Post;

                await client.SendAsync(message);

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
