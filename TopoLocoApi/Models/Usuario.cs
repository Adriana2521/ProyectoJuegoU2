using System;
using System.Collections.Generic;

namespace TopoLocoApi.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public int Puntaje { get; set; } = 0;
}
