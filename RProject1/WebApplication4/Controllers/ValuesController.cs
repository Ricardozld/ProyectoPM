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
            int user = 4;
            Environment.SetEnvironmentVariable("PATH", @"C:\Program Files\R\R-3.3.2\bin\i386");
            REngine.SetEnvironmentVariables(@"C:\Program Files\R\R-3.3.2\bin\i386", @"C:\Program Files\R\R-3.3.2");
            REngine engine = REngine.GetInstance();
            Dictionary<string, List<double>> LiqProductionData = new Dictionary<string, List<double>>();

            IList<hstorial_atractivos> calificacion = _db.hstorial_atractivos.ToList();

            IEnumerable[] columns = new IEnumerable[3];
            columns[0] = new int[] { 1, 1, 1, 1, 2, 2, 2, 3,3,3,3,4,4,5,5,5};
            columns[1] = new int[] { 1,2,3,4,1,2,3,1,2,3,4,2,3,1,3,4 };
            columns[2] = new double[] {5,3.3,5,3.9,4.2,2.0,2.2,5,4,1,4.9,4.3,3.2,2.7,3,5};
            //IEnumerable[] columns = new IEnumerable[3];
            //columns[0] = calificacion.Select(x => x.id_Atractivo).ToArray();
            //columns[1] = calificacion.Select(x => x.id_Usuario).ToArray();
            //columns[2] = calificacion.Select(x => x.promedio).ToArray();

            string[] columnNames = new[] { "atractivo", "usuario", "valoracion" };
            // IEnumerator<MatrizCalificaciones>

            //IEnumerable[] atractivos = _db


            //var calif = (IEnumerable[])calificacion.Select(x => x.promedio.Value);
            //var DataFrameColumns = new List<IEnumerable>(calificacion.Select(x=>x.promedio.Value));

            DataFrame calificaciones = engine.CreateDataFrame(columns, columnNames: columnNames);
            engine.SetSymbol("calificaciones", calificaciones);

            //engine.Evaluate("library(tidyverse)");
            engine.Evaluate("library(dplyr)");
            engine.Evaluate("library(gsheet)");
           // engine.Evaluate("library(recommenderlab)");
            //engine.Evaluate("library(purrr)");
            engine.Evaluate(@"usuarios_excluidos <- calificaciones %>% filter(!is.na(valoracion)) %>% group_by(usuario) %>% count() %>% filter(n < 15) %>% pull(usuario)");

            engine.Evaluate(@"calificaciones <- calificaciones %>% filter(!usuario %in% usuarios_excluidos)");

            engine.Evaluate(@"valoraciones_usuarios <- calificaciones %>%
                         spread(key = usuario, value = valoracion, fill = NA)");

            engine.Evaluate(@"funcion_correlacion < -function(x, y){
                correlacion < -cor(x, y, use = '" + "na.or.complete" + "', method = '" + "pearson" +
                "' return (correlacion)}");

            engine.Evaluate(@"similitud_usuarios <- map_dbl(.x = valoraciones_usuarios[, -1],
                              .f = funcion_correlacion,
                              y = valoraciones_usuarios[,'" + user + "']) ");

            engine.Evaluate(@"similitud_usuarios <- data_frame(usuario = names(similitud_usuarios),
                                 similitud = similitud_usuarios) %>%
                      arrange(desc(similitud))");

            engine.Evaluate(@"atractivos_vistos <- calificaciones %>%
                    filter(usuario == " + user + @"& !is.na(valoracion)) %>%
                    pull(atractivo)");

            engine.Evaluate(@"atractivo_no_visto < -calificaciones %>%
                       filter(usuario == " + user + @" & is.na(valoracion)) %>%
                       pull(atractivo)");
            engine.Evaluate("prediccion < -rep(NA, length(atractivo_no_visto))");
            engine.Evaluate("pelicula < -rep(NA, length(atractivo_no_visto))");
            engine.Evaluate("n_obs_prediccion < -rep(NA, length(atractivo_no_visto))");

            engine.Evaluate(@"for (i in seq_along(atractivo_no_visto))
            {usuarios_atractivo_i < -valoraciones_tidy %>%
                                       filter(atractivo == atractivo_no_visto[i] &
                                              !is.na(valoracion)) %>% pull(usuario)



            usuarios_atractivo_i < -valoraciones_tidy %>%
                                       filter(atractivo == atractivo_no_visto[i] &
                                              !is.na(valoracion)) %>% pull(usuario)

            if (length(usuarios_atractivo_i) < 10)
                {
                    next()
                    }

            top_15_usuarios < -similitud_usuarios %>%
                               filter(similitud >= 0 & (usuario %in% usuarios_atractivo_i)) %>%
                               arrange(desc(similitud)) %>%
                               head(15)

            if (nrow(top_15_usuarios) < 10)
                        {
                            next()
              }

            valoraciones_top_15 < -valoraciones_tidy %>%
                                   filter(atractivo == atractivo_no_visto[i] &
                                          usuario %in% top_15_usuarios$usuario)


            top_15_usuarios < -top_15_usuarios %>% left_join(valoraciones_top_15,
                                                   by = '" + "usuario" +"')"+
            @"prediccion[i] < -sum(top_15_usuarios$similitud * top_15_usuarios$valoracion) /
                   sum(top_15_usuarios$similitud)
            atractivo[i] < -atractivo_no_visto[i]
  n_obs_prediccion[i] < -nrow(top_15_usuarios)
}");

            engine.Evaluate(@"top10_recomendaciones<- data.frame(pelicula, prediccion, n_obs_prediccion) %>% 
                         arrange(desc(prediccion)) %>%
                         head(10)");
     


            //PredictedData = engine.Evaluate("PredictedData").AsDataFrame();


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
