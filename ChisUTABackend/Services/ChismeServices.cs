using ChisUTABackend.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace ChisUTABackend.Services
{
    public class ChismeServices
    {
        #region Configurations 
        private readonly IMongoCollection<ChismeModel> _chismes;
        private readonly MongoDbContext _context;

        public ChismeServices()
        {
            _context = new MongoDbContext();
            _chismes = _context.Database.GetCollection<ChismeModel>("Chismes");
        }
        #endregion

        public ChisUtaResponse PostChisme(ChismeModel chisme)
        {
            _chismes.InsertOne(chisme);
            return new ChisUtaResponse
            {
                Success = true,
                Message = "Chisme registrado con éxito",
                Data = chisme
            };
        }

        public ChismeModel GetOneChisme(string id)
        {
            return _chismes.Find(ch => ch.Id == id).FirstOrDefault();
        }

        public List<ChismeModel> GetAllChismes()
        {
            return _chismes.Find(ch => true).ToList();
        }

        public void DeleteChisme(string id)
        {
            _chismes.DeleteOne(ch => ch.Id == id);
        }

        public ChismeModel UpdateChisme(string id, ChismeModel chisme)
        {
            _chismes.ReplaceOne(ch => ch.Id == id, chisme);
            return chisme;
        }

        public List<ChismeModel> GetByCategory(string category)
        {
            return _chismes.Find(ch => ch.Categorias.Contains(category)).ToList();
        }
    }
}
