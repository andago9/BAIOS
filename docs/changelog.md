# Changelog

Historial de BAIOS. Este archivo es la **fuente de verdad**. Formato: [Keep a Changelog](https://keepachangelog.com/es/1.1.0/).

Hay dos HTML de origen (mismo CSS / Bootstrap / html5-editor):

- [`archive/Changelog-v3.html`](archive/Changelog-v3.html) — snapshot de **solo v3** (11 jul 2022): migración AutoPlay → Visual Studio, leyenda de tipos y pendientes. Sin historial previo. Pie «Copyright» a medias.
- [`archive/Changelog.html`](archive/Changelog.html) — el mismo encabezado de v3 más el historial AutoPlay pegado debajo (duplicados, tabla vacía «Cell 2x2», una «versión 1.0.0» contaminada con 1.5.0–2.0.1).

Las fechas de 0.0.1–1.x son inciertas: el HTML compilado copiaba bloques enteros. Si hay conflicto, gana la entrada más específica. Los pendientes no van aquí: están en el [backlog](backlog.md) (P0–P3 archivados; F1–F2 hechos; trabajo vivo F3–F5).

## Tipos

Definición original del HTML, alineada con Keep a Changelog:

| Tipo | Significado |
| --- | --- |
| **Added** | Algo nuevo en archivos o código. |
| **Changed** | Cambio de un elemento ya existente. |
| **Fixed** | Corrección de errores (pruebas o usuarios). |
| **Upgraded** | Actualización de un complemento, plugin o herramienta necesaria. |
| **Removed** | Archivo, aplicativo o código retirado. |

Solo se listan las secciones que tienen entradas. No se rellenan categorías vacías.

## Índice

| Versión | Fecha | Edición / motor | Nota |
| --- | --- | --- | --- |
| [Unreleased](#unreleased) | — | BAIOS 4 (definición) | No publicado |
| [6.0.1-alpha](#601-alpha) | — | Prototipo WinForms (`test2`) | Tag git `V6.0.1`; congelado |
| [3](#3---2022-07-11) | 2022-07-11 | C# WinForms | Snapshot propio; última release histórica |
| [2.1.2](#212---2022-04-13) | 2022-04-13 | Lite (AutoPlay) | |
| [2.1.1](#211---2022-01-10) | 2022-01-10 | Lite (AutoPlay) | |
| [2.1.0](#210---2022-01-06) | 2022-01-06 | BAIOS (AutoPlay) | Retiro de Vipre |
| [2.0.1](#201---2021-08-30) | 2021-08-30 | BAIOS (AutoPlay) | |
| [2.0.0](#200---2021-08-20) | 2021-08-20 | BAIOS (AutoPlay) | En el HTML aparece como «V 2» |
| [1.8.0](#180) | no documentada | AutoPlay | |
| [1.5.0](#150) | no documentada | AutoPlay | |
| [1.3.0](#130) | no documentada | AutoPlay | |
| [1.2.0](#120) | no documentada | AutoPlay | |
| [1.1.0](#110) | no documentada | AutoPlay | |
| [0.0.1](#001) | ~2019–2021 | AutoPlay | Primera versión documentada |

## [Unreleased]

Definición de **BAIOS 4** (visión, arquitectura, catálogo, roadmap). F1 y F2 cerrados (2026-08-17). **F3-01:** esqueleto [`src/BAIOS.sln`](../src/BAIOS.sln) (.NET 8 + WPF, `BAIOS.exe`). El prototipo `test2` no recibe features. Siguiente: F3-02. Detalle: [roadmap](roadmap.md) y [backlog](backlog.md).

## [6.0.1-alpha]

Código actual del repo (tag git `V6.0.1`). El HTML **no cubría** esta etapa.

### Notes

- UI de bienvenida: `VERSION: 6 Alpha`, `NAME: BAIOS`.
- Ensamblado: `1.0.0.0` (no alineado con la UI; P3-03 archivado).
- Solución Visual Studio: proyecto `test2`, .NET Framework 4.7.2, C# WinForms (el HTML de v3 decía «visual basic»; el repo es C#).
- Formularios Online/Portables (`Form4` / `Form5`) referenciados y sin fuente; referencias muertas eliminadas en higiene.

## [3] - 2022-07-11

Autor: Andago. Fuente: [`archive/Changelog-v3.html`](archive/Changelog-v3.html).

Al migrar se trató el changelog como documento nuevo: este snapshot no trae 0.0.1–2.1.2. El historial AutoPlay vive en el otro HTML y en las secciones de abajo.

### Changed

- Migración desde AutoPlay Media Studio 8 a Visual Studio por caducidad / licencia de AutoPlay. Desarrollo desde cero (WinForms). El HTML lo etiqueta `Upgrade` y dice «visual basic»; el repo es C# WinForms.

### Notes

- Leyenda de tipos en v3: Add, Change, Fix, Upgrade. **Removed** no está en esta tabla (sí se usó en 2.1.0 / 2.0.0).
- Pendientes de ese momento (Lite ~500 MB, Full/Rescue, alertas, blogger, etc.): no son cambios de v3; están en el backlog archivado. El HTML compilado añade siete ítems más (readme, Vipre, `00.antivirus.txt`, Online, etiquetas ejecutable/portable/online).

## [2.1.2] - 2022-04-13

BAIOS Lite. Autor: Andago.

### Added

- Enlace en el logo hacia la web.
- Archivo *Acerca de* (HTML) con información, créditos y lista del repositorio.

### Fixed

- En Acuerdo de uso, «No acepto» cerraba la aplicación; ahora permanece en la misma pantalla.

## [2.1.1] - 2022-01-10

BAIOS Lite. Autor: Andago.

### Added

- Manuales de uso de aplicativos en Detalles de software.

### Changed

- Actualización de Detalles de software.

## [2.1.0] - 2022-01-06

Autor: Andago. Motor: AutoPlay Media Studio 8.

### Upgraded

Portables (varios pasan a tratarse como ejecutables):

- AdwCleaner
- HijackThis
- McAfee Stinger
- NoBot
- HitmanPro
- Kaspersky Virus Removal Tool
- Norton Power Eraser
- Microsoft Safety Scanner
- ZHPCleaner

Repositorio LiveCD / booteable:

- Avira Rescue System
- Comodo Rescue Disk
- Dr.Web LiveDisk
- ESET SysRescue Live
- Kaspersky Rescue Disk 18
- Norton Bootable Recovery Tool
- Panda Rescue Cloud Cleaner
- Sophos SBAV
- Trend Micro Rescue Disk
- VBA32 Rescue

### Removed

- Vipre / Viper Rescue.

### Notes

- Todo el software añadido se prueba previamente.

## [2.0.1] - 2021-08-30

Autor: Andago.

El bloque `<details>` del HTML repetía aquí iconos, *Acerca de*, color `#006980` y el diálogo de Detalles: eso pertenece a [1.5.0](#150) y [1.8.0](#180). Aquí solo lo propio de esta versión.

### Added

- Botones adelante / atrás en los formularios.
- Logo BAIOS en las pantallas.
- Lista de aplicativos ejecutables.
- Lista de instaladores con enlaces.

### Changed

- Advertencias de riesgo en aplicativos (el HTML corta el texto en «advertencia que algunos aplicat»).

## [2.0.0] - 2021-08-20

Autor: Andago. En el HTML: «Version: 2».

### Changed

- Formato de changelog con tipos Add / Change / Fix / Upgrade / Remove.
- Bienvenida (introducción del AIO).
- Acuerdo de uso (el usuario debe ser consciente de los riesgos).
- Menú.
- Portables (con imágenes).
- Online (aún sin formato).

### Upgraded

Listado de software ejecutable y últimas versiones descargadas:

- AdwCleaner
- HijackThis
- McAfee Stinger
- NoBot
- HitmanPro
- Kaspersky Virus Removal Tool
- Norton Power Eraser
- Microsoft Safety Scanner
- IObit Malware Fighter
- K7 Disinfector
- Spybot

### Removed

- Vipre Rescue (el HTML lo anuncia aquí; la retirada queda explícita en 2.1.0).

## [1.8.0]

### Changed

- Color de fondo `#006980`.
- Disposición del menú.
- Animación de iconos (cursor tipo mano).
- Logos de marca Andago y velosergio en *Acerca de*.
- Detalles de software: de TXT a ventana de diálogo.

## [1.5.0]

### Added

- Pack de iconos de navegación (adelante / atrás). Autor: [srip](https://www.flaticon.es/autores/srip) / [Flaticon](https://www.flaticon.es/) ([pack UI](https://www.flaticon.es/packs/ui-217)).
- Pack de iconos de tipos de archivo. Autor: [Smashicons](https://www.flaticon.es/autores/smashicons) / [Flaticon](https://www.flaticon.es/) ([pack file-type](https://www.flaticon.es/packs/file-type-set)).
- Diálogo *Acerca de* (versión y créditos).

### Changed

- Color de fondo `#6B626F`.
- Títulos: Franklin Gothic Semi Bold 20, antialias, script occidental, `#FFFFFF`.
- Párrafos: Bahnschrift Condensed 12, antialias, script occidental, `#FFFFFF`.

## [1.3.0]

### Added

- Opción Online en el menú (ESET y Panda).
- Archivo de listado de software antivirus.

## [1.2.0]

### Added

- Malwarebytes, ClamWin, Emsisoft Emergency Kit, Spybot Search & Destroy, Vipre Rescue.
- Páginas Ejecutables y Portables.
- Detalles de software (versiones y fechas de prueba).

### Changed

- Color general a naranja.
- Menú con listas de portables y ejecutables.

## [1.1.0]

Autor: Andago (Blinter).

### Added

- Kaspersky Virus Removal Tool, Norton Power Eraser, Microsoft Safety Scanner, NoBot, HitmanPro.
- Botón Salir.
- Logos en PNG.

### Changed

- Color general.
- Acuerdo de uso / página de riesgos con selector (radio) para aceptar o no.

### Notes

- Motor: AutoPlay Media Studio 8.
- Todo el software incluido se prueba previamente.

## [0.0.1]

Primera versión documentada. Autor: Andago. Motor: AutoPlay Media Studio 8.

El HTML archivado la etiqueta `0.0.1` (fecha copiada `10 Jan 2022`, la misma que 2.1.1: no es fiable). Un borrador posterior la relabeló `1.0.0` y le pegó iconos, *Acerca de*, color `#006980` y el diálogo de Detalles; esos ítems viven en 1.5.0 / 1.8.0.

### Added

- Portables iniciales: AdwCleaner, HijackThis, McAfee Stinger, NoBot, HitmanPro.
- Pantalla de bienvenida (versión, copyright, autores, logo; agradecimiento a velosergio).
- Acuerdo de uso con selección para continuar.
- Menú con portables y Detalles de software.
- README para GitHub y este changelog.
- Botones Regresar / Salir.

### Notes

- Todo el software añadido se prueba previamente.
- El software de terceros es propiedad de sus autores; BAIOS usa ediciones gratuitas o de prueba.
- Copyright del pie HTML: 2019–2023 Andago.
