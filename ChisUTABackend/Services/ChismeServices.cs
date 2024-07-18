using ChisUTABackend.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;


namespace ChisUTABackend.Services
{
    public class ChismeServices : BaseServices
    {

        #region Configurations
        private readonly IMongoCollection<ChismeModel> _chismeModel;

        public ChismeServices()
        {

            _chismeModel = _database.GetCollection<ChismeModel>("Chismes");
        }

        #endregion

        #region Get
        // obtener todos los chismes de la coleccion
        public ChisUtaResponse GetAllChismes()
        {
            ChisUtaResponse response = new ChisUtaResponse();

            var chismeFound = _chismeModel.Find(chisme => true).ToList();

            response = new ChisUtaResponse()
            {
                Success = true,
                Message = "Se obtuvo el chisme.",
                Data = chismeFound
            };

            return response;
        }

        // obtener un solo chisme
        public ChisUtaResponse GetOneChisme(string id)
        {
            ChisUtaResponse response = new ChisUtaResponse();

            var chismeFound = _chismeModel.Find(chisme => chisme.Id == id).FirstOrDefault();

            response = new ChisUtaResponse()
            {
                Success = true,
                Message = "Se obtuvo el chisme.",
                Data = chismeFound
            };

            return response;
        }

        public ChisUtaResponse GetByCategory(string category)
        {
            ChisUtaResponse response = new ChisUtaResponse();

            var builder = Builders<ChismeModel>.Filter;
            var filtro = builder.AnyEq(f => f.Categorias, category);
            var chismes = _chismeModel.Find(filtro).ToList();

            response = new ChisUtaResponse()
            {
                Success = true,
                Message = "Se obtuvo el chisme.",
                Data = chismes
            };

            return response;

        }

        #endregion

        #region Post
        // crear un nuevo chisme
        public ChisUtaResponse PostChisme(ChismeModel chisme)
        {
            ChisUtaResponse response = new ChisUtaResponse();

            if (chisme != null)
            {
                if (chisme.Titulo == null)
                {
                    response = new ChisUtaResponse()
                    {
                        Success = false,
                        Message = "Falta proporcionar el título"
                    };

                    return response;
                }
                if (chisme.Contexto == null)
                {
                    response = new ChisUtaResponse()
                    {
                        Success = false,
                        Message = "Falta proporcionar el contexto"
                    };

                    return response;
                }
                if (chisme.Categorias == null)
                {
                    response = new ChisUtaResponse()
                    {
                        Success = false,
                        Message = "Falta proporcionar la categoria"
                    };

                    return response;
                }

                _chismeModel.InsertOne(chisme);
                response = new ChisUtaResponse()
                {
                    Success = true,
                    Message = "Chisme guardado correctamente."
                };
            }
           
            return response;

        }

        // actualizar un chisme
        public ChisUtaResponse UpdateChisme(string id, ChismeModel chismeactualizado)
        {
            ChisUtaResponse response = new ChisUtaResponse();

            var update = Builders<ChismeModel>.Update
                .Set(chisme => chisme.Titulo, chismeactualizado.Titulo)
                .Set(chisme => chisme.Contexto, chismeactualizado.Contexto)
                .Set(chisme => chisme.Categorias, chismeactualizado.Categorias);

            _chismeModel.UpdateOne(chisme => chisme.Id == id, update);
            response = new ChisUtaResponse()
            {
                Success = true,
                Message = "El chisme se actualizo correctamente."
            };

            return response;
        }

        #endregion

        #region Delete
        // eliminar un chisme
        public ChisUtaResponse DeleteChisme(string id)
        {
            ChisUtaResponse response = new ChisUtaResponse();

            _chismeModel.DeleteOne(chisme => chisme.Id == id);

            response = new ChisUtaResponse()
            {
                Success = true,
                Message = "El chisme se elimino correctamente."
            };

            return response;
        }

        #endregion



    }
}