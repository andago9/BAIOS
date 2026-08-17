# Sistema de reportes

Esquema **cerrado** en F2-06. MVP: [F3-08](backlog.md).

## Objetivo

Una sesión → un informe que el técnico deja en el PC o se lleva. 4.0: **HTML canónico** + **TXT** gemelo. PDF: 4.1+ si hace falta.

## Modelo (`Finding`)

| Campo | Valores |
| --- | --- |
| `section` | `security` \| `storage` \| `network` \| `system` \| `maintenance` |
| `severity` | `ok` \| `warning` \| `fail` |
| `title` | Corto |
| `detail` | Opcional |
| `source` | `native:<modulo>` o `tool:<id>` |

La sesión añade cabecera y `recommendations[]` (strings).

## Cabecera

- Producto: BAIOS — Blinter All In One Security
- Hostname
- Edición de Windows
- Fecha/hora local
- Modo: Hogar / Técnico
- Versión del motor

## Secciones (orden fijo)

1. **Seguridad** — Defender, firewall, herramientas lanzadas y su código de salida.
2. **Almacenamiento** — espacio, SMART si aplica.
3. **Red** — conectividad, DNS, gateway.
4. **Sistema** — CPU/RAM, inicio, drivers con problema.
5. **Mantenimiento** — qué se ejecutó (SFC, DISM, limpieza) y resultado.
6. **Recomendaciones** — lista accionable.

Tono (ejemplo, no plantilla literal de UI):

```
SEGURIDAD
OK  Microsoft Defender activo
OK  Firewall activo
OK  Sin amenazas reportadas por el motor

ALMACENAMIENTO
OK  Disco accesible
AVISO  Espacio disponible: 18%

RED
OK  Conectividad
OK  DNS

RECOMENDACIONES
- Liberar espacio
- Revisar programas de inicio (Autoruns)
```

## Almacenamiento

- Portable: `Reports/` junto al exe.
- Instalado (4.2): `%LOCALAPPDATA%\BAIOS\Reports\`.
- Nombre: `BAIOS-<hostname>-<yyyyMMdd-HHmm>.html` y `.txt`.

## Exportar

4.0: abrir carpeta / abrir HTML. 4.2: al cerrar el diagnóstico completo en Técnico, ofrecer «abrir informe».

## Privacidad

Puede incluir hostname y adaptadores. Sin secretos, sin telemetría en 4.0. Aviso en Hogar antes de guardar. Ver [seguridad](seguridad.md).
