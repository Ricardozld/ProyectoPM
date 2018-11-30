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
    public class ActualizarController : ApiController
    {
        // GET: api/Actualizar
        private readonly sistemarecomendacionEntities _db = new sistemarecomendacionEntities();
        // POST: api/Actualizar
        [Route("PueblosMagicos/Actualizar")]
        public Respuesta PostActualizar([FromBody]ActualizarRequest request)
        {
            try
            {
                usuario usuario = _db.usuario.Where(x => x.id_Usuario.Equals(request.idViajero)).FirstOrDefault();
                usuario.fecha_nacimiento = DateTime.Parse(request.fechaNacimiento);
                usuario.nombre = request.nombre;
                List<tipo_atractivo> lista = usuario.tipo_atractivo.ToList();

                foreach (tipo_atractivo t in lista)
                {
                    usuario.tipo_atractivo.Remove(t);
                }

                foreach (int y in request.idAtractivos)
                {
                    usuario.tipo_atractivo.Add(_db.tipo_atractivo.Where(x => x.id_Tipo_atractivo.Equals(y)).FirstOrDefault());
                }

                _db.SaveChanges();

                return new Respuesta
                {
                    status = 200,
                    mensaje = "Cambios realizados"
                };
                    }
            catch(Exception e)
            {
                return new Respuesta
                {
                    status = 404,
                    mensaje = "Error"
                };

            }
            
        }
       
    }
}
