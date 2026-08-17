# Roadmap de BAIOS 4

Fuente de producto: [visión](vision.md). Trabajo pendiente: [backlog](backlog.md).

Orden: **DOC + F1 + F2 + F3-01 hechos**. Siguiente: **F3-02**. El prototipo WinForms `test2` permanece congelado (sin features).

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

No incluye: auto-updater de portables, instalador MSI pulido, LiveCD, edición empresarial, embebido masivo de antivirus.

Backlog: **F3**. DOC + F1 + F2 están firmes; F3 es la siguiente fase.

## 4.1 — Motor de herramientas

- Manifiesto JSON: nombre, versión, arquitectura, URL, sha256, categoría, admin, cómo interpretar la salida.
- Descarga, verificación de integridad, versionado. Reemplazar una herramienta sin recompilar BAIOS.
- Logs de ejecución.
- Más fichas del [catálogo](catalogo.md) que F1 marque como vigentes.

Backlog: **F4**. Ver [actualización](actualizacion.md) y [seguridad](seguridad.md).

## 4.2 — Distribución y rescate

- Instalador y actualizador del motor.
- Perfil USB / Technician Edition como **empaquetado** del mismo código, no otro producto.
- Rescue / LiveCD: catálogo de enlaces oficiales, no ISO embebidas.
- Logs y UX de producto; documentación de usuario.

Backlog: **F5**. Empresa: explícitamente más allá de 4.2.

## Fuera de alcance (hasta nuevo aviso)

| Ítem | Nota |
| --- | --- |
| Edición empresarial | Posterior a 4.2 |
| Recopilación masiva tipo «BAIOS Full» ISO | Sustituida por manifiesto |
| Restaurar Form4 / Form5 de Lite | Producto 3 archivado |
| Reimplementar motores de terceros | Orquestar, no reemplazar |
| Mini blog Blogger | Distribución; no es producto 4.x |
| Personalizar el `.exe` de cada herramienta embebida | Incompatible con manifiesto |

## Mapa de ediciones antiguas

| Antes (v3 / Lite) | Ahora |
| --- | --- |
| BAIOS Lite | Modo Hogar (4.0) |
| BAIOS Full | Repositorio / manifiesto (4.1) |
| BAIOS Rescue | Enlaces LiveCD (4.2+) |
