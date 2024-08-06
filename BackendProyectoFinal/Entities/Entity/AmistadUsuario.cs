using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Entities
{
    public class AmistadUsuario
    {
        public long? identificacionUsuario1 { get; set; }
        public string nombreUsuario1 { get; set; }
        public long? identificacionUsuario2 { get; set; }
        public string nombreUsuario2 { get; set; }
        public DateTime fechaInicio { get; set; }

    }
}
