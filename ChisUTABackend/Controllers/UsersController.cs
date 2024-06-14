using ChisUTABackend.Data;
using ChisUTABackend.Models;
using ChisUTABackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}