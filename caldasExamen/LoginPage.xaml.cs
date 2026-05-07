namespace caldasExamen;

public partial class LoginPage : ContentPage
{
    private readonly string[,] _usuarios =
    {
        { "estudiante", "moviles" },
        { "suda", "2025" }
    };

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnIniciarSesionClicked(object? sender, EventArgs e)
    {
        var usuario = UsuarioEntry.Text?.Trim() ?? string.Empty;
        var contrasena = ContrasenaEntry.Text?.Trim() ?? string.Empty;

        if (!CredencialesValidas(usuario, contrasena))
        {
            await DisplayAlertAsync("Acceso denegado", "Dato incorrecto", "OK");
            return;
        }

        await Navigation.PushAsync(new RegistroPage(usuario));
    }

    private bool CredencialesValidas(string usuario, string contrasena)
    {
        for (var i = 0; i < _usuarios.GetLength(0); i++)
        {
            if (_usuarios[i, 0].Equals(usuario, StringComparison.OrdinalIgnoreCase)
                && _usuarios[i, 1].Equals(contrasena, StringComparison.Ordinal))
            {
                return true;
            }
        }

        return false;
    }
}
