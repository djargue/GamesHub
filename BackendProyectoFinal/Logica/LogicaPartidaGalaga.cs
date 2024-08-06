using BackendProyectoFinal.DataAccess;
using BackendProyectoFinal.Entities;
using BackendProyectoFinal.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Logica
{
    //Logica de PartidaGalaga
    //Utiliza
    //// Insertar Partida Galaga

    
    internal class LogicaPartidaGalaga
    {
        //
        // Insertar Partida Galaga  
        public ResGalagaPartidaInsertar GalagaPartidaInsertar(ReqGalagaPartidaInsertar req) { 
            //Crear la respuesta
            ResGalagaPartidaInsertar res = new ResGalagaPartidaInsertar();
            res.resultado = false; //Por defecto resultado es falso
            //Verificar errores
            if (req == null)
            {
                res.descripcionError = "El Req estaba null";
            }
            else
            {
                try
                {
                    //Variables para el req
                    long? identificadorJuego = 0;
                    string errorDescripcion = null;
                    LinqConexionDataContext linq = new LinqConexionDataContext();
                    linq.SP_GalagaCrearPartida(req.partidaGalaga.identificadorUsuario,req.partidaGalaga.puntajeUsuario,req.partidaGalaga.duracion,ref identificadorJuego,ref errorDescripcion);
                    //Verificaciones del resultado
                    if (identificadorJuego > 1)
                    {
                        res.resultado = true;
                    }
                    else {
                        res.descripcionError ="Error en la base de datos" + errorDescripcion;
                    }
                }
                catch (Exception ex) {
                    res.descripcionError="Error al conectar a la base de datos" + ex.Message;
                }
            }
            return res;
        }
        //

    }
}
