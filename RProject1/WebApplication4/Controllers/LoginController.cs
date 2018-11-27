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
    public class LoginController : ApiController
    {
        private readonly sistemarecomendacionEntities _db = new sistemarecomendacionEntities();

        [Route("PueblosMagicos/Login")]
        public RespuestaLogin PostLogon(Login request)
        {
            RespuestaLogin response;
            try
            {
                usuario usuarioRegistrado = _db.usuario.Where(x => x.correo.Equals(request.correo)).FirstOrDefault();
                if (usuarioRegistrado != null)
                {
                    string contrasena = _db.usuario.Where(x => x.id_Usuario.Equals(usuarioRegistrado.id_Usuario)).Select(x => x.contrasena).FirstOrDefault();
                    if (contrasena == request.contrasena)
                    {
                        response = new RespuestaLogin()
                        {
                            status = 200,
                            mensaje = "Usuario Encontrado",
                            idViajero = usuarioRegistrado.id_Usuario,
                            nombre = usuarioRegistrado.nombre,
                            fechaNacimiento = usuarioRegistrado.fecha_nacimiento.ToShortDateString(),
                            correo = usuarioRegistrado.correo,
                            gustos = usuarioRegistrado.tipo_atractivo.Select(x=>x.id_Tipo_atractivo).ToList()
                            //Where(x=>x.usuario.Equals(usuarioRegistrado)).Select(x=>x.id_Tipo_atractivo).ToList(),  
                        };
                        return response;

                    }

                    response = new RespuestaLogin()
                    {
                        status = 404,
                        mensaje = "Contraseña Incorrecta",
                    };
                    return response;
                }
                else
                {
                    response = new RespuestaLogin()
                    {
                        status = 404,
                        mensaje = "Usuario no Encontrado",
                    };
                    return response;
                }
            }
            catch (Exception e)
            {
                response = new RespuestaLogin()
                {
                    status = 404,
                    mensaje = "Usuario no Encontrado",
                };
                return response;
            }

        }

        [Route("PueblosMagicos/Registro")]
        public Respuesta PostRegistro([FromBody]RegistroRequest nuevoUsuario)
        {
            Respuesta response;
            try
            {
                usuario n = _db.usuario.Where(x => x.correo.Equals(nuevoUsuario.correo)).FirstOrDefault();

                if (_db.usuario.Where(x => x.correo.Equals(nuevoUsuario.correo)).FirstOrDefault() == null) {

                    usuario nuevo = new usuario
                    {
                        nombre = nuevoUsuario.nombre,
                        fecha_nacimiento = DateTime.Parse(nuevoUsuario.fechaNacimiento),
                        correo = nuevoUsuario.correo,
                        contrasena = nuevoUsuario.contrasena,
                        //tipo_atractivo = _db.tipo_atractivo.Where(x=>x.id_Tipo_atractivo.Equals(nuevoUsuario.gustos)).ToList()                          
                    };

                    List<tipo_atractivo> list = new List<tipo_atractivo>();
                    foreach(int y in nuevoUsuario.gustos)
                    {
                        list.Add(_db.tipo_atractivo.Where(x => x.id_Tipo_atractivo.Equals(y)).FirstOrDefault());
                    }

                    nuevo.tipo_atractivo = list;

                    _db.usuario.Add(nuevo);
                    _db.SaveChanges();

                    return response = new Respuesta()
                    {
                        status = 200,
                        mensaje = "Registro exitoso"
                    };
                
                }
                else
                {
                    return response = new Respuesta()
                    {
                        status = 202,
                        mensaje = "Ya existe cuenta vinculada a ese correo"
                    };
                }
            }
            catch (Exception e)
            {
                return response = new Respuesta()
                {
                    status = 404,
                    mensaje = "Error de registro"
                };
            }
        }
    }
}
