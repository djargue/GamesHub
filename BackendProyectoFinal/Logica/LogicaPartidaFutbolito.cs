using BackendProyectoFinal.DataAccess;
using BackendProyectoFinal.Entities;
using BackendProyectoFinal.Entities.Response;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Logica
{
    //Logica de PartidasFutbolito
    //Utiliza
    //// Crear Partida Futbolito
    //// Buscar Partida Futbolito 
    //// Traer Partida Futbolito
    internal class LogicaPartidaFutbolito
    {
        //
        //Metodo para Crear Partida Futbolito
        public ResFutbolitoPartidaCrear FutbolitoPartidaCrear(ReqFutbolitoPartidaCrear req) {
            //Crear la respuesta
            ResFutbolitoPartidaCrear res = new ResFutbolitoPartidaCrear();

            res.resultado = false; //Por defecto es false
            //Verificacion de errores en el REQ
            if (req == null) {
                res.descripcionError = "El REQ esta NUll";
            }
            else if(req.partidaFutbolito.identificacionUsuario1 <1)
            {
                res.descripcionError = "Identificacion de usuario no valida";
            }
            else
            {
                //Todo Correcto, envair el REQ
                try
                {
                    //Variables a obtener
                    long? identificadorJuego = 0;
                    string errorDescripcion = null;
                    //Conexion LINQ
                    LinqConexionDataContext linq = new LinqConexionDataContext();
                    linq.SP_FutbolitoCrearPartida(req.partidaFutbolito.identificacionUsuario1, ref identificadorJuego, ref errorDescripcion);
                    if (identificadorJuego>0)
                    {
                        res.resultado = true;
                        res.partidaFutbolito.identificacionJuego = identificadorJuego;
                        res.partidaFutbolito.identificacionUsuario1 = req.partidaFutbolito.identificacionUsuario1;
                    }                 
                }
                catch (Exception ex) {
                    res.descripcionError = "Error al conectar a la Base de Datos" +ex.Message;
                }
            }
            return res;
        }
        //

        //
        //Buscar Partida
        public ResFutbolitoPartidaBuscar FutbolitoPartidaBuscar(ReqFutbolitoPartidaBuscar req) {
            //Crear la respuesta
            ResFutbolitoPartidaBuscar res = new ResFutbolitoPartidaBuscar();
            res.resultado = false; //Por defecto es falso

            //Verificar que el REQ este correcto
            if (req==null)
            {
                res.descripcionError = "El REQ es null";
            }else if (req.partidaFutbolito.identificacionUsuario2<1)
            {
                res.descripcionError = "El REQ no tiene el identificador del usuario";
            }
            else
            {
                //Si el REQ esta correcto, intenta usar linq
                try {
                    //Variables
                    string errorDescripcion = "";
                    string nombreUsuario = "";
                    long? identificadorJuego = 0;
                    long? identificadorUsuario1 = 0;
                    int? numerodeTurno =0;

                    //Conexion
                    LinqConexionDataContext linq = new LinqConexionDataContext();
                    linq.SP_FutbolitoBuscarPartida(req.partidaFutbolito.identificacionUsuario2, ref identificadorJuego, ref numerodeTurno, ref errorDescripcion, ref identificadorUsuario1, ref nombreUsuario);
                    
                    //Ver el resultado
                    if (identificadorJuego>0)
                    {
                        res.resultado = true;
                        res.partidaFutbolito.identificacionJuego = identificadorJuego;
                        res.partidaFutbolito.identificacionUsuario1 = identificadorUsuario1;
                        res.partidaFutbolito.identificacionUsuario2 = req.partidaFutbolito.identificacionUsuario2;
                        res.partidaFutbolito.nombreJugador1 = nombreUsuario;
                        res.partidaFutbolito.turno = numerodeTurno;
                    }
                    else
                    {
                        res.descripcionError = errorDescripcion;
                    }
                } catch (Exception ex) {
                    res.descripcionError = "Error en la conexion a la base de datos"+ex.Message;
                }
            }
            return res;
        }
        //


        //
        //Traer Datos Partida
        public ResFutbolitoTraerInformacionJuego FutbolitoTraerInformacionJuego(ReqFutbolitoTraerInformacionJuego req) {
            //crear respuesta
            ResFutbolitoTraerInformacionJuego res = new ResFutbolitoTraerInformacionJuego();

            // Por defecto el resultado es falso
            res.resultado = false;

            //Verificar que el REQ este correcto
            if (req == null)
            {
                res.descripcionError = "El REQ es null";
            }
            else if (req.partidaFutbolito.identificacionJuego<1)
            {
                res.descripcionError = "El REQ no tiene el identificador valido";
            }
            else
            {
                //Si el REQ esta correcto, intenta usar linq
                try
                {
                    //Variables
                    string errorDescripcion = "";
                    string nombreUsuario = "";
                    long? usuarioGanador= 0;
                    long? identificadorUsuario2 = 0;
  
                    //Evio a linq
                    LinqConexionDataContext linq = new LinqConexionDataContext();
                    linq.SP_FutbolitoTraerDatosPartida(req.partidaFutbolito.identificacionJuego, ref usuarioGanador, ref identificadorUsuario2, ref nombreUsuario, ref errorDescripcion);

                    //Verificacion resultado
                    if (identificadorUsuario2 > 0)
                    {
                        res.partidaFutbolito.identificacionUsuario2 = identificadorUsuario2;
                        res.partidaFutbolito.identificadorGanador = usuarioGanador;
                        res.partidaFutbolito.identificacionUsuario2 = identificadorUsuario2;
                        res.partidaFutbolito.nombreJugador2 = nombreUsuario;
                    }
                    else {
                        res.descripcionError = errorDescripcion;
                    }

                }
                catch (Exception ex){

                    res.descripcionError = "Error en la conexion a la base de datos" + ex.Message;
                }

            }

        return res;
        }
        //

    }
}
