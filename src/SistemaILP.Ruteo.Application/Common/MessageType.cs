namespace SistemaILP.Ruteo.Application.Common;

/// <summary>
/// Clasificacion de un mensaje mostrado al usuario, independiente de
/// MudBlazor (la eleccion de icono/color vive en la capa UI). Permite que
/// Application/Infrastructure etiqueten sus mensajes sin depender de la UI.
/// </summary>
public enum MessageType
{
    Success,
    Error,
    Warning,
    Info,
    Confirmation
}
