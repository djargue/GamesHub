using BackendProyectoFinal.DataAccess;
using BackendProyectoFinal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Logica
{
    //Logica de Puntuaciones Finales Galaga
    //Utiliza
    //// Buscar Partidas Galaga de un usuario
    ////Buscar mejores Puntuaciones de todos los usuarios Galaga
    //// Fabrica de Puntuaciones
    internal class LogicaPuntuacionGalaga
    {


        // Buscar Partidas Galaga de un usuario
        public ResGalagaPartidaUsuarioPuntuaciones GalagaPartidaUsuarioPuntuaciones(ReqGalagaPartidaUsuarioPuntuaciones req)
        {
            // Crear la respuesta
            ResGalagaPartidaUsuarioPuntuaciones res = new ResGalagaPartidaUsuarioPuntuaciones();
            res.resultado = false; //Por defecto en falso

            try
            {
                //Generar llamada al metodo y la lista con los resultados
                LinqConexionDataContext linq = new LinqConexionDataContext();
                List<SP_GalagaPuntuacionUsuarioResult> resulSet = new List<SP_GalagaPuntuacionUsuarioResult>();
                resulSet = linq.SP_GalagaPuntuacionUsuario(req.partidaGalaga.identificadorUsuario).ToList();

                res.listaPartidaGalagaUsuario.Clear();

                foreach (SP_GalagaPuntuacionUsuarioResult cadaResultSet in resulSet)
                {
                    res.listaPartidaGalagaUsuario.Add(fabricaPuntuciacionesGalaga(cadaResultSet));
                }
                res.resultado = true;//se logro extraer la informacion

            }
            catch (Exception ex)
            {
                res.descripcionError = "Error en la conexion a la Base de Datos" + ex.Message;
            }

            return res;
        }
        //



        public ResGalagaPartidaUsuariosPuntuaciones GalagaPartidaUsuariosPuntuaciones(ReqGalagaPartidaUsuariosPuntuaciones req)
        {
            // Crear la respuesta
            ResGalagaPartidaUsuariosPuntuaciones res = new ResGalagaPartidaUsuariosPuntuaciones();
            res.resultado = false; //Por defecto en falso

            try
            { 
                //Generar llamada al metodo y la lista con los resultados
                LinqConexionDataContext linq = new LinqConexionDataContext();
                List<SP_GalagaObtenerPuntuacionesUsuariosResult> resulSet = new List<SP_GalagaObtenerPuntuacionesUsuariosResult>();
                resulSet = linq.SP_GalagaObtenerPuntuacionesUsuarios().ToList();

                res.listaPuntuacionesGalaga.Clear();

                foreach (SP_GalagaObtenerPuntuacionesUsuariosResult cadaResultSet in resulSet)
                {
                    res.listaPuntuacionesGalaga.Add(fabricaPuntuciacionesGalagaUsuarios(cadaResultSet));
                }
                res.resultado = true;//se logro extraer la informacion

            }
            catch (Exception ex)
            {
                res.descripcionError = "Error en la conexion a la Base de Datos" + ex.Message;
            }

            return res;
        }
        //

        //
        //Metodos Auxiliares
        //
        //Constructor de lista de puntuaciones del usuario
        private PuntuacionGalaga fabricaPuntuciacionesGalaga(SP_GalagaPuntuacionUsuarioResult puntuacionesResult)
        {
            PuntuacionGalaga puntuacionGalaga = new PuntuacionGalaga();

            puntuacionGalaga.indentificadorJuegoGalaga = puntuacionesResult.identificadorJuego;
            puntuacionGalaga.identificadorUsuario = puntuacionesResult.identificadorUsuario1;
            puntuacionGalaga.puntajeUsuario = puntuacionesResult.PuntajeUsuario1;
            puntuacionGalaga.actualizacion = puntuacionesResult.fechaJuego;
            return puntuacionGalaga;
        }
        //

        //Constructor de lista de puntuaciones del usuario
        private PuntuacionGalaga fabricaPuntuciacionesGalagaUsuarios(SP_GalagaObtenerPuntuacionesUsuariosResult puntuacionesResult)
        {
            PuntuacionGalaga puntuacionGalaga = new PuntuacionGalaga();

            puntuacionGalaga.indentificadorJuegoGalaga = puntuacionesResult.identificadorJuego;
            puntuacionGalaga.identificadorUsuario = puntuacionesResult.identificadorUsuario1;
            puntuacionGalaga.puntajeUsuario = puntuacionesResult.PuntajeUsuario1;
            puntuacionGalaga.actualizacion = puntuacionesResult.fechaJuego;
            puntuacionGalaga.nombre = puntuacionesResult.Nombre;
            return puntuacionGalaga;
        }
        //



    }
}
