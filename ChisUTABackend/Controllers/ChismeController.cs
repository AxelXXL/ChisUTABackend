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
        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Falta el parámetro ID para hacer la consulta");
            }

            var chisme = _chismeServices.GetOneChisme(id);

            if (chisme == null)
            {
                return Content(HttpStatusCode.NotFound, "No hay coincidencias en la BD");
            }

            _chismeServices.DeleteChisme(id);
            return Ok("Chisme eliminado");

        }



        [Route("Get-Chismes")]
        [HttpGet]
        public IHttpActionResult GetChismes()
        {
            var chismes = _chismeServices.GetAllChismes();
            if (chismes != null || chismes.Count > 0)
            {
                return Ok(chismes);
            }
            return Content(HttpStatusCode.NotFound, "Aun no hay registros en la coleccion Chismes");

        }



        [Route("Get-One-Chisme")]
        [HttpGet]
        public IHttpActionResult GetChisme(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Falta el parámetro ID para hacer la consulta");
            }
            var chisme = _chismeServices.GetOneChisme(id);
            if (chisme != null)
            {
                return Ok(chisme);
            }
            return Content(HttpStatusCode.NotFound, "No hay coincidencias en la base de datos");

        }



        [Route("Update-Chisme")]
        [HttpPut]
        public IHttpActionResult Update(string id, ChismeModel datos)
        {
            if (string.IsNullOrEmpty(id) || datos == null)
            {
                return BadRequest("Parámetros incompeltos");
            }
            var chismefound = _chismeServices.GetOneChisme(id);
            if (chismefound != null)
            {
                chismefound.Titulo = string.IsNullOrEmpty(datos.Titulo) ? chismefound.Titulo : datos.Titulo;
                chismefound.Contexto = string.IsNullOrEmpty(datos.Contexto) ? chismefound.Contexto : datos.Contexto;
                chismefound.Categorias = datos.Categorias ?? chismefound.Categorias;

                var updatedChisme = _chismeServices.UpdateChisme(id, chismefound);
                return Ok(updatedChisme);

            }
            return Content(HttpStatusCode.NotFound, "No hubo coincidencias en la BD");
        }


        [Route("Category-search")]
        [HttpGet]
        public IHttpActionResult GetCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return BadRequest("No se proporcionó la categoria");
            }
            var chismes = _chismeServices.GetByCategory(category);

            if (chismes == null || chismes.Count == 0)
            {
                return Content(HttpStatusCode.NotFound, "No hay chismes de dicha categoria");
            }

            return Ok(chismes);

        }

    }
}