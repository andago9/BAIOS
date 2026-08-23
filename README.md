# BAIOS

**Blinter All In One Security** — Security & Diagnostic Toolkit para Windows.

BAIOS es un centro de operaciones que orquesta diagnóstico, seguridad y mantenimiento del equipo y genera un reporte por sesión. **No es un antivirus** y no sustituye Microsoft Defender ni otros motores: los usa, lanza herramientas de terceros y deja constancia.

Un motor, dos modos (desde 4.0): **Hogar** (dashboard y acciones seguras) y **Técnico** (flujo USB/PC hasta el reporte).

Documentación de producto:

- [Manual de usuario](docs/usuario.md)
- [Visión](docs/vision.md)
- [Roadmap 4.0 / 4.1 / 4.2](docs/roadmap.md)
- [Backlog](docs/backlog.md)
- Sitio: [landing local](website/index.html) · vigente en [Google Sites](https://sites.google.com/view/blinter-baios) hasta publicar (WEB-02 / WEB-03)
- [Catálogo de herramientas](docs/catalogo.md)
- [Arquitectura](docs/arquitectura.md) · [Diseño funcional](docs/diseno-funcional.md) · [Actualización](docs/actualizacion.md) · [Reportes](docs/reportes.md) · [Seguridad](docs/seguridad.md)

## Estado del repositorio

El código está en [`src/BAIOS.sln`](src/BAIOS.sln) (.NET 8, WPF). El prototipo WinForms `test2` se retiró en F5-06 (historia en git y `docs/archive/`).

Licencia del motor: [GNU GPL v3](LICENSE). Las herramientas de terceros conservan la suya.

## Cómo abrir BAIOS 4

1. Instala el [SDK de .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) (en esta máquina puede haber solo el *runtime*; hace falta el SDK para `dotnet build`).
2. Abre [`src/BAIOS.sln`](src/BAIOS.sln) en Visual Studio 2022 o:

```text
dotnet build src/BAIOS.sln
dotnet run --project src/BAIOS.App/BAIOS.App.csproj
```

Publicación portable (Hogar + Technician Edition) e instalación en `%LOCALAPPDATA%\BAIOS`:

```text
powershell -File scripts/publish.ps1
powershell -File scripts/install.ps1
```

El ensamblado se llama `BAIOS.exe`.

## Uso

Arranque: bienvenida → acuerdo (si no aceptas, no entra al motor) → Hogar o Técnico → shell. Detalle: [manual de usuario](docs/usuario.md).

Coloca los binarios en `src/BAIOS.App/Tools/<id>/` para que el publish los copie junto al exe (`dist/BAIOS/Tools/<id>/`). No se embeben en `BAIOS.exe`. Si la ficha tiene URL https y `sha256` de 64 hex, usa **Instalar / Actualizar**. Un hash que no coincida impide instalar y ejecutar. `manifestUrl` en `config.json` refresca el manifiesto remoto. Cada lanzamiento deja una línea en `Tools/<id>/execution.log` (modo Técnico: **Ver log**). Cola operativa: [backlog OPS](docs/backlog.md).

## Requisitos

Windows x64. SDK .NET 8 para compilar. El publish self-contained no pide instalar .NET en el PC de destino.

## Contacto

blinter.baios@gmail.com

BAIOS no es un sitio PHP ni necesita XAMPP; el clone puede vivir en `htdocs` solo por hábito de Git. La landing estática está en [`website/`](website/index.html).
