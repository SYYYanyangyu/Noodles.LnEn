using Listening.Main.WebAPI.Controllers.Albums.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Noodles.Common;
using Noodles.Common.Validators;
using RabbitMQ.Client;
using RestSharp;
using static Listening.Main.WebAPI.Controllers.AI.ViewModel.MessageModel;

namespace Listening.Main.WebAPI.Controllers.AI
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class AIAnalysisController : ControllerBase
    {
        public static string API_KEY = string.Empty;

        public static string SECRET_KEY = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AIAnalysisController()
        {

        }

        /// <summary>
        /// simple parsingG rammar
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ParsingGrammar")]
        public async Task<ActionResult<string>> ParsingGrammar(string prompt)
        {
            try
            {
                var accessToken = await GetAccessToken(); // 确保使用异步方式获取token

                var client = new RestClient($"https://aip.baidubce.com/rpc/2.0/ai_custom/v1/wenxinworkshop/chat/chatlaw?access_token={accessToken}");

                var request = new RestRequest(Method.POST);

                request.AddHeader("Content-Type", "application/json");
                var body = $@"{{""messages"":[{{""role"":""user"",""content"":""{prompt}""}}],""extra_parameters"":{{""use_keyword"":true,""use_reference"":true}}}}";

                request.AddParameter("application/json", body, ParameterType.RequestBody);

                var response = await client.ExecuteAsync(request); // 使用异步执行请求

                return response.Content;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 得到Token
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public async Task<string> GetAccessToken()
        {
            try
            {
                API_KEY = AppSetting.app(new string[] { "BaiduAI", "APIKey" });

                SECRET_KEY = AppSetting.app(new string[] { "BaiduAI", "SecretKey" });

                var client = new RestClient($"https://aip.baidubce.com/oauth/2.0/token");

                client.Timeout = -1;

                var request = new RestRequest(Method.POST);

                request.AddParameter("grant_type", "client_credentials");

                request.AddParameter("client_id", API_KEY);

                request.AddParameter("client_secret", SECRET_KEY);

                IRestResponse response = client.Execute(request);

                var result = JsonConvert.DeserializeObject<dynamic>(response.Content);

                return result.access_token.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
