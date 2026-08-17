# Sistema de reportes

Esqueleto. Esquema cerrado en [F2-06](backlog.md); MVP en [F3-08](backlog.md). El reporte es lo que convierte BAIOS de colección de programas en herramienta de diagnóstico.

## Objetivo

Cada sesión genera un informe BAIOS que un técnico puede dejar en el PC o llevarse. Formato 4.0: texto y HTML. PDF: 4.1+ si hace falta.

## Cabecera

- Producto: BAIOS — Blinter All In One Security
- Equipo (hostname)
- Edición de Windows
- Fecha y hora
- Modo (Hogar / Técnico)
- Versión del motor

## Secciones

Cada hallazgo: **OK** / **aviso** / **fallo**, texto corto, origen (módulo nativo o herramienta).

1. **Seguridad** — Defender, firewall, amenazas conocidas, herramientas lanzadas.
2. **Almacenamiento** — salud del disco, SMART si aplica, espacio libre.
3. **Red** — conectividad, DNS, gateway.
4. **Sistema** — CPU/RAM, inicio, drivers con problema, servicios relevantes.
5. **Mantenimiento** — qué se ejecutó (SFC, DISM, limpieza) y código de salida.
6. **Recomendaciones** — lista accionable (liberar espacio, revisar inicio, etc.).

Ejemplo de tono (no es plantilla final):

```
SEGURIDAD
OK  Microsoft Defender activo
OK  Firewall activo
OK  Sin amenazas detectadas

ALMACENAMIENTO
OK  Disco saludable
AVISO  Espacio disponible: 18%

RED
OK  Conectividad
OK  DNS funcionando

RECOMENDACIONES
- Liberar espacio
- Revisar programas de inicio
```

## Almacenamiento

Carpeta `Reports/` junto a la app o en `%LOCALAPPDATA%\BAIOS\Reports\` si está instalada. Nombre sugerido: `BAIOS-<hostname>-<yyyyMMdd-HHmm>.html` (y `.txt` gemelo o el HTML como canónico).

## Exportar

4.0: abrir carpeta, copiar HTML/TXT. 4.2: atajo en el flujo Técnico al cerrar el diagnóstico completo.

## Privacidad

El reporte puede incluir hostname, adaptadores, rutas. No incluir secretos. Aviso en modo Hogar antes de guardar. Detalle en [seguridad](seguridad.md).
