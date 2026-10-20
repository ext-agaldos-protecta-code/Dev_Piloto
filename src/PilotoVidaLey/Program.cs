var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.Ok(new
{
    application = "Piloto Vida Ley",
    message = "Aplicacion de ejemplo para validar CI/CD en IIS."
}));

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();

public partial class Program
{
}
