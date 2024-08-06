using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Entities
{
    public class FutbolitoPartida
    {
        public long? identificacionJuego { get; set; }
        public long? identificacionUsuario1 { get; set; }
        public long? identificacionUsuario2 { get; set; }
        public string nombreJugador1 { get; set; }
        public string nombreJugador2 { get; set; }
        public long? identificadorGanador { get; set; }
        public DateTime fechaIngresoJuego { get; set; }
        public int? turno { get; set; }
    }
}
