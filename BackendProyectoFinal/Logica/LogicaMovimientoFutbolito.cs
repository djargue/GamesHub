using BackendProyectoFinal.DataAccess;
using BackendProyectoFinal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Logica
{
    //Logica de Movimiento Futbolito
    //Utiliza
    //// Insertar Movimiento

    public class LogicaMovimientoFutbolito
    {

        //
        //Insertar movimientos 
        public ResFutbolitoMovimientoInsertar FutbolitoMovimientoInsertar(ReqFutbolitoMovimientoInsertar req)
        {
            //Crear la respuesta
            ResFutbolitoMovimientoInsertar res = new ResFutbolitoMovimientoInsertar();

            res.resultado = false; //por default el resultado es falso a menos que se logre

            // Casos que genera error y la descripcion
            if (req == null)
            {
                res.descripcionError = "Req null";
            }
            else if (req.futbolitoMovimiento.identificadorMovimiento < 1)
            {
                res.descripcionError = "No hay identificador de movimiento";
            }
            else if (req.futbolitoMovimiento.identificadorTurno < 1)
            {
                res.descripcionError = "No hay identificador de turno";
            }
            else if (req.futbolitoMovimiento.identificadorUsuario < 1)
            {
                res.descripcionError = "Identificador Usuario Vacio";
            }
            else if (req.futbolitoMovimiento.direccionX > 200 || req.futbolitoMovimiento.direccionX < -200)
            {
                res.descripcionError = "direccion X no valida";
            }
            else if (req.futbolitoMovimiento.direccionY > 200 || req.futbolitoMovimiento.direccionY < 0)
            {
                res.descripcionError = "direccion Y no valida";
            }
            else if (req.futbolitoMovimiento.tipoJugador != 0 || req.futbolitoMovimiento.tipoJugador != 1)
            {
                res.descripcionError = "tipo jugador invalido";
            }
            else
            {
                //Variables
                int? identificadorMovimiento = 0;
                string errorDescripcion = null;

                try
                {
                    //llamar a linq
                    LinqConexionDataContext linq = new LinqConexionDataContext();
                    linq.SP_FutbolitoMovimientoInsertar(req.futbolitoMovimiento.identificadorMovimiento, req.futbolitoMovimiento.identificadorUsuario, req.futbolitoMovimiento.direccionX, req.futbolitoMovimiento.direccionY, req.futbolitoMovimiento.tipoJugador, ref identificadorMovimiento, ref errorDescripcion);

                    if (identificadorMovimiento > 0)
                    {
                        res.resultado = true;
                    }
                    else
                    {
                        res.descripcionError = errorDescripcion;
                    }
                }
                catch (Exception EX) {
                    res.descripcionError = "Error de conexion con la Base de Datos" + EX.Message;
                }
            }
            return res;
        }
        //


    }
}
