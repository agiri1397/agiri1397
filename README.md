# SistemaILP.Ruteo

App **.NET MAUI Blazor Hybrid** con **Clean Architecture**, migración de la app Android existente (Preventa/Autoventa/Despachos). Login real contra el Web Service (`POST validarUsuario`), persistencia local en **SQLite** compatible con el esquema de la app Android (tabla `usuario`), navegación independiente entre Login y App, tema claro/oscuro global y soporte de variantes (`Preventa`/`Autoventa`/`Despachos`) desde una sola base de código.

## Arquitectura

```
SistemaILP.Ruteo.sln
src/
  SistemaILP.Ruteo.Domain          -> Entidades (Usuario, mapeada 1:1 a la tabla "usuario" de Android). Sin dependencias.
  SistemaILP.Ruteo.Application     -> Configuracion central (AppVariant/AppConfiguration), DTOs del WS,
                                       interfaces (puertos) y casos de uso (AuthService).
  SistemaILP.Ruteo.Infrastructure  -> EF Core + SQLite, repositorios, AuthenticationStateProvider,
                                       cliente HTTP del login real (LoginWebServiceClient).
  SistemaILP.Ruteo.UI              -> Razor Class Library con las páginas y layouts (MudBlazor).
  SistemaILP.Ruteo.Maui            -> Proyecto "head" MAUI (Android/iOS/MacCatalyst/Windows): wiring de DI,
                                       resuelve la variante activa y construye AppConfiguration.
tests/
  SistemaILP.Ruteo.Application.Tests -> Pruebas unitarias (xUnit) de AuthService con dobles en memoria.
```

Regla de dependencias (Clean Architecture): `Maui` → `UI`/`Infrastructure` → `Application` → `Domain`. La UI solo conoce las interfaces de `Application`; nunca referencia `Infrastructure` directamente, ni contiene URLs, SQL ni lógica de variante.

## Funcionalidades incluidas

- **Login real** contra el Web Service (`POST validarUsuario`), con los mismos DTOs/JSON que la app Android (`WsLoginDTO`, `WsResultDTO`, `ConfiguracionDTO`).
- **Persistencia en SQLite compatible con Android**: tabla `usuario` con las mismas columnas (`asCodigoUsuario`, `vendedor`, `nombre`, `passW`, `sesionActiva`, `ultimaSincronizacion`).
- **Sesión persistida y recuperada al reabrir la app** (pantalla de arranque `Splash.razor` que verifica la sesión antes de mostrar Login o la App).
- **Logout** = elimina la fila de sesión activa en `usuario` (no borra catálogos ni otros datos).
- **Login y Aplicación con navegación totalmente independiente**: menú de 3 puntos exclusivo del login (`LoginLayout.razor`) vs. barra de navegación inferior exclusiva de la app (`MainLayout.razor`).
- **Tema claro/oscuro global** persistido entre sesiones (`ThemeState` + `IPreferencesService`), paleta basada en Industria La Popular.
- **Variantes** (`Preventa`/`Autoventa`/`Despachos`) resueltas en compilación vía propiedad MSBuild `AppVariant`, sin tocar Razor.
- **Orientación vertical forzada** (Android/iOS/MacCatalyst).
- **Toda la UI en MudBlazor**, sin emojis.

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
| Consumir otro Web Service                     | Nueva interfaz + implementación siguiendo el patrón de `ILoginWebServiceClient`/`LoginWebServiceClient`, registrar con `AddHttpClient<TInterfaz, TImpl>()` en `Infrastructure/DependencyInjection.cs` |
| Agregar una página                            | `.razor` en `UI/Pages`, usando componentes `Mud*`; protégela con `@attribute [Authorize]` si requiere sesión |
| Cambiar la Base URL / la variante             | `MauiProgram.BuildAppConfiguration()` (Base URL real pendiente de configurar) y propiedad MSBuild `AppVariant` en `SistemaILP.Ruteo.Maui.csproj` |
| Cambiar la ruta/nombre de la base de datos    | `MauiProgram.BuildAppConfiguration()` (`DatabaseConfiguration`) |

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

## Pendiente de configurar

- **Base URL real del Web Service**: `MauiProgram.BuildAppConfiguration()` tiene un placeholder obvio (`https://pendiente-configurar-base-url.example/api/`) que hay que reemplazar antes de poder loguear de verdad.

## Notas

- La contraseña se guarda tal cual la envía el usuario (texto plano) en la columna `passW` de la tabla `usuario` - es el mismo comportamiento de la app Android existente, replicado literalmente en esta migración (no es una decisión de seguridad de este proyecto).
- El campo `imei` del login está temporalmente hardcodeado a `"000000000000000"`, igual que en el código Android actual (comentario original: "mientras no se tengan los dispositivos disponibles").
