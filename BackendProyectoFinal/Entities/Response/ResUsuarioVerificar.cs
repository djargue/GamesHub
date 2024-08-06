using BackendProyectoFinal.Entities;
using BackendProyectoFinal.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Entities
{
    public class ResUsuarioVerificar:ResDataBase
    {
        public Usuario User { get; set; }
    }
}
