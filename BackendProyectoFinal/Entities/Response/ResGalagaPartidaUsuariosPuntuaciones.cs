using BackendProyectoFinal.Entities;
using BackendProyectoFinal.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Entities
{
    public class ResGalagaPartidaUsuariosPuntuaciones:ResDataBase
    {
        public List<PuntuacionGalaga> listaPuntuacionesGalaga = new List<PuntuacionGalaga>();
    }
}

