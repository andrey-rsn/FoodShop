using FoodShop.Web.Models;
using FoodShop.Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace FoodShop.Web.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDTO responseModel { get; set; }

        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new ResponseDTO();
            this.httpClient = httpClient;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("FoodShop");
                HttpRequestMessage requestMessage = new HttpRequestMessage();
                requestMessage.Headers.Add("Accept","application/json");
                requestMessage.RequestUri=new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.Data != null)
                {
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),Encoding.UTF8,"application/json");
                }
                HttpResponseMessage apiResponse = null;
                switch(apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        requestMessage.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.GET:
                        requestMessage.Method = HttpMethod.Get;
                        break;
                    case SD.ApiType.PUT:
                        requestMessage.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        requestMessage.Method = HttpMethod.Delete;
                        break;

                }
                apiResponse = await client.SendAsync(requestMessage);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);

                return apiResponseDto;
            }
            catch (Exception ex)
            {
                var dto = new ResponseDTO()
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);

                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
        }
    }
}
