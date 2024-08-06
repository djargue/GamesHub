using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using BackendProyectoFinal.Entities;
using System.Reflection;

namespace APIS.Controllers
{
    [ApiController]
    [Route("Usuarios")]

    public class UsuarioController : ControllerBase
    {

        [HttpGet]
        [Route("traerUsuarios")]
        public dynamic listarUsuario()
        {
            List<Usuario> usuarios = new List<Usuario> {
                new Usuario{
                    identificadorUsuario = 5,
                    nombre = ""
                }
            };

            return User;
        }


        [HttpPost]
        [Route("CrearUsuario")]
        public dynamic RegistrarUsuario(Usuario usuario) {

            // Verificar que sea valido el usuario
            if (!ModelState.IsValid)
            {
                return BadRequest("Datos No Proporcionados");
            }

            // Crea la instancia de ApplicationUser con los datos del modelo
            var user = new ApplicationUser {
                UsuarioController = usuario.correo,
                EmailTokenProvider = usuario.correo,
                FechaNacimiento = usuario.fechaNacimiento,
                FotoPerfil = usuario.fotoPerfil,
                Estatus = usuario.estatus
            };

            var result = await _userManager.CreateAsync(user, usuario.contrasena);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Usuario Creado Correctamente" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return BadRequest(ModelState);
        }



    }
}
