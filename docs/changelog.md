# Changelog

Historial deduplicado de BAIOS. Formato inspirado en [Keep a Changelog](https://keepachangelog.com/es/1.1.0/).

Las entradas HTML originales (con duplicados y placeholders) están en [`archive/Changelog.html`](archive/Changelog.html). Esta es la fuente de verdad.

Las fechas de las versiones muy tempranas (0.0.1–1.x) son aproximadas: el HTML mezclaba copias. Donde hay conflicto, se usa la fecha de la entrada más específica.

## [6 Alpha] — en desarrollo

Código actual del repo (etiqueta git `V6.0.1`). El changelog HTML **no cubría** esta etapa.

### Notas

- UI de bienvenida: `VERSION: 6 Alpha`, `NAME: BAIOS`.
- Ensamblado: `1.0.0.0` (no alineado con la UI; ver [P3-03](backlog.md)).
- Solución Visual Studio: proyecto `test2`, .NET Framework 4.7.2.
- Formularios Online/Portables (`Form4` / `Form5`) referenciados y ausentes.

## [3] — 2022-07-11

Autor: Andago.

### Changed

- Migración desde Autoplay Media Studio 8 a Visual Studio (desarrollo desde cero) por caducidad / licencia de Autoplay.

## [2.1.2] — 2022-04-13

BAIOS Lite. Autor: Andago.

### Added

- Enlace en el logo hacia la web.
- Archivo *Acerca de* con información, créditos y lista del repositorio.

### Fixed

- En Acuerdo de uso, “No acepto” cerraba la aplicación; ahora permanece en la misma pantalla.

## [2.1.1] — 2022-01-10

BAIOS Lite. Autor: Andago.

### Added

- Manuales de uso de aplicativos en Detalles de software.

### Changed

- Actualización de Detalles de software.

## [2.1.0] — 2022-01-06

Autor: Andago.

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

## [2.0.1] — 2021-08-30

Autor: Andago.

### Added

- Botones adelante / atrás en los formularios.
- Logo BAIOS en las pantallas.
- Lista de aplicativos ejecutables.
- Lista de instaladores con enlaces.
- Pack de iconos de navegación (srip / Flaticon).
- Pack de iconos de tipos de archivo (Smashicons / Flaticon).
- Diálogo *Acerca de* (versión y créditos).

### Changed

- Advertencias de riesgo en aplicativos.
- Color de fondo `#006980`.
- Disposición del menú.
- Cursor tipo mano sobre iconos de interfaz.
- Logos Andago y velosergio en *Acerca de*.
- Detalles de software: de TXT a ventana de diálogo.

## [2.0.0] — 2021-08-20

Autor: Andago.

### Changed

- Formato de changelog para detallar Add / Change / Fix / Upgrade / Remove.
- Bienvenida, acuerdo de uso, menú, portables (con imágenes) y online (sin formato aún).

### Upgraded

- Listado de software ejecutable y últimas versiones descargadas (AdwCleaner, HijackThis, McAfee Stinger, NoBot, HitmanPro, Kaspersky VRT, Norton Power Eraser, Microsoft Safety Scanner, IObit Malware Fighter, K7 Disinfector, Spybot).

### Removed

- Vipre Rescue (esta entrada adelanta la retirada formal de 2.1.0).

## [1.8.0]

### Changed

- Color de fondo `#006980`.
- Disposición del menú.
- Animación de iconos (cursor mano).
- Logos de marca en *Acerca de*.
- Detalles de software como diálogo.

## [1.5.0]

### Added

- Packs de iconos (navegación y tipos de archivo).
- Diálogo *Acerca de*.

### Changed

- Color de fondo `#6B626F`.
- Tipografía: títulos Franklin Gothic Semi Bold 20 blanco; párrafos Bahnschrift Condensed 12 blanco.

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

### Added

- Kaspersky VRT, Norton Power Eraser, Microsoft Safety Scanner, NoBot, HitmanPro.
- Botón salir.
- Logos PNG.

### Changed

- Color general.
- Acuerdo de uso con selección (radio) para aceptar o no los riesgos.

## [0.0.1]

Primera versión documentada. Autor: Andago. Motor de autorun: Autoplay Media Studio 8.

### Added

- Portables iniciales: AdwCleaner, HijackThis, McAfee Stinger, NoBot, HitmanPro.
- Pantalla de bienvenida (versión, copyright, autores, logo; agradecimiento a velosergio).
- Acuerdo de uso con selección para continuar.
- Menú con portables y Detalles de software.
- README para GitHub y changelog.
- Botones regresar / salir.

### Notes

- Todo el software añadido se prueba previamente.
- El software de terceros es propiedad de sus autores; BAIOS usa ediciones gratuitas o de prueba.
