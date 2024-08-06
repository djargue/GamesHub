using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Entities
{
    public class PartidaGalaga
    {
        public long? indentificadorJuegoGalaga { get; set; }
        public long? identificadorUsuario { get; set; }
        public int? puntajeUsuario { get; set; }
        public float duracion { get; set; } 
    }
}
