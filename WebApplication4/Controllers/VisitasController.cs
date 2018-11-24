using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication4.BaseDatos;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class VisitasController : ApiController
    {
        private readonly sistemarecomendacionEntities _db = new sistemarecomendacionEntities();
        // GET: api/Visitas
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Visitas/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Visitas
        [Route("PueblosMagicos/Consultar")]
        public Respuesta PostConsultar(int idViajero)
        {
            Respuesta response;
            try
            {
                if (_db.historial_visitas.Where(x => x.id_Usuario.Equals(idViajero)) != null)
                {

                    response = new Respuesta()
                    {
                        status = 200,
                        mensaje = "OK",
                    };
                    return response;
                }
                else
                {
                    response = new Respuesta()
                    {
                        status = 404,
                        mensaje = "Aún no has visitado ningún pueblo mágico",
                    };
                    return response;
                }
            }
            catch
            {
                response = new Respuesta()
                {
                    status = 404,
                    mensaje = "Aún no has visitado nungún pueblo mágico",
                };
                return response;
            }

        }

        // PUT: api/Visitas/5
        public RespuestaVisitas PostDescargar(int idViajero)
        {
            try
            {
                RespuestaVisitas response =
                new RespuestaVisitas()
                {
                    status = 200,
                    pueblosMagicos = _db.historial_visitas.Where(x => x.id_Usuario.Equals(idViajero)).Select(x => new PueblosMagicos()
                    {
                        idPuebloMagico = x.id_Pueblo_magico,
                        nombrePuebloMagico = x.pueblo_magico.nombre,
                        fotos = _db.fotos.Where(y => y.id_Usuario.Equals(idViajero)).Select(y => y.foto).ToList()

                    }).ToList()

                };
                return response;
            }
            catch (Exception e)
            {
                return new RespuestaVisitas()
                {
                    status = 404
                };
            }

             
        }
    }
}
