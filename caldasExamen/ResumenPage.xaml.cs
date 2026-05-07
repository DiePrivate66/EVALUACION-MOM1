namespace caldasExamen;

public partial class ResumenPage : ContentPage
{
    public ResumenPage(StudentRegistrationData data)
    {
        InitializeComponent();

        UsuarioConectadoLabel.Text = $"Usuario conectado: {data.UsuarioConectado}";
        NombreValueLabel.Text = data.Nombre;
        ApellidoValueLabel.Text = data.Apellido;
        EdadValueLabel.Text = data.Edad.ToString();
        FechaValueLabel.Text = data.Fecha.ToString("dd/MM/yyyy");
        CiudadValueLabel.Text = data.Ciudad;
        PaisValueLabel.Text = data.Pais;
        MontoInicialValueLabel.Text = data.MontoInicial.ToString("C2");
        PagoMensualValueLabel.Text = data.PagoMensual.ToString("C2");
        PagoTotalValueLabel.Text = data.PagoTotal.ToString("C2");
    }
}
