using ChisUTABackend.Models;
using ChisUTABackend.Services;
using Microsoft.Win32;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace ChisUTABackend.Controllers
{
    public class TokenController : ApiController
    {
        [Route("api/GetToken")]
        [HttpGet]
        public HttpResponseMessage GenerateNewToken(Guid ID_App)
        {
            TokenResponseModel token = new TokenResponseModel() { Token = Security.GenerateNewToken(ID_App) };

            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(token));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;
        }
    }
}