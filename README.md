# Piloto CI/CD Vida Ley

Cascaron minimo de una aplicacion ASP.NET Core 10 para demostrar CI/CD hacia IIS, sin Docker ni logica de negocio de Vida Ley.

## Estructura

- `src/PilotoVidaLey`: aplicacion web con pagina raiz y endpoint `/health`.
- `tests/PilotoVidaLey.Tests`: prueba de integracion del endpoint de salud.
- `.github/workflows/piloto-ci-cd.yml`: compilacion, pruebas, publicacion y despliegue.

## Ejecucion local

Requiere el SDK de .NET 10.

```powershell
dotnet restore PilotoVidaLey.slnx
dotnet test PilotoVidaLey.slnx --configuration Release
dotnet run --project src/PilotoVidaLey
```

La aplicacion responde en `/` y `/health`.

## Requisitos para desplegar en IIS

1. IIS y el **.NET 10 Hosting Bundle** deben estar instalados en Windows Server; se recomienda configurar el application pool con **No Managed Code**.
2. Crear el sitio IIS y su directorio fisico. La identidad del application pool debe tener permisos de escritura en ese directorio.
3. Registrar un runner auto-hospedado de GitHub Actions en el mismo servidor IIS con las etiquetas `self-hosted`, `windows` y `pilot-iis`. Este cascaron despliega localmente; no configura acceso remoto. La cuenta del servicio del runner necesita permisos para administrar el application pool y escribir en el directorio del sitio.
4. En **Settings > Secrets and variables > Actions > Variables**, configurar `IIS_SITE_NAME` (nombre exacto del sitio) y `IIS_DEPLOY_PATH` (ruta fisica existente del sitio). El pipeline verifica que ambas apunten al mismo sitio antes de desplegar.
5. Al hacer push a `piloto`, se ejecuta CI y, si pasa, CD actualiza el directorio del sitio. Los pull requests solo ejecutan CI.

No almacenar credenciales en el repositorio. Este ejemplo copia los archivos publicados al directorio configurado y conserva archivos que ya existan allí; validar el procedimiento de respaldo y recuperacion antes de usarlo con datos o trafico real.
