using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Sesion
{
    public int IdSesion { get; set; }

    public string? Correo { get; set; }

    public string? Contrasena { get; set; }
}
