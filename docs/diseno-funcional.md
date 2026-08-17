# Diseño funcional

Pantallas y comportamiento **cerrados** en F2-05. Producto: [visión](vision.md). Catálogo: [catalogo.md](catalogo.md).

## Flujo común

1. Bienvenida: nombre, versión del motor, GPL-3.0 con enlace a `LICENSE`.
2. Acuerdo de uso: falsos positivos, no borrar a ciegas, terceros con su EULA. «No acepto» no cierra la app; no entra al motor.
3. Selector **Hogar** / **Técnico**. Si `ModeDefault=Technician` o se detecta extraíble (perfil USB 4.2), preseleccionar Técnico.
4. Shell: Inicio | Seguridad | Diagnóstico | Mantenimiento | Red | Herramientas | Reportes.

Herencia v3: acuerdo y avisos. No se replica el menú Portables / Ejecutables / Online.

## Inicio

| | Hogar | Técnico |
| --- | --- | --- |
| Contenido | Semáforos: Windows Update, Defender, firewall, disco, RAM; 1–3 alertas | Mismas señales + cifras (espacio %, último análisis, build de Windows) |
| Acción principal | «Revisar equipo» (recorre módulos en modo seguro) | «Diagnóstico completo» (F3-10): Seguridad → Diagnóstico → Red → Mantenimiento opcional → Reporte |

## Seguridad

- Defender: estado (activo/pasivo/ausente), análisis rápido, completo; offline si el SO lo permite.
- Lanzar AdwCleaner y Microsoft Safety Scanner (núcleo). Aviso MSERT: caduca ~10 días.
- No recomendar desactivar Defender de forma permanente.

Técnico: códigos de salida y hora de cada lanzamiento. Hogar: «Ejecutar limpieza de adware» / «Segunda opinión Microsoft».

## Diagnóstico

Hardware (CPU, RAM, discos), SMART si WMI lo expone, servicios, procesos, programas de inicio, drivers con problema.

- Hogar: resumen + «ver detalle».
- Técnico: tablas.
- Autoruns: botón en Diagnóstico (y duplicado en Herramientas). BAIOS no clona la UI de Autoruns.

## Mantenimiento

Nada se ejecuta al abrir la pantalla.

| Acción | Confirmación | Admin |
| --- | --- | --- |
| Temporales, cachés, papelera | Sí, lista de qué se borra | No / según carpeta |
| DISM / SFC | Sí, texto de que puede tardar | Sí |
| CHKDSK | Sí, **advertencia de reinicio y duración** | Sí |

Hogar: confirmación más larga. Técnico: mismas acciones, menos copy.

## Red

IP, DNS, gateway, ping a un host fijo (p. ej. `1.1.1.1` y el gateway), tracert opcional, flush DNS, renovar DHCP, «¿Hay internet?».

- Hogar: conectividad y DNS en lenguaje claro.
- Técnico: salida de comando en panel.

## Herramientas

Ficha visible: nombre, versión si se conoce, fabricante, URL, licencia, admin, arquitectura.

4.0: **Ejecutar** y **Manual** (URL). Núcleo: AdwCleaner, MSERT, Autoruns.

4.1: estado de versión y «Actualizar» según manifiesto.

Modo Hogar: solo el núcleo. Técnico: núcleo + (4.1) el resto vigente + enlaces online (VirusTotal, ESET).

## Reportes

Lista de informes; abrir HTML; abrir carpeta. El flujo Técnico termina aquí de forma explícita. Esquema: [reportes](reportes.md).

## Online

Sin vistas propias. Técnico: enlaces VirusTotal y ESET Online Scanner. Jotti / Hybrid Analysis opcionales. Panda no en 4.0.
