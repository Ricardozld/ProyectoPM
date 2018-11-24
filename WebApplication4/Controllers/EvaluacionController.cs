using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication4.Models;
using WebApplication4.BaseDatos;

namespace WebApplication4.Controllers
{
    public class EvaluacionController : ApiController
    {
        private readonly sistemarecomendacionEntities _db = new sistemarecomendacionEntities();
        [Route("PueblosMagicos/Evaluar")]
        // GET: api/Evaluacion
        public Respuesta PostAsignar([FromBody]EvaluacionRequest request)
        {
            try
            {
                historial_visitas registroh = new historial_visitas();
                hstorial_atractivos registroa = new hstorial_atractivos();

                registroh.id_Usuario = request.idViajero;
                registroh.id_Pueblo_magico = request.idPuebloMagico;
                registroh.fecha_inicio = DateTime.Now;
                registroh.primedio_visita = request.promedio;

                List<calificacion> registroc = request.evaluaciones.Select(x => new calificacion
                {
                    id_Usuario = request.idViajero,
                    id_Pueblo_magico = request.idPuebloMagico,
                    fecha_inicio = registroh.fecha_inicio,
                    calificacion1 = x.calificacion,
                    id_Atractivo = x.idAtractivo,
                    id_Criterios_evaluacion = x.idCriterio
                }).ToList();

                foreach (int x in request.evaluaciones.Select(x => x.idAtractivo).Distinct())
                {
                    registroa.id_Usuario = request.idViajero;
                    registroa.id_Atractivo = x;
                    registroa.promedio = request.evaluaciones.Where(y => y.idAtractivo.Equals(x)).Average(y => y.calificacion);
                    _db.hstorial_atractivos.Add(registroa);
                }

                foreach (calificacion c in registroc)
                {
                    _db.calificacion.Add(c);
                }

                _db.historial_visitas.Add(registroh);
                _db.SaveChanges();

                //request.evaluaciones.Average(x=>x.calificacion);
                return new Respuesta
                {
                    status = 200,
                    mensaje = "Recepción correcta de calificaciones"
                };
                }
            catch(Exception e)
            {
                return new Respuesta
                {
                    status = 404,
                    mensaje = e.Message
                };
            }
        }
    }
}

        