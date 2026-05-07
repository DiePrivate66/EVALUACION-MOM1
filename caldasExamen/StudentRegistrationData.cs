using System;

namespace caldasExamen;

public class StudentRegistrationData
{
    public string UsuarioConectado { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public int Edad { get; set; }
    public DateTime Fecha { get; set; }
    public string Pais { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public decimal MontoInicial { get; set; }
    public decimal PagoMensual { get; set; }
    public decimal PagoTotal { get; set; }
}
