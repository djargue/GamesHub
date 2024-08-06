using BackendProyectoFinal.DataAccess;
using BackendProyectoFinal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Logica
{
    //Logica de AmistadUsuarios
    //Utiliza
    //// Crear Amistad Usuario
    //// Eliminar Amistad
    //// Listar Amistad
 
    internal class LogicaAmistadUsuario
    {
        //
        //Metodo para Crear Amistad Usuario
        public ResAmistadUsuarioCrear AmistadUsuarioCrear(ReqAmistadUsuarioCrear req) {
            //Instancia de la respuesta
            ResAmistadUsuarioCrear res = new ResAmistadUsuarioCrear();

            res.resultado = false; //por default el resultado es falso a menos que se logre
            // Casos que genera error y la descripcion
            if (req == null)
            {
                res.descripcionError = "El req esta null";
            }
            else if (req.amistadUsuario.identificacionUsuario1 <1) {
                res.descripcionError = "Identificacion Usuario1 no encontrada";
            }
            else if (req.amistadUsuario.identificacionUsuario2 <1)
            {
                res.descripcionError = "Identificacion Usuario2 no encontrada";
            }else 
            {
                //Todo fue correcto
                //Enviar a Linq

                //en caso que no se pueda conectar a la BD
                try
                {
                    int? identificadorAmistad=0;
                    string errorDescripcion = null;

                    LinqConexionDataContext linq = new LinqConexionDataContext();
                    linq.SP_UsuarioAmistadIniciar(req.amistadUsuario.identificacionUsuario1, req.amistadUsuario.identificacionUsuario2, ref identificadorAmistad, ref errorDescripcion);

                    //Verificacion del resultado de la base de datos

                    if (identificadorAmistad <1)
                    {
                        errorDescripcion = "No se pudo agregar en Base de Datos";
                    }
                    else
                    {
                        res.resultado = true;
                    }
                }
                catch (Exception ex) {
                    res.descripcionError = "Error en conexion en Base de Datos" + ex.Message;
                }
            }
            return res;
        }
        //

        //
        // Metodo para Eliminar Amistad
        public ResAmistadUsuarioEliminar AmistadUsuarioEliminar(ReqAmistadUsuarioEliminar req) {
            // Crear Respuesta
            ResAmistadUsuarioEliminar res = new ResAmistadUsuarioEliminar();

            res.resultado = false; //Por defecto para todos los casos
            if (req == null)
            {
                res.descripcionError = "El req es Null";
            } 
            else if (req.amistadUsuario.identificacionUsuario1 < 1) 
            {
                res.descripcionError = "El identificador del primero usuario esta Null";
            } 
            else if (req.amistadUsuario.identificacionUsuario2 < 1) 
            {
                res.descripcionError = "El identificador del segundo usuario esta Null";
            }
            else
            {
                try {
                    int? estatus = 0;
                    string errorDescripcion = null;
                    LinqConexionDataContext linq = new LinqConexionDataContext();
                    linq.SP_UsuarioAmistadEliminar(req.amistadUsuario.identificacionUsuario1, req.amistadUsuario.identificacionUsuario2, ref estatus, ref errorDescripcion);
                    if (estatus==0)
                    {
                        res.resultado = true;
                    }
                    else
                    {
                        res.descripcionError = "No se pudo eliminar en la base de datos";
                    }
                } catch (Exception ex) {
                    res.descripcionError = "Error en la conexion a la base de datos" + ex.Message;
                }
            }
            return res;
        }
        //

        //
        // Metodo para Listar Amistad de un usuario
        public ResAmistadUsuarioListar listarAmigosUsuario(ReqAmistadUsuarioListar req) {
            //Crear la respuesta
            ResAmistadUsuarioListar res = new ResAmistadUsuarioListar();

            res.resultado = false; // Por defecto es falso
            if (req==null)
            {
                res.descripcionError = "El req es Null";
            }else if (req.amistadUsuario.identificacionUsuario1<1)
            {
                res.descripcionError = "El usuario no es valido";
            }
            else
            {
                //Si todo funciona
                try
                {
                    LinqConexionDataContext linq = new LinqConexionDataContext();
                    List<SP_UsuarioAmistadListarResult> resultSet = new List<SP_UsuarioAmistadListarResult>();
                    resultSet = linq.SP_UsuarioAmistadListar(req.amistadUsuario.identificacionUsuario1).ToList();
                    res.resultado = true;


                    res.listaAmistadUsuarios.Clear();

                    //Listar todos los resutados de la base de datos
                    foreach (SP_UsuarioAmistadListarResult cadaResultSet in resultSet)
                    {
                        res.listaAmistadUsuarios.Add(this.fabricaAmigosUsuario(cadaResultSet));
                    }
                }
                catch (Exception ex) {
                    res.descripcionError = "Error al conectar a la base de datos "+ ex.Message;
                }
            }
            return res;
        }
        //

        //
        //Metodos Auxiliares
        //
        //Constructor de lista de amigos
        private AmistadUsuario fabricaAmigosUsuario(SP_UsuarioAmistadListarResult amigosResult) {
            
            AmistadUsuario amistadUsuario = new AmistadUsuario();
            amistadUsuario.identificacionUsuario1 = amigosResult.identificadorUsuario1;
            amistadUsuario.nombreUsuario1 = amigosResult.NombreUsuario1;
            amistadUsuario.identificacionUsuario2 = amigosResult.identificadorUsuario2;
            amistadUsuario.nombreUsuario2 = amigosResult.NombreUsuario2;
            amistadUsuario.fechaInicio = amigosResult.FechaInicio;
            return amistadUsuario;
        }
    }
}
