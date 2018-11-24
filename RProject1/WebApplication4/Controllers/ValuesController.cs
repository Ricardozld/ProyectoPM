using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RDotNet;
using WebApplication4.BaseDatos;
using WebApplication4.Models.Recomendación;

namespace WebApplication4.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly sistemarecomendacionEntities _db = new sistemarecomendacionEntities();
        // GET api/values
        public string Get()
        {
            Environment.SetEnvironmentVariable("PATH", @"C:\Program Files\Microsoft\R Client\R_SERVER\bin\i386");
            REngine.SetEnvironmentVariables(@"C:\Program Files\Microsoft\R Client\R_SERVER\bin\i386", @"C:\Program Files\Microsoft\R Client\R_SERVER");
            REngine engine = REngine.GetInstance();
            Dictionary<string, List<double>> LiqProductionData = new Dictionary<string, List<double>>();

            IList<hstorial_atractivos> calificacion = _db.hstorial_atractivos.ToList();

            IEnumerable[] columns = new IEnumerable[3];
            columns[0] = calificacion.Select(x => x.id_Atractivo).ToArray();
            columns[1] = calificacion.Select(x => x.id_Usuario).ToArray();
            columns[2] = calificacion.Select(x => x.promedio).ToArray();

            // IEnumerator<MatrizCalificaciones>

            //IEnumerable[] atractivos = _db


            //var calif = (IEnumerable[])calificacion.Select(x => x.promedio.Value);
            //var DataFrameColumns = new List<IEnumerable>(calificacion.Select(x=>x.promedio.Value));


            DataFrame calificaciones = engine.CreateDataFrame(columns);



            //MatrizCalificaciones matriz = _db.calificacion.Select(x => x.atractivo_has_criterios_evaluacion).Select(x => new MatrizCalificaciones()
            //{

            //    idAtractivo = x.id_Atractivo
            //    //alificacion = x.calificacion.Where(x => x.id_Atractivo.Equals(idAtractivo))

            //});


            engine.Initialize();
            //var matriz = _db.
            var matrizCal = engine.GetSymbol("");
                string[] a = engine.Evaluate("'Hi there .NET, from the R engine'").AsCharacter().ToArray();
            return a[0];

        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
