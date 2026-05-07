namespace caldasExamen;

public partial class RegistroPage : ContentPage
{
    private const decimal CostoCurso = 1500m;
    private const decimal RecargoPorCuota = CostoCurso * 0.04m;

    private readonly string _usuarioConectado;

    private readonly Dictionary<string, List<string>> _paisesCiudades = new()
    {
        ["Ecuador"] = new List<string> { "Quito", "Guayaquil", "Cuenca" },
        ["Colombia"] = new List<string> { "Bogotá", "Medellín", "Cali" },
        ["Perú"] = new List<string> { "Lima", "Arequipa", "Cusco" }
    };

    public RegistroPage(string usuarioConectado)
    {
        InitializeComponent();
        _usuarioConectado = usuarioConectado;

        UsuarioConectadoLabel.Text = $"Usuario conectado: {_usuarioConectado}";

        PaisPicker.ItemsSource = _paisesCiudades.Keys.ToList();
        PaisPicker.SelectedIndex = 0;

        PagoMensualEntry.Text = RecargoPorCuota.ToString("F2");
    }

    private void OnPaisChanged(object? sender, EventArgs e)
    {
        var paisSeleccionado = PaisPicker.SelectedItem?.ToString();
        if (string.IsNullOrWhiteSpace(paisSeleccionado) || !_paisesCiudades.TryGetValue(paisSeleccionado, out var ciudades))
        {
            CiudadPicker.ItemsSource = null;
            return;
        }

        CiudadPicker.ItemsSource = ciudades;
        CiudadPicker.SelectedIndex = 0;
    }

    private async void OnMontoInicialChanged(object? sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(MontoInicialEntry.Text))
        {
            PagoMensualEntry.Text = RecargoPorCuota.ToString("F2");
            return;
        }

        if (!decimal.TryParse(MontoInicialEntry.Text, out var montoInicial))
        {
            PagoMensualEntry.Text = string.Empty;
            return;
        }

        if (montoInicial < 0 || montoInicial > CostoCurso)
        {
            await DisplayAlertAsync("Monto inválido", "El monto inicial debe estar entre 0 y 1500.", "OK");
            MontoInicialEntry.Text = string.Empty;
            return;
        }

        var restante = CostoCurso - montoInicial;
        var pagoMensual = (restante / 4m) + RecargoPorCuota;
        PagoMensualEntry.Text = pagoMensual.ToString("F2");
    }

    private async void OnVerResumenClicked(object? sender, EventArgs e)
    {
        var nombre = NombreEntry.Text?.Trim() ?? string.Empty;
        var apellido = ApellidoEntry.Text?.Trim() ?? string.Empty;
        var pais = PaisPicker.SelectedItem?.ToString() ?? string.Empty;
        var ciudad = CiudadPicker.SelectedItem?.ToString() ?? string.Empty;

        if (!int.TryParse(EdadEntry.Text, out var edad) || edad <= 0)
        {
            await DisplayAlertAsync("Dato inválido", "Ingrese una edad válida.", "OK");
            return;
        }

        if (!decimal.TryParse(MontoInicialEntry.Text, out var montoInicial))
        {
            await DisplayAlertAsync("Dato inválido", "Ingrese un monto inicial válido.", "OK");
            return;
        }

        if (!decimal.TryParse(PagoMensualEntry.Text, out var pagoMensual))
        {
            await DisplayAlertAsync("Dato inválido", "No se pudo calcular el pago mensual.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || string.IsNullOrWhiteSpace(pais) || string.IsNullOrWhiteSpace(ciudad))
        {
            await DisplayAlertAsync("Campos requeridos", "Complete todos los campos del registro.", "OK");
            return;
        }

        var resumen = new StudentRegistrationData
        {
            UsuarioConectado = _usuarioConectado,
            Nombre = nombre,
            Apellido = apellido,
            Edad = edad,
            Fecha = FechaDatePicker.Date ?? DateTime.Today,
            Pais = pais,
            Ciudad = ciudad,
            MontoInicial = montoInicial,
            PagoMensual = pagoMensual,
            PagoTotal = montoInicial + (pagoMensual * 4m)
        };

        await Navigation.PushAsync(new ResumenPage(resumen));
    }
}
