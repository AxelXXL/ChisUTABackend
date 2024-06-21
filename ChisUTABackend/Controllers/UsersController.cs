using ChisUTABackend.Models;
using ChisUTABackend.Services;
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
        public ChisUtaResponse RegisterUser(Users user)
        {
            return _userServices.RegisterUser(user);
        }

        [Auth]
        [Route("api/Login", Name = "Login")]
        [HttpPost]
        public ChisUtaResponse Login(Users users)
        {
            return _userServices.Login(users);
        }
    }
}