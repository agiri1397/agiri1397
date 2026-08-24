namespace SistemaILP.Ruteo.Application.Interfaces;

/// <summary>
/// Creates the local SQLite database (if missing) and seeds a demo
/// account so the login screen is functional out of the box.
/// </summary>
public interface IDbInitializer
{
    Task InitializeAsync();
}
