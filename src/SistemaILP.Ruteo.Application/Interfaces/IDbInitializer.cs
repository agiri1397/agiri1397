namespace SistemaILP.Ruteo.Application.Interfaces;

/// <summary>
/// Creates the local SQLite database and schema if they do not exist
/// yet. Does not seed any data - accounts are created only through a
/// successful login against the real Web Service.
/// </summary>
public interface IDbInitializer
{
    Task InitializeAsync();
}
