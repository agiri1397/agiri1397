namespace SistemaILP.Ruteo.Application.Configuration;

/// <summary>
/// Variante de la aplicacion que se esta ejecutando. Determinada en
/// tiempo de compilacion (ver el proyecto Maui) y expuesta en runtime
/// via AppConfiguration.Variant - nunca se decide dentro de un
/// componente Razor.
///
/// PreventaNicaragua queda reservada para una etapa posterior; no debe
/// usarse todavia. Se deja aqui unicamente para que agregarla despues
/// no implique reestructurar el enum ni el codigo que lo consume.
/// </summary>
public enum AppVariant
{
    Preventa,
    Autoventa,
    Despachos
    // PreventaNicaragua - NO implementar todavia.
}
