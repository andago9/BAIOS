# BAIOS

**Blinter All In One Security** — Security & Diagnostic Toolkit para Windows.

BAIOS es un centro de operaciones que orquesta diagnóstico, seguridad y mantenimiento del equipo y genera un reporte por sesión. **No es un antivirus** y no sustituye Microsoft Defender ni otros motores: los usa, lanza herramientas de terceros y deja constancia.

Un motor, dos modos (desde 4.0): **Hogar** (dashboard y acciones seguras) y **Técnico** (flujo USB/PC hasta el reporte).

Documentación de producto:

- [Visión](docs/vision.md)
- [Roadmap 4.0 / 4.1 / 4.2](docs/roadmap.md)
- [Backlog](docs/backlog.md)
- [Catálogo de herramientas](docs/catalogo.md)
- [Arquitectura](docs/arquitectura.md) · [Diseño funcional](docs/diseno-funcional.md) · [Actualización](docs/actualizacion.md) · [Reportes](docs/reportes.md) · [Seguridad](docs/seguridad.md)

## Estado del repositorio

El código de **BAIOS 4** está en [`src/BAIOS.sln`](src/BAIOS.sln) (.NET 8, WPF). Esqueleto F3-01: ventana mínima, aún sin dashboard ni herramientas.

El código en `BAIOS/test2` es el prototipo **BAIOS 3 / 6 Alpha** (WinForms, .NET Framework 4.7.2). Está **congelado**. No es BAIOS 4.

Licencia del motor: [GNU GPL v3](LICENSE). Las herramientas de terceros conservan la suya.

## Cómo abrir BAIOS 4

1. Instala el [SDK de .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) (en esta máquina puede haber solo el *runtime*; hace falta el SDK para `dotnet build`).
2. Abre [`src/BAIOS.sln`](src/BAIOS.sln) en Visual Studio 2022 o:

```text
dotnet build src/BAIOS.sln
dotnet run --project src/BAIOS.App/BAIOS.App.csproj
```

Publicación portable self-contained (`win-x64`):

```text
dotnet publish src/BAIOS.App/BAIOS.App.csproj -p:PublishProfile=win-x64
```

El ensamblado se llama `BAIOS.exe`, no `test2`.

## Uso (producto 4.0)

El flujo completo (acuerdo, modos, diagnóstico, reporte) se implementa en F3-02…F3-11. Hoy la app solo muestra que el motor 4.0 arranca.

El `.exe` del prototipo `test2` sigue el flujo antiguo (bienvenida → acuerdo → menú) y está incompleto.

## Requisitos

**BAIOS 4:** Windows x64, SDK .NET 8 para compilar. El publish self-contained no pide instalar .NET en el PC de destino.

**Prototipo test2:** .NET Framework 4.7.2. Mínimo histórico: 1 GB RAM, 4 GB de disco, 2 núcleos a 1,6 GHz.

## Contacto

blinter.baios@gmail.com

BAIOS no es un sitio PHP ni necesita XAMPP; el clone puede vivir en `htdocs` solo por hábito de Git.
