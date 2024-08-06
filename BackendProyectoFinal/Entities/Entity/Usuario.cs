using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Entities
{
    public class Usuario
    {
        public long? identificadorUsuario { get; set; }
        public string nombre { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public Byte[] fotoPerfil { get; set; }
        public bool estatus { get; set; }
    }
}
