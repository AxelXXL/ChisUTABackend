using ChisUTABackend.Models;
using ChisUTABackend.Services;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ChisUTABackend.Controllers
{
    public class UsersController : ApiController
    {

        #region Configurations
        private UserServices _userServices;

        public UsersController()
        {
            _userServices = new UserServices();
        }
        #endregion

        [Auth]
        [Route("api/RegisterUser", Name = "RegisterUser")]
        [HttpPost]
        public HttpResponseMessage RegisterUser(Users user)
        {
            ChisUtaResponse register = _userServices.RegisterUser(user);

            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(register));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;
        }

        [Auth]
        [Route("api/Login", Name = "Login")]
        [HttpPost]
        public HttpResponseMessage Login(Users users)
        {
            ChisUtaResponse login = _userServices.Login(users);

            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(login));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;
        }
    }
}