using BackendProyectoFinal.DataAccess;
using BackendProyectoFinal.Entities;
using BackendProyectoFinal.Entities.Response;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Web;


namespace BackendProyectoFinal.Logica
{
    //Logica de Usuarios
    //Utiliza
    //// Crear Usuario
    //// Verificar Usuario Login
    public class LogicaUsuario
    {

        //
        //Metodo para crear usuario
        public ResUsuarioCrear UsuarioCrear(ReqUsuarioCrear req){
            
            //Instancia Respuesta
            ResUsuarioCrear res = new ResUsuarioCrear();
            
            res.resultado = false; //por default el resultado es falso a menos que se logre
            // Casos que genera error y la descripcion
            if (req == null) {
                res.descripcionError = "Req null";
            }
            else if (String.IsNullOrEmpty(req.user.contrasena)) {
                res.descripcionError = "Contrasena Vacia";
            }
            else if (req.user.fotoPerfil==null || req.user.fotoPerfil.Length == 0)
            {
                res.descripcionError = "Foto Perfil Vacia";
            }
            else if (String.IsNullOrEmpty(req.user.nombre)){
                res.descripcionError = "Nombre Vacio";
            }
            else if (req.user.fechaNacimiento==null){
                res.descripcionError = "Fecha Vacio";
            }
            else if (!CorreoValido(req.user.correo)){
                res.descripcionError = "Correo Vacio";
            }
            else { 
            
                //Todo fue correcto
                //Enviar a Linq

                //en caso que no se pueda conectar a la BD
                try
                {

                    long? identificadorUsuario = 0; //NULLEABLE
                    string errorDescripcion = null;
                    
                    // Hashing de la contraseña
                    string hashedPassword = HashPassword(req.user.contrasena);
                    string UsuarioSeguro = Seguridad(req.user.correo);


                    LinqConexionDataContext linq = new LinqConexionDataContext();
                    linq.SP_UsuarioCrear(req.user.nombre, req.user.fechaNacimiento, req.user.contrasena, req.user.fotoPerfil, UsuarioSeguro, ref identificadorUsuario, ref errorDescripcion);

                    //Verificacion del resultado de la base de datos
                    if (identificadorUsuario == -1){
                        res.descripcionError = "Usuario ya existe";
                    }
                    else{
                        res.resultado = true;
                    }
                }
                catch(Exception ex){
                    res.descripcionError = "Error de Conexion con la BASE DE DATOS" + ex.Message;
                }
            }
            return res;
        }
        //


        //
        //Metodo para verificar credenciales usuario
        public ResUsuarioVerificar verificarUsuario(ReqUsuarioVerificar req) {
            //Instancia Respuesta
            ResUsuarioVerificar res = new ResUsuarioVerificar();

            res.resultado = false; //Por default el resultado es falso
            //Casos que generan error y descripcion
            if (req == null)
            {
                res.descripcionError = "Req es null";
            }
            else if (String.IsNullOrEmpty(req.user.correo))
            {
                res.descripcionError = "No se ingreso un correo";
            }
            else if (string.IsNullOrEmpty(req.user.contrasena))
            {
                res.descripcionError = "No se ingreso un contrasena";
            }
            else {
                //Toda a informacion esta correcta
                //Se envia a linq

                try {

                    // Hashing de la contraseña proporcionada
                    string hashedPassword = HashPassword(req.user.contrasena);
                    string UsuarioSeguro = Seguridad(req.user.correo);
                    string errorDescripcion = null;

                    LinqConexionDataContext linq = new LinqConexionDataContext();
                    List<SP_UsuarioVerificarResult> resultSet = new List<SP_UsuarioVerificarResult>();
                    resultSet = linq.SP_UsuarioVerificar(UsuarioSeguro, hashedPassword, ref errorDescripcion).ToList();

                    var usuario = resultSet.First();


                    res.User.identificadorUsuario = (int)usuario.identificadorUsuario;
                    res.User.nombre = usuario.Nombre;
                    res.User.fechaNacimiento =usuario.FechaNacimiento;
                    res.User.correo =usuario.Correo;
                    res.User.fotoPerfil=usuario.FotoPerfil.ToArray();

                } catch (Exception ex) {
                    res.descripcionError = "Error en la conexion a la Base de Datos" + ex.Message;
                }
            }
            return res;
        }
        //







        //METODOS ADICIONALES
        // Correos Validos
        private bool CorreoValido(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        //Seguridad
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        // Seguridad
        private string Seguridad(string input)
        {
            // Escapa caracteres peligrosos
            return HttpUtility.HtmlEncode(input);
        }

    }
}
