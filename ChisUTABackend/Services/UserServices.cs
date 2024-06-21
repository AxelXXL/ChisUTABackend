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

        #region Registro
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
        #endregion

        #region Login

        public ChisUtaResponse Login(Users userLogged)
        {
            try
            {
                string encryptedPassword = Security.Encrypt(userLogged.Password);

                var filter = Builders<Users>.Filter.And(Builders<Users>.Filter.Eq(u => u.Email, userLogged.Email),
                    Builders<Users>.Filter.Eq(u => u.Password, encryptedPassword));

                var existingUser = _users.Find(filter).FirstOrDefault();

                if (existingUser == null)
                {
                    return new ChisUtaResponse
                    {
                        Success = false,
                        Message = "Credenciales inválidas.",
                        Data = null
                    };
                }
                else
                {
                    return new ChisUtaResponse
                    {
                        Success = true,
                        Message = "Usuario logueado con éxito.",
                        Data = new Users
                        {
                            Id = existingUser.Id,
                            Email = existingUser.Email,
                            Name = existingUser.Name
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ChisUtaResponse
                {
                    Success = false,
                    Message = "Ocurrió un error durante el inicio de sesión.",
                    Data = ex.Message 
                };
            }
        }


        #endregion
    }
}