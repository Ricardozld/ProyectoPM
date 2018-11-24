using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.BaseDatos;
namespace WebApplication4.Models
{
    public class RespuestaVisitas
    {
        public int status { get; set; }
        public List<PueblosMagicos> pueblosMagicos { get; set; }
    }
}