using ChisUTABackend.Models;
using ChisUTABackend.Services;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ChisUTABackend.Controllers
{
    public class ChismeController : ApiController
    {
        #region Configurations

        private readonly ChismeServices _chismeServices;

        public ChismeController()
        {
            _chismeServices = new ChismeServices();
        }
        #endregion

        [Route("Post-Chisme")]
        [HttpPost]
        public HttpResponseMessage Post(ChismeModel chisme)
        {
            ChisUtaResponse postChisme = _chismeServices.PostChisme(chisme);

            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(postChisme));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;
        }



        [Route("Delete-Chisme")]
        [HttpPost]
        public HttpResponseMessage Delete(string id)
        {
            ChisUtaResponse deleteChisme = _chismeServices.DeleteChisme(id);

            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(deleteChisme));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;
        }



        [Route("Get-Chismes")]
        [HttpGet]
        public HttpResponseMessage GetChismes()
        {
            ChisUtaResponse getChismes = _chismeServices.GetAllChismes();

            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(getChismes));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;

        }



        [Route("Get-One-Chisme")]
        [HttpGet]
        public HttpResponseMessage GetChisme(string id)
        {
            ChisUtaResponse getChisme = _chismeServices.GetOneChisme(id);

            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(getChisme));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;

        }



        [Route("Update-Chisme")]
        [HttpPut]
        public HttpResponseMessage Update(string id, ChismeModel datos)
        {
            ChisUtaResponse updateChisme = _chismeServices.UpdateChisme(id, datos);

            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(updateChisme));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;
        }


        [Route("Category-search")]
        [HttpGet]
        public HttpResponseMessage GetCategory(string category)
        {
            ChisUtaResponse getCategory = _chismeServices.GetByCategory(category);

            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(getCategory));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;

        }

    }
}