using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class RecomendacionRespuesta
    {
        public int status { get; set; }
        public List<PueblosMagicosRecomendacion> pueblosMagicos { get; set; }
    }
}