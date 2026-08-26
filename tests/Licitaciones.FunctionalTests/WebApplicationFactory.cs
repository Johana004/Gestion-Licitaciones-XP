// En tu clase de test o fixture donde configuras WebApplicationFactory
public class ProveedoresControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProveedoresControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Deshabilita la validación de Antiforgery durante el testing
                services.AddControllersWithViews(options =>
                {
                    options.Filters.Add<IgnoreAntiforgeryTokenAttribute>();
                });
            });
        }).CreateClient();
    }

    [Fact]
    public async Task Crear_PostValido_RedireccionaAIndex()
    {
        var formData = new Dictionary<string, string>
        {
            { "CedulaJuridica", "3-101-999999" },
            { "NombreRazonSocial", "Proveedor Test S.A." },
            { "EmailContacto", "test@proveedor.com" },
            { "Telefono", "8888-8888" }
        };

        var content = new FormUrlEncodedContent(formData);

        var response = await _client.PostAsync("/Proveedores/Crear", content);

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Equal("/Proveedores", response.Headers.Location?.OriginalString);
    }
}