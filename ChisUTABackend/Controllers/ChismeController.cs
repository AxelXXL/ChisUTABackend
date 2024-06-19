using ChisUTABackend.Models;
using ChisUTABackend.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
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
            _chismeServices.PostChisme(chisme);
            if (ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<ChismeModel>(chisme, new JsonMediaTypeFormatter())
                };
            }
            return new HttpResponseMessage(HttpStatusCode.Conflict)
            {
                Content = new StringContent("Los datos no cumplen con el modelo")
            };
        }

        [Route("Delete-Chisme")]
        [HttpDelete]
        public HttpResponseMessage Delete(string id)
        {
            if (id != null)
            {
                _chismeServices.DeleteChisme(id);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Chisme eliminado")
                };
            }

            return new HttpResponseMessage(HttpStatusCode.Conflict)
            {
                Content = new StringContent("parametro id necesario")
            };

        }


        [Route("Get-Chismes")]
        [HttpGet]
        public HttpResponseMessage GetChismes()
        {
            var chismes = _chismeServices.GetAllChismes();
            if (chismes != null || chismes.Count > 0)
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<ChismeModel>>(chismes, new JsonMediaTypeFormatter())
                };
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);

        }

        [Route("Get-One-Chisme")]
        [HttpGet]
        public HttpResponseMessage GetChisme(string id)
        {
            var chisme = _chismeServices.GetOneChisme(id);
            if (chisme != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<ChismeModel>(chisme, new JsonMediaTypeFormatter())
                };
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);

        }

        [Route("Update-Chisme")]
        [HttpPut]
        public HttpResponseMessage Update(string id, ChismeModel chismeactualizado)
        {
            if (id != null && chismeactualizado != null)
            {
                _chismeServices.UpdateChisme(id, chismeactualizado);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<ChismeModel>(chismeactualizado, new JsonMediaTypeFormatter())
                };
            }
            return new HttpResponseMessage(HttpStatusCode.Conflict)
            {
                Content = new StringContent("Los datos no fueron dados correctamente")
            };
        }

    }
}