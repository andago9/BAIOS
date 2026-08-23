# Roadmap de BAIOS 4

Fuente de producto: [visión](vision.md). Trabajo pendiente: [backlog](backlog.md).

Orden: **DOC + F1–F5 cerrados**. Cola actual: **OPS** en el [backlog](backlog.md). No hay edición empresarial. El prototipo WinForms `test2` se retiró del árbol.

## 4.0 — MVP usable en ambos modos

Motor nativo primero. Herramientas externas: **lanzar, no actualizar**.

Incluye:

- Inicio: Windows Update, Defender, firewall, disco, RAM/CPU, alertas, último análisis.
- Seguridad nativa: estado y análisis de Microsoft Defender (rápido / completo; offline si aplica).
- Diagnóstico nativo: hardware, discos/SMART si es accesible, servicios, procesos, inicio, drivers.
- Mantenimiento nativo (con confirmación): temporales, cachés, papelera; DISM / SFC; CHKDSK solo advertido.
- Red: IP, DNS, gateway, ping, tracert, flush DNS, renovar DHCP, conectividad básica.
- Reportes: un informe por sesión (texto/HTML), OK / aviso / fallo y recomendaciones.
- Herramientas externas (lanzamiento): AdwCleaner, Microsoft Safety Scanner, Autoruns. HijackThis en **4.1** (F1 vigente + alerta de falsos positivos). Norton Power Eraser **fuera** (EOL 2026-04-30).
- Modos Hogar y Técnico: mismas pantallas, distinta densidad; flujo «diagnóstico completo» en Técnico.

No incluye: auto-updater de portables, instalador MSI pulido, LiveCD embebido, embebido masivo de antivirus.

Backlog: **F3** (cerrada).

## 4.1 — Motor de herramientas

- Manifiesto JSON: nombre, versión, arquitectura, URL, sha256, categoría, admin, cómo interpretar la salida.
- Descarga, verificación de integridad, versionado. Reemplazar una herramienta sin recompilar BAIOS.
- Logs de ejecución (`Tools/<id>/execution.log`).
- Fichas vigentes de F1-03 (HijackThis con alerta de falsos positivos; KVRT, HitmanPro, ZHPCleaner, EEK; Spybot y Malwarebytes como instaladores en Técnico).

Backlog: **F4** (cerrada).

## 4.2 — Distribución y rescate

- Instalador (copia a `%LOCALAPPDATA%\BAIOS`, sin MSI) y actualizador del motor (`engine.json` + sha256).
- Perfil USB / Technician Edition como **empaquetado** del mismo código (`dist/BAIOS.Technician/`).
- Rescue / LiveCD: catálogo de enlaces oficiales, no ISO embebidas.
- Logs y UX de producto; [documentación de usuario](usuario.md).

Backlog: **F5** (cerrada). Ver [actualización](actualizacion.md) y [seguridad](seguridad.md).

## Después de 4.2 — Operación (no es versión nueva)

El código de 4.0 / 4.1 / 4.2 está cerrado. Queda sembrar herramientas, hashes y publicar.

- Colocar binarios oficiales en `src/BAIOS.App/Tools/<id>/` (no dentro del `.exe`). `publish.ps1` los copia a `dist/BAIOS/` y `dist/BAIOS.Technician/`.
- Rellenar `sha256` en el manifiesto para que **Instalar / Actualizar** descargue.
- Primera release 4.0 (hoy: Unreleased / `4.0.0-dev`).
- Validar cada herramienta en un PC real.
- Seguir clasificando el [inventario](inventario.md) de disco.

Backlog: **OPS**.

## Presencia web (no es versión de motor)

Sustituye el Google Sites (`sites.google.com/view/blinter-baios`) y el mini blog Blogger (P2-05, archivado).

- Fase 1: landing estática local en [`website/`](../website/) (estructura del sitio actual, copy BAIOS 4, tema Adminox Landing).
- Después: publicar (p. ej. GitHub Pages), apuntar la app al URL público y retirar Google Sites.

Fuera de alcance: blog, panel Adminox, CMS.

Backlog: **WEB**.

## Fuera de alcance (hasta nuevo aviso)

| Ítem | Nota |
| --- | --- |
| Edición empresarial | No habrá. Un motor, dos modos (Hogar / Técnico). |
| Recopilación masiva tipo «BAIOS Full» ISO | Sustituida por manifiesto |
| Restaurar Form4 / Form5 de Lite | Producto 3 archivado |
| Reimplementar motores de terceros | Orquestar, no reemplazar |
| Mini blog Blogger | Archivado (P2-05). Sustituido por la cola WEB. |
| Personalizar el `.exe` de cada herramienta embebida | Incompatible con manifiesto |

## Mapa de ediciones antiguas

| Antes (v3 / Lite) | Ahora |
| --- | --- |
| BAIOS Lite | Modo Hogar (4.0) |
| BAIOS Full | Repositorio / manifiesto (4.1) |
| BAIOS Rescue | Enlaces LiveCD (4.2+) |
