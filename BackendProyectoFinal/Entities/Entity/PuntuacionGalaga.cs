using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Entities
{
    public class PuntuacionGalaga : PartidaGalaga
    {
        public DateTime actualizacion { get; set; }

        public string nombre { get; set; }
    }
}
