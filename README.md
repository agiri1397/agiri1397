# SistemaILP.Ruteo

Plantilla funcional de una app **.NET MAUI Blazor Hybrid** con **Clean Architecture**, persistencia local en **SQLite** (Entity Framework Core), un **login/registro funcional** (usuarios y contraseñas hasheadas guardados en SQLite), un **ejemplo de consumo de un Web Service REST** (GET/POST) y una interfaz construida **100% con MudBlazor**.

> ⚠️ Esta plantilla se generó en un entorno sin el SDK de .NET ni los workloads de MAUI instalados, por lo que el código **no pudo compilarse ni ejecutarse aquí**. Sigue la sección [Puesta en marcha](#puesta-en-marcha) para compilarla en tu máquina. La estructura, namespaces y paquetes siguen las convenciones estándar de las plantillas oficiales `dotnet new maui-blazor`, así que debería compilar sin cambios; si tu SDK trae versiones distintas de EF Core / MudBlazor, ajusta los rangos de versión en los `.csproj`.

## Arquitectura

```
SistemaILP.Ruteo.sln
src/
  SistemaILP.Ruteo.Domain          -> Entidades (User, TodoItem). Sin dependencias.
  SistemaILP.Ruteo.Application     -> DTOs, interfaces (puertos) y casos de uso (AuthService, TodoService).
  SistemaILP.Ruteo.Infrastructure  -> EF Core + SQLite, repositorios, hashing de contraseñas,
                                                  AuthenticationStateProvider, cliente HTTP para el WS de ejemplo.
  SistemaILP.Ruteo.UI              -> Razor Class Library con las páginas y layouts (MudBlazor).
  SistemaILP.Ruteo.Maui            -> Proyecto "head" MAUI (Android/iOS/MacCatalyst/Windows) que
                                                  aloja el BlazorWebView y hace el wiring de inyección de dependencias.
tests/
  SistemaILP.Ruteo.Application.Tests -> Pruebas unitarias (xUnit) de AuthService con dobles en memoria.
```

Regla de dependencias (Clean Architecture): `Maui` → `UI`/`Infrastructure` → `Application` → `Domain`. La UI solo conoce las interfaces de `Application`; nunca referencia `Infrastructure` directamente. Los detalles de plataforma (SecureStorage) se inyectan desde el proyecto `Maui` implementando `ISecureStorageService`.

## Funcionalidades incluidas

- **Login y registro funcionales** contra SQLite, sin backend: contraseñas con hash PBKDF2 + salt (`Infrastructure/Security/PasswordHasher.cs`), sesión persistida con `SecureStorage` de MAUI, y `AuthenticationStateProvider` propio integrado con `[Authorize]`/`AuthorizeRouteView` de Blazor.
- **Cuenta demo** creada automáticamente al primer arranque: usuario `demo`, contraseña `Demo123!` (ver `Infrastructure/Persistence/DbInitializer.cs`).
- **Persistencia local en SQLite** vía EF Core: CRUD completo de tareas (`Pages/Todos.razor`) aislado por usuario logueado.
- **Ejemplo de consumo de Web Service (REST)**: `Pages/Posts.razor` hace `GET`/`POST` contra `https://jsonplaceholder.typicode.com` usando `HttpClient` tipado registrado con `IHttpClientFactory` (`Infrastructure/Http/PostsApiService.cs`). Cambia la `BaseAddress` en `Infrastructure/DependencyInjection.cs` por tu propia API.
- **Toda la UI en MudBlazor**: `MudLayout`, `MudAppBar`, `MudDrawer`, `MudNavMenu`, `MudForm`, `MudTextField`, `MudTable`, `MudList`, `MudSnackbar`, `MudDialogProvider`, tema claro/oscuro (`App.razor`).

## Puesta en marcha

### Requisitos

- [.NET SDK 8](https://dotnet.microsoft.com/download) (o superior, ver `global.json`)
- Workload de MAUI (instala/actualiza exactamente lo que cada proyecto de la solución necesita):
  ```bash
  dotnet workload restore
  ```
  ejecútalo desde la carpeta que contiene `SistemaILP.Ruteo.sln`. Si prefieres instalar todo manualmente: `dotnet workload install maui`.
- Para compilar/ejecutar en cada plataforma necesitas las herramientas nativas correspondientes (Android SDK, Xcode para iOS/MacCatalyst, Visual Studio con carga de trabajo ".NET Multi-platform App UI" en Windows).
- **Solo en Windows/Linux**: por defecto `SistemaILP.Ruteo.Maui.csproj` compila para `android` (+ `windows` en Windows), y **omite iOS/MacCatalyst** porque esos targets requieren un Mac. Si compilas desde macOS se incluyen automáticamente; si necesitas forzarlos desde otro SO (por ejemplo en CI con un Mac remoto), compila con `/p:IncludeAppleTargets=true`.

### Restaurar y compilar

```bash
dotnet restore SistemaILP.Ruteo.sln
dotnet build SistemaILP.Ruteo.sln -f net8.0-windows10.0.19041.0   # Windows
# o el target framework de tu plataforma: net8.0-android / net8.0-ios / net8.0-maccatalyst
```

### Ejecutar

```bash
dotnet build -t:Run -f net8.0-android src/SistemaILP.Ruteo.Maui/SistemaILP.Ruteo.Maui.csproj
```

O abre `SistemaILP.Ruteo.sln` en Visual Studio 2022 (17.8+) con la carga de trabajo MAUI, selecciona el proyecto `SistemaILP.Ruteo.Maui` como proyecto de inicio y el emulador/dispositivo deseado, y pulsa **F5**.

### Pruebas unitarias

```bash
dotnet test tests/SistemaILP.Ruteo.Application.Tests
```

## Dónde extender

| Quiero...                                   | Archivo/carpeta                                                             |
|----------------------------------------------|-------------------------------------------------------------------------------|
| Agregar una entidad nueva                     | `Domain/Entities`, su `IEntityTypeConfiguration` en `Infrastructure/Persistence/Configurations`, y añadir el `DbSet` en `AppDbContext` |
| Agregar un caso de uso / servicio             | Interfaz en `Application/Interfaces`, implementación en `Application/Services`, registro en `Application/DependencyInjection.cs` |
| Consumir otro Web Service                     | Nueva interfaz + implementación siguiendo el patrón de `IPostsApiService`/`PostsApiService`, registrar con `AddHttpClient<TInterfaz, TImpl>()` en `Infrastructure/DependencyInjection.cs` |
| Agregar una página                            | `.razor` en `UI/Pages`, usando componentes `Mud*`; protégela con `@attribute [Authorize]` si requiere sesión |
| Cambiar la ruta del archivo SQLite            | `MauiProgram.cs` (`dbPath`) |

## Solución de problemas (Windows)

**`NETSDK1147: deben estar instaladas las siguientes cargas de trabajo: android`**
Falta el workload de Android para el SDK de .NET. Abre una terminal (idealmente como Administrador) en la carpeta del `.sln` y ejecuta:
```bash
dotnet workload restore
```
Reinicia Visual Studio después de que termine.

**`MSB4184 ... supera el límite máximo para la ruta de acceso del sistema operativo (260 caracteres)`**
Windows limita la longitud total de una ruta a 260 caracteres, y las carpetas `obj/bin` de MAUI son muy profundas (`obj\Debug\net8.0-windows10.0.19041.0\win10-x64\ref\...`). Esto pasa casi siempre porque el ZIP de GitHub se descomprimió dentro de una carpeta con el mismo nombre (ruta duplicada) y/o el proyecto quedó dentro de `Desktop`. Solución:
1. Mueve/extrae el proyecto a una ruta corta, por ejemplo `C:\src\agiri1397` (evita `Desktop`, evita carpetas anidadas con el mismo nombre).
2. Verifica que el archivo `.sln` quede directamente en esa carpeta (`C:\src\agiri1397\SistemaILP.Ruteo.sln`), no dentro de otra carpeta repetida.
3. Vuelve a compilar.

**Errores "No se puede encontrar ... `.GeneratedMSBuildEditorConfig.editorconfig`"**
Son un efecto secundario de los dos problemas anteriores (el `obj/` nunca se generó porque falló el restore/workload, o la ruta era demasiado larga). Se resuelven solos al aplicar los dos puntos anteriores; si persisten, borra las carpetas `obj/` y `bin/` de cada proyecto y vuelve a restaurar (`dotnet restore` o *Clean Solution* + *Restore NuGet Packages* en Visual Studio).

## Notas de seguridad

- Las contraseñas nunca se guardan en texto plano: se derivan con PBKDF2-SHA256 (100 000 iteraciones) y salt aleatorio por usuario.
- La sesión (id del usuario logueado) se guarda con `SecureStorage` de MAUI (Keychain/KeyStore/DPAPI según la plataforma), no en `Preferences` ni en texto plano.
- El ejemplo de Web Service usa una API pública de pruebas (jsonplaceholder) sin autenticación; si tu backend requiere tokens, añade el header `Authorization` en el `AddHttpClient` correspondiente.
