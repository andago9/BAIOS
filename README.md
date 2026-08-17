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

El código en `BAIOS/test2` es el prototipo **BAIOS 3 / 6 Alpha** (WinForms, .NET Framework 4.7.2). Está **congelado**. No es BAIOS 4; no se restauran las pantallas Lite que faltan.

BAIOS 4 se define ahora en `docs/`. El código nuevo empieza en la fase F3 del backlog, cuando arquitectura y catálogo vigente estén firmes.

Licencia del motor: [GNU GPL v3](LICENSE). Las herramientas de terceros conservan la suya.

## Uso (cuando exista 4.0)

1. Copia de seguridad de lo importante.
2. No desactives Defender de forma permanente.
3. Acepta el acuerdo de uso (falsos positivos; no borres a ciegas).
4. Elige modo Hogar o Técnico.
5. Diagnóstico → seguridad → hardware → red → mantenimiento → reporte.

Hasta entonces, el `.exe` del prototipo sigue el flujo antiguo (bienvenida → acuerdo → menú de herramientas) y está incompleto.

## Requisitos (histórico del prototipo)

Mínimo: 1 GB RAM, 4 GB de disco, 2 núcleos a 1,6 GHz. Recomendado: 4 GB RAM, 8 GB de disco, 4 núcleos a 2,6 GHz. .NET Framework 4.7.2 para `test2`. BAIOS 4 apuntará a .NET 8 self-contained (ver arquitectura).

Algunos aplicativos de terceros piden red o privilegios de administrador.

## Contacto

blinter.baios@gmail.com

BAIOS no es un sitio PHP ni necesita XAMPP; el clone puede vivir en `htdocs` solo por hábito de Git.
