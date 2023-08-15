using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BonifatiusERP.Models;
using BonifatiusERP.Admin.Models;
using BonifatiusERP.Models.Models.Common;

namespace BonifatiusERP.Services
{
    public class WebApiClient
    {
       private readonly IConfiguration _configuration;
        private readonly ILogger<WebApiClient> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IAuthHelper _authHelper;
        private readonly UserSession _UserSession;

        private string ApiBaseUrl { get; set; }
        private string Token { get; set; }

        public WebApiClient(IConfiguration configuration,
                         ILogger<WebApiClient> logger,
                         IHttpContextAccessor httpContextAccessor,
                         //IAuthHelper authHelper,
                         UserSession UserSession)
        {
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            //_authHelper = authHelper;
            _UserSession = UserSession;

            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                Token = _UserSession.AccessToken;
            //else
            //    Token = _authHelper.GetDefaultToken();

            ApiBaseUrl = _configuration.GetSection("AppSettings:ApiBaseUrl").Value;
        }

        public async Task<ApiResponseModel> GetAsync(dynamic id, string endpoint)
        {
            ApiResponseModel model;

            try
            {
                var url = $"{ApiBaseUrl}/{endpoint}";
                if (id != null)
                    url = $"{url}?{id}";

                using var httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                using var client = new HttpClient(httpClientHandler)
                {
                    Timeout = TimeSpan.FromMinutes(30)
                };

                if (!string.IsNullOrWhiteSpace(Token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                var response = await client.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<object>(responseContent);
                model = JsonConvert.DeserializeObject<ApiResponseModel>(responseContent);
                model.Code = (int)response.StatusCode;

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error calling endpoint: {endpoint}. Error Description: {ex.Message}");
                throw ex;
            }
        }

        public async Task<ApiResponseModel> PostAsync(dynamic input, string endpoint)
        {
            ApiResponseModel model;

            try
            {
                StringContent data = new StringContent("");
                if (input != null)
                {
                    var json = JsonConvert.SerializeObject(input);
                    data = new StringContent(json, Encoding.UTF8, "application/json");
                }
                var url = $"{ApiBaseUrl}/{endpoint}";

                using var httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                using var client = new HttpClient(httpClientHandler)
                {
                    Timeout = TimeSpan.FromMinutes(30)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                var response = await client.PostAsync(url, data);
                var responseContent = await response.Content.ReadAsStringAsync();

                model = JsonConvert.DeserializeObject<ApiResponseModel>(responseContent);
                model.Code = (int)response.StatusCode;


                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error calling endpoint: {endpoint}. Error Description: {ex.Message}");
                throw ex;
            }
        }

        public async Task<ApiResponseModel> PostAsync(string endpoint)
        {
            return await PostAsync(null, endpoint);
        }

        public async Task<ApiResponseModel> GetAsync(string endpoint)
        {
            return await GetAsync(null, endpoint);
        }

        public async Task<ApiResponseModel> PostFormDataAsync(MultipartFormDataContent input, string endpoint)
        {
            ApiResponseModel model;

            try
            {
                //StringContent data = new StringContent("");
                //if (input != null)
                //{
                //    var json = JsonConvert.SerializeObject(input);
                //    data = new StringContent(json, Encoding.UTF8, "application/json");
                //}

                var url = $"{ApiBaseUrl}/{endpoint}";

                using var httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                using var client = new HttpClient(httpClientHandler)
                {
                    Timeout = TimeSpan.FromMinutes(30)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                var response = await client.PostAsync(url, input);
                var responseContent = await response.Content.ReadAsStringAsync();

                model = JsonConvert.DeserializeObject<ApiResponseModel>(responseContent);
                model.Code = (int)response.StatusCode;

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error calling endpoint: {endpoint}. Error Description: {ex.Message}");
                throw ex;
            }
        }

        public async Task<LoginResponseModel> LoginPostAsync(dynamic input, string endpoint)
        {
            LoginResponseModel model;

            try
            {
                StringContent data = new StringContent("");
                if (input != null)
                {
                    var json = JsonConvert.SerializeObject(input);
                    data = new StringContent(json, Encoding.UTF8, "application/json");
                }
                var url = $"{ApiBaseUrl}/{endpoint}";

                using var httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                using var client = new HttpClient(httpClientHandler)
                {
                    Timeout = TimeSpan.FromMinutes(30)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                var response = await client.PostAsync(url, data);
                var responseContent = await response.Content.ReadAsStringAsync();

                model = JsonConvert.DeserializeObject<LoginResponseModel>(responseContent);
                model.Code = (int)response.StatusCode;

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error calling endpoint: {endpoint}. Error Description: {ex.Message}");
                throw ex;
            }
        }

    }
}
