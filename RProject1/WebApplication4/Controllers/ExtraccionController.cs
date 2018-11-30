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
    public class ExtraccionController : ApiController
    {
        private readonly sistemarecomendacionEntities _db = new sistemarecomendacionEntities();
        // GET: api/Extraccion

        [Route("PueblosMagicos/Detalles")]
        // GET: api/Extraccion/5
        public DetallesRespuesta PostDetalles([FromBody]DetallesRequest request)
        {
            try
            {
                DetallesRespuesta response = _db.pueblo_magico.Where(x => x.id_Pueblo_magico.Equals(request.idPuebloMagico))
                    .Select(x => new DetallesRespuesta
                    {
                        status = 200,
                        mensaje = "Pueblo Mágico encontrado",
                        nombre = x.nombre,
                        descripcion = x.descripcion,
                        latitud = x.latitud,
                        longitud = x.longitud,
                        
                        //fotos = x.foto
                    }).FirstOrDefault();
                response.fotos = _db.fotos.Where(x => x.id_Pueblo_magico.Equals(request.idPuebloMagico)).Select(x => x.foto).ToList();
                response.atractivos = _db.atractivo.Where(x => x.id_Pueblo_magico.Equals(request.idPuebloMagico)).Select(x => new atractivoDetalles
                {
                    idAtractivo = x.id_Atractivo,
                    //categoria = _db.categoria.Select(y => y.tipo_atractivo.Where(w => w.id_Tipo_atractivo.Equals(x.id_Tipo_atractivo)).FirstOrDefault()).Select(y => y.id_Categoria).FirstOrDefault(),
                    categoria = _db.tipo_atractivo.Where(y=> y.id_Tipo_atractivo.Equals(x.id_Atractivo)).Select(y=>y.id_Categoria).FirstOrDefault(),
                    descripcion = x.descripcion,
                    foto = x.foto,
                    latitud = x.latitud,
                    longitud = x.longitud,
                    nombre = x.nombre
                    
                }).ToList();

                return response;
            }
            catch (Exception e)
            {
                DetallesRespuesta response;
                return response = new DetallesRespuesta { };
            }

        }

        [Route("PueblosMagicos/Recomendacion")]
        public RecomendacionRespuesta PostRecomendacion([FromBody]RecomendacionRequest request)
        {
            try
            {
                pueblo_magico pueblo1 = _db.pueblo_magico.Where(x => x.id_Pueblo_magico.Equals(1)).FirstOrDefault();
                pueblo_magico pueblo2 = _db.pueblo_magico.Where(x => x.id_Pueblo_magico.Equals(2)).FirstOrDefault();
                pueblo_magico pueblo3 = _db.pueblo_magico.Where(x => x.id_Pueblo_magico.Equals(3)).FirstOrDefault();
                pueblo_magico pueblo4 = _db.pueblo_magico.Where(x => x.id_Pueblo_magico.Equals(4)).FirstOrDefault();
                List<pueblo_magico> pueblos = new List<pueblo_magico>();
                pueblos.Add(pueblo1);
                pueblos.Add(pueblo2);
                pueblos.Add(pueblo3);
                pueblos.Add(pueblo4);

                RecomendacionRespuesta response = new RecomendacionRespuesta
                {
                    status = 200,
                    pueblosMagicos = pueblos.Select(x => new PueblosMagicosRecomendacion()
                    {
                        idPuebloMagico = x.id_Pueblo_magico,
                        nombre = x.nombre,
                        foto = x.foto
                    }).ToList()
                };
                return response;
            }
            catch
            {
                return new RecomendacionRespuesta();
            }

        }
    }
}
