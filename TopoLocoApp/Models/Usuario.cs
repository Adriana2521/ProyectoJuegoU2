using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopoLocoApp.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public int Puntaje { get; set; } = 0;
    }
}
