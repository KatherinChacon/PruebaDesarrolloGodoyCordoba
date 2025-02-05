using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public long? Cedula { get; set; }

    public string? Correo { get; set; }

    public DateTime? FechaAcceso { get; set; }
}
