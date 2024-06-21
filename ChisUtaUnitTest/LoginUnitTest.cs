using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ChisUTABackend.Services;
using ChisUTABackend.Models;

namespace ChisUtaUnitTest
{
    [TestClass]
    public class LoginUnitTest
    {
        [TestMethod]
        public void T001_Register()
        {
            UserServices userServices = new UserServices();

            Users newUser = new Users()
            {
                Name = "Axel",
                Email = "emaildeprueba@gmail.com",
                Password = "unacontraseña@",
                ConfirmPassword = "unacontraseña@"
            };

            ChisUtaResponse response = userServices.RegisterUser(newUser);

            Assert.AreEqual(true, response.Success, "Ok");
        }
    }
}
