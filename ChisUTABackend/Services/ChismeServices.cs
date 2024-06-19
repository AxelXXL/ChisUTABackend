using ChisUTABackend.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;


namespace ChisUTABackend.Services
{
    public class ChismeServices
    {
        private readonly IMongoCollection<ChismeModel> _chismeModel;

        public ChismeServices()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MongoDbConnection"].ConnectionString;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Db_ChisUTA");
            _chismeModel = database.GetCollection<ChismeModel>("Chismes");
        }

        // obtener todos los chismes de la coleccion
        public List<ChismeModel> GetAllChismes()
        {
            return _chismeModel.Find(chisme => true).ToList();
        }

        // obtener un solo chisme
        public ChismeModel GetOneChisme(string id)
        {
            var chismeFound =_chismeModel.Find(chisme => chisme.Id == id).FirstOrDefault();
            return chismeFound;
        }

        // crear un nuevo chisme
        public ChismeModel PostChisme(ChismeModel chisme)
        {
            _chismeModel.InsertOne(chisme);
            return chisme;

        }

        // eliminar un chisme
        public void DeleteChisme(string id)
        {
            _chismeModel.DeleteOne(chisme=> chisme.Id == id);
        }

        // actualizar un chisme
        public ChismeModel UpdateChisme(string id, ChismeModel chismeactualizado)
        {
            chismeactualizado.Id = id;
            _chismeModel.ReplaceOne(chisme => chisme.Id == id, chismeactualizado);
            return chismeactualizado;
        }



    }
}