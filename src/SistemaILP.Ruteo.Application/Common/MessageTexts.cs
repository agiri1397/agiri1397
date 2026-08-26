namespace SistemaILP.Ruteo.Application.Common;

/// <summary>
/// Textos centralizados mostrados al usuario. Evita repetir los mismos
/// mensajes en distintos archivos y sirve de unico punto de edicion de
/// redaccion. Los mensajes conservan el significado funcional del Login
/// Android original (LoginService.java), solo mejora la redaccion y se
/// agrega un titulo para la presentacion en UI.
/// </summary>
public static class MessageTexts
{
    public static class Login
    {
        public const string RequiredFieldsTitle = "Datos incompletos";
        public const string RequiredFieldsMessage = "Usuario y contraseña son obligatorios.";

        public const string InvalidCredentialsTitle = "Datos incorrectos";
        public const string InvalidCredentialsMessage =
            "El usuario o la contraseña son incorrectos. Verifique sus datos e inténtelo nuevamente.";

        public const string InvalidServerResponseTitle = "Respuesta inválida";
        public const string InvalidServerResponseMessage =
            "El servidor no devolvió una respuesta válida. Inténtelo nuevamente.";

        public const string ServerUnavailableTitle = "Servidor no disponible";
        public const string ServerUnavailableMessage =
            "No fue posible completar el inicio de sesión. Inténtelo nuevamente.";

        public const string InvalidResponseTitle = "Respuesta inválida";
        public const string InvalidResponseMessage = "Se recibió una respuesta inválida del servidor.";

        public const string TimeoutTitle = "Tiempo de espera agotado";
        public const string TimeoutMessage = "El servidor tardó demasiado en responder. Inténtelo nuevamente.";

        public const string ConnectionErrorTitle = "Sin conexión";
        public const string ConnectionErrorMessage =
            "No fue posible conectarse con el servidor. Verifique su conexión e inténtelo nuevamente.";

        public const string InvalidBaseUrlTitle = "Configuración inválida";
        public const string InvalidBaseUrlMessage =
            "La dirección del servidor configurada no es válida. Revísela en Configuración.";

        public const string UnexpectedErrorTitle = "Error inesperado";
        public const string UnexpectedErrorMessage = "Ocurrió un error inesperado. Inténtelo nuevamente.";

        public const string SuccessTitle = "Listo";

        public static string SuccessMessage(string nombre) => $"Bienvenido, {nombre}.";

        public const string LogoutErrorTitle = "No se pudo cerrar sesión";
        public const string LogoutErrorMessage = "No fue posible cerrar la sesión. Inténtelo nuevamente.";
    }
}
