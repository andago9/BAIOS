# Diseño funcional

Esqueleto de pantallas y comportamiento. Detalle por control en [F2-05](backlog.md). Producto: [visión](vision.md).

## Flujo común

1. Bienvenida (nombre, versión, GPL).
2. Acuerdo de uso (falsos positivos, no borrar a ciegas). «No acepto» no cierra la app; no entra al motor.
3. Selector de modo **Hogar** / **Técnico** (o default Técnico en perfil USB).
4. Shell con navegación: Inicio, Seguridad, Diagnóstico, Mantenimiento, Red, Reportes, Herramientas.

Herencia útil de v3: acuerdo explícito y avisos. No se replica el menú Portables / Ejecutables / Online.

## Inicio

Estado general: Windows actualizado, antivirus activo, firewall, espacio en disco, RAM/CPU, alertas, último análisis. En Técnico, más cifras; en Hogar, semáforos y una acción principal («Revisar equipo» / «Diagnóstico completo»).

## Seguridad

- Microsoft Defender: estado, análisis rápido, completo, offline si aplica.
- Herramientas externas del [núcleo 4.0](catalogo.md): AdwCleaner, Microsoft Safety Scanner; Autoruns también desde Diagnóstico/Inicio.
- Adware/PUP y elementos sospechosos: vía herramienta, no motor propio.

## Diagnóstico

Hardware (CPU, RAM, almacenamiento), SMART si es accesible, servicios, procesos, inicio de Windows, drivers. Técnico muestra tablas; Hogar un resumen y «ver detalle».

## Mantenimiento

Temporales, cachés, papelera, archivos innecesarios. DISM y SFC con confirmación. CHKDSK solo con advertencia (puede pedir reinicio y tardar). Nada se ejecuta al abrir la pantalla.

## Red

IP, DNS, gateway, ping, tracert, flush DNS, renovar DHCP, conectividad básica. Técnico: comandos y salida. Hogar: «¿Hay internet?» y DNS.

## Reportes

Lista de informes de sesión; abrir, exportar. Contenido en [reportes](reportes.md). El flujo Técnico termina aquí de forma explícita.

## Herramientas

Fichas: nombre, versión, fabricante, URL, licencia, admin, arquitectura. En 4.0: botón Ejecutar (y Manual si hay URL). En 4.1: estado de versión y actualizar. BAIOS no clona la UI de AdwCleaner ni de Autoruns.

## Diferencia de modos

| | Hogar | Técnico |
| --- | --- | --- |
| Densidad | Poca | Tablas y salida de comandos |
| Flujo guiado | Opcional | Diagnóstico completo hasta reporte |
| Online | Oculto o un enlace VirusTotal | Enlaces ESET / VirusTotal / etc. |
| CHKDSK / DISM | Confirmación larga | Visible, igual de advertido |
