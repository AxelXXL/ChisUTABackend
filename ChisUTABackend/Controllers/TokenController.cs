using ChisUTABackend.Models;
using ChisUTABackend.Services;
using System;
using System.Web.Http;

namespace ChisUTABackend.Controllers
{
    public class TokenController : ApiController
    {
        [Route("api/GetToken")]
        [HttpGet]
        public TokenResponseModel GenerateNewToken(Guid ID_App)
        {
            return new TokenResponseModel() { Token = Security.GenerateNewToken(ID_App) };
        }
    }
}