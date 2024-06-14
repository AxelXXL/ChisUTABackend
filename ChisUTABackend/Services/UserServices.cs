using ChisUTABackend.Data;
using ChisUTABackend.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChisUTABackend.Services
{
    public class UserServices : BaseServices
    {

        #region Configurations 
        private readonly IMongoCollection<Users> _users;

        public UserServices()
        {
            _users = _database.GetCollection<Users>("Users");
            _context = new MongoDbContext();
        }
        #endregion


        public ChisUtaResponse RegisterUser(Users newUser)
        {
            var filter = Builders<Users>.Filter.And( Builders<Users>.Filter.Exists("Email"), Builders<Users>.Filter.Eq("Email", newUser.Email));
            var existingUser = _users.Find(filter).FirstOrDefault();

            if (existingUser != null)
            {
                return new ChisUtaResponse()
                {
                    Success = false,
                    Message = "El usuario ya se encuentra existente.",
                    Data = null
                };
            }
            else
            {
                if(newUser.Password == newUser.ConfirmPassword)
                {
                    newUser.Password = Security.Encrypt(newUser.Password);
                }
                else
                {
                    return new ChisUtaResponse()
                    {
                        Success = false,
                        Message = "Las contraseñas no coinciden.",
                        Data = null
                    };
                }
            }

            _context.Users.InsertOne(newUser);
            return new ChisUtaResponse()
            {
                Success = true,
                Message = "Usuario registrado con exito.",
                Data = new Users()
                {
                    Id = newUser.Id,
                    Email = newUser.Email,
                    Name = newUser.Name,
                }
            };
        }
    }
}