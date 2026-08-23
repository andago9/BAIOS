# Backlog

Trabajo de **BAIOS 4**. Fuente de producto: [visión](vision.md). Versiones: [roadmap](roadmap.md).

Leyenda de tipo (Keep a Changelog): **Add**, **Change**, **Fix**, **Upgrade**, **Remove**.

Estados: `pendiente` · `parcial` · `hecho` · `fuera_de_alcance_ahora` · `archivado`.

Orden: **DOC + F1–F5 cerrados**. Cola actual: **OPS** + **WEB**. No hay edición empresarial. El prototipo `test2` se retiró del árbol.

## Hecho (no reabrir)

| ID | Tipo | Nota |
| --- | --- | --- |
| DONE-01 | Add | README del repositorio. |
| DONE-02 | Add | Licencia GNU GPL v3 (`LICENSE`). Mostrarla en la UI queda para F5. |
| DONE-03 | Remove | Vipre / Viper retirado en v2.1.0. |
| DONE-04 | Change | Migración Autoplay Media Studio → Visual Studio / C# (v3, julio 2022). |
| DONE-05 | Change | Documentación histórica en `docs/archive/`. |
| DONE-06 | Change | Visión 4.0: toolkit, modos Hogar/Técnico, no-antivirus. |
| DONE-07 | Add | Roadmap 4.0 / 4.1 / 4.2. |
| DONE-08 | Fix | Higiene v3: `.gitignore`, sin `bin/`/`obj/`/`.vs` en git; código muerto Form4/Form5 y handlers vacíos. |
| DONE-09 | Change | F1 catálogo vigente + F2 arquitectura/contratos cerrados (2026-08-17). |
| DONE-10 | Add | F3-01: solución `src/BAIOS.sln` (.NET 8 + WPF). |
| DONE-11 | Add | F3-02…F3-11: MVP 4.0 usable (modos, dashboard, módulos nativos, reporte, núcleo de herramientas). |
| DONE-12 | Add | F4-01: manifiesto schema 1 y repositorio `Tools/<id>/`. |
| DONE-13 | Add | F4-02: descarga https, sha256, versionado y reemplazo sin recompilar. |
| DONE-14 | Add | F4-03…F4-05: logs de ejecución, fichas F1-03 en manifiesto, HijackThis con alerta de falsos positivos. |
| DONE-15 | Add | F5-01…F5-06 y F5-08: empaquetado, Rescue, About/logs, manual, retiro de test2, identidad visual. |
| DONE-16 | Remove | F5-07: no habrá edición empresarial. Un motor, dos modos. |

## DOC — Definición (esta fase)

| ID | Tipo | Ítem | Estado |
| --- | --- | --- | --- |
| DOC-01 | Change | Reescribir [visión](vision.md). | hecho |
| DOC-02 | Add | [Roadmap](roadmap.md) 4.0 / 4.1 / 4.2. | hecho |
| DOC-03 | Change | Este backlog (DOC/F1–F5); archivar P0–P3. | hecho |
| DOC-04 | Change | Vigencia en [catálogo](catalogo.md) (primera pasada). | hecho |
| DOC-05 | Add | Esqueleto [arquitectura](arquitectura.md). | hecho |
| DOC-06 | Add | Esqueleto [diseño funcional](diseno-funcional.md). | hecho |
| DOC-07 | Add | Esqueleto [actualización](actualizacion.md). | hecho |
| DOC-08 | Add | Esqueleto [reportes](reportes.md). | hecho |
| DOC-09 | Add | Esqueleto [seguridad](seguridad.md) y alinear `SECURITY.md`. | hecho |
| DOC-10 | Change | Alinear [README](../README.md) con la visión 4.0. | hecho |

F1, F2 y F3 cerrados. Validar binarios de terceros en un PC es OPS-04, no reabre las tablas del catálogo.

## F1 — Rescate

Inventario ya extraído en el catálogo. Aquí se **confirma vigencia**, no se re-parsean los HTML.

| ID | Tipo | Ítem | Estado |
| --- | --- | --- | --- |
| F1-01 | Change | Marcar cada ficha: `vigente` / `abandonada` / `alternativa` / `fuera_de_4.0`. | hecho |
| F1-02 | Add | Núcleo 4.0: AdwCleaner, Microsoft Safety Scanner, Autoruns. | hecho |
| F1-03 | Change | Lista 4.1+: HijackThis, KVRT, HitmanPro, ZHPCleaner, EEK, Spybot/MB (opcional instalador). Norton PE fuera (EOL). | hecho |
| F1-04 | Remove | Fuera: Stinger/Trellix, K7, NoBot, IObit, ClamWin, Zemana, Immunet, Adaware, CureIt, Vipre, NPE. | hecho |
| F1-05 | Change | LiveCD históricos → 4.2+ (solo URLs oficiales). | hecho |
| F1-06 | Change | Online: enlaces Técnico (VirusTotal, ESET); no Form4/Form5. | hecho |
| F1-07 | Change | Herencia v1–v3: launcher, acuerdo, avisos. | hecho |
| F1-08 | Fix | URLs oficiales (MSERT, KVRT, ZHP); quitar McAfee en fichas ajenas. | hecho |

## F2 — Definición de 4.0 (cerrada)

| ID | Tipo | Ítem | Estado |
| --- | --- | --- | --- |
| F2-01 | Add | Stack: .NET 8 self-contained + WPF. WinUI 3 solo si 4.2 lo exige. | hecho |
| F2-02 | Add | Solución `src/BAIOS.*` (App, Core, Security, Diagnostics, Maintenance, Networking, Tools, Reports, Config). | hecho |
| F2-03 | Add | Contrato `manifest.json` schema 1. | hecho |
| F2-04 | Add | Permisos / UAC por acción. | hecho |
| F2-05 | Add | Pantallas Hogar vs Técnico. | hecho |
| F2-06 | Add | Esquema de reporte (Finding + secciones). | hecho |

## F3 — MVP 4.0 (cerrada, 2026-08-17)

| ID | Tipo | Ítem | Estado |
| --- | --- | --- | --- |
| F3-01 | Add | Proyecto nuevo (no `test2`): [`src/BAIOS.sln`](../src/BAIOS.sln). | hecho |
| F3-02 | Add | Selector de modo Hogar / Técnico (o default Técnico si se detecta perfil USB). | hecho |
| F3-03 | Add | Inicio: dashboard de estado del equipo. | hecho |
| F3-04 | Add | Seguridad nativa: Defender (estado, rápido, completo; offline si aplica). | hecho |
| F3-05 | Add | Diagnóstico nativo: hardware, disco/SMART, servicios, procesos, inicio, drivers. | hecho |
| F3-06 | Add | Mantenimiento nativo con confirmación: temporales, cachés, papelera; DISM; SFC; CHKDSK advertido. | hecho |
| F3-07 | Add | Red: IP, DNS, gateway, ping, tracert, flush DNS, DHCP, conectividad. | hecho |
| F3-08 | Add | Reporte de sesión texto/HTML. | hecho |
| F3-09 | Add | Lanzar AdwCleaner, Microsoft Safety Scanner, Autoruns (sin auto-update). | hecho |
| F3-10 | Add | Flujo «diagnóstico completo» en modo Técnico. | hecho |
| F3-11 | Add | Acuerdo de uso, aviso de no borrar a ciegas, alerta de falsos positivos (herencia útil de v3). | hecho |

Probar binarios de terceros en un PC real es OPS-04, no reabre F3-09.

## F4 — Motor de herramientas (4.1; cerrada)

| ID | Tipo | Ítem | Estado |
| --- | --- | --- | --- |
| F4-01 | Add | Manifiesto JSON y repositorio local de herramientas. | hecho |
| F4-02 | Add | Descarga, sha256, versionado; reemplazo sin recompilar. | hecho |
| F4-03 | Add | Logs de ejecución por herramienta. | hecho |
| F4-04 | Upgrade | Incorporar fichas F1-03 vigentes. | hecho |
| F4-05 | Add | HijackThis (F1 lo confirma vigente), con alerta de falsos positivos. | hecho |

## F5 — Producto 4.0 / 4.2 (cerrada)

| ID | Tipo | Ítem | Estado |
| --- | --- | --- | --- |
| F5-01 | Add | Instalador y actualizador del motor. | hecho |
| F5-02 | Add | Perfil USB / Technician Edition (empaquetado, mismo código). | hecho |
| F5-03 | Add | Rescue: catálogo de enlaces LiveCD oficiales (no ISO embebidas). | hecho |
| F5-04 | Add | UX, logs de aplicación, About amplio con GPL-3.0 y enlace a `LICENSE`. El diálogo corto al salir (Andago, terceros, repo, foro) ya está. | hecho |
| F5-05 | Add | Documentación de usuario (sustituye el PDF de instalación perdido). | hecho |
| F5-06 | Change | Aplanar `BAIOS/BAIOS/` y retirar el prototipo `test2` cuando 4.0 compile. | hecho |
| F5-07 | Remove | Edición empresarial. | archivado (no habrá) |
| F5-08 | Add | Identidad visual: logo BAIOS (velosergio) en bienvenida/ventana, icono del `.exe`. No embeber logos de AdwCleaner/MSERT/Autoruns (marca ajena); en 4.1 el manifiesto podrá apuntar a iconos locales opcionales. | hecho |

## OPS — Operación (pendiente)

No es una versión nueva. El motor 4.0 / 4.1 / 4.2 ya está en código. Esto es sembrar, publicar y validar.

Los binarios **no van dentro de `BAIOS.exe`**. Van en `Tools/<id>/` junto al exe. Para que `scripts/publish.ps1` los copie a `dist/BAIOS/` y `dist/BAIOS.Technician/`: colocarlos en [`src/BAIOS.App/Tools/<id>/`](../src/BAIOS.App/Tools/). Git no versiona esos `.exe` (`.gitignore`). Alternativa: copiarlos después del publish a `dist/BAIOS/Tools/<id>/`.

| ID | Tipo | Ítem | Estado |
| --- | --- | --- | --- |
| OPS-01 | Add | Sembrar binarios oficiales en `src/BAIOS.App/Tools/<id>/` (hoy solo `.gitkeep`). Nombres: `adwcleaner.exe`, `msert.exe`, `Autoruns64.exe`, `HiJackThis.exe`, `KVRT.exe`, `HitmanPro_x64.exe`, `ZHPCleaner.exe`, `EmsisoftEmergencyKit.exe`, `SpybotSetup.exe`, `MBSetup.exe`. | pendiente |
| OPS-02 | Add | Rellenar `sha256` de 64 hex en `manifest.json`. Sin hash, **Instalar / Actualizar** no descarga; se puede seguir colocando el archivo a mano (OPS-01). | pendiente |
| OPS-03 | Add | Primera publicación 4.0: dejar de ser `4.0.0-dev`, pasar [Unreleased](changelog.md) a versión fechada. | pendiente |
| OPS-04 | Add | Probar cada ficha del manifiesto en un PC real (lanzar, UAC, log). No reabre F3-09 ni F4-04. | pendiente |
| OPS-05 | Change | [Inventario](inventario.md): ~50 ítems `pendiente` en la carpeta local (~10 GB). No es catálogo ni producto 4.x. | pendiente |

## WEB — Sitio público

No es una versión del motor. Sustituye el Google Sites y el P2-05 (blog Blogger, archivado). El HTML estático vive en [`website/`](../website/); el tema Adminox en `website/Adminox_v2.0.0/` es solo referencia (no se publica el panel Admin).

| ID | Tipo | Ítem | Estado |
| --- | --- | --- | --- |
| WEB-01 | Add | Landing estática en `website/` (estructura del Google Sites, copy BAIOS 4, tema Adminox Landing). | hecho |
| WEB-02 | Change | Apuntar [`AppLinks.cs`](../src/BAIOS.App/Services/AppLinks.cs), [`WelcomeView.xaml`](../src/BAIOS.App/Views/WelcomeView.xaml), [`AgreementView.xaml`](../src/BAIOS.App/Views/AgreementView.xaml) y [`ReportsModule.cs`](../src/BAIOS.Reports/ReportsModule.cs) al URL público. | pendiente |
| WEB-03 | Add | Publicar (p. ej. GitHub Pages) y retirar Google Sites. | pendiente |

## Archivado — BAIOS 3 / Lite

No se reabre. El producto 4.0 no restaura Form4/Form5 ni pulirá el launcher Lite.

### Hecho de Lite (contexto)

DONE-01 a DONE-05 siguen valiendo como historia del repo.

### P0 — Bloqueantes Lite

| ID | Tipo | Ítem | Estado |
| --- | --- | --- | --- |
| P0-01 | Fix | Restaurar Online y Portables (`Form4` / `Form5`). | archivado |
| P0-02 | Add | Confirmar salida. | archivado |
| P0-03 | Add | Alerta: suspender antivirus residente. | archivado (espíritu en F3-11) |
| P0-04 | Add | Alerta HijackThis: falsos positivos. | archivado (espíritu en F4-05) |
| P0-05 | Add | No eliminar archivos sin saber qué son. | archivado (espíritu en F3-11) |
| P0-06 | Add | Etiqueta ejecutable / portable / online. | archivado |

### P1 — UX Lite

| ID | Ítem | Estado |
| --- | --- | --- |
| P1-01 … P1-11 | Pulido de bienvenida, acuerdo, Ejecutables, Online, Portables, About, logos, GPL en UI. | archivado |

P1-10 (mostrar GPL en la app) se retoma como F5-04.

### P2 — Catálogo y distribución Lite

| ID | Ítem | Estado |
| --- | --- | --- |
| P2-01 | Actualizar fichas ~2020–2022. | archivado (sustituido por F1-01) |
| P2-02 | Ficha Dr. Web CureIt!. | archivado (F1-04: probablemente fuera) |
| P2-03 | Pruebas y manuales por portable. | archivado (F1 / F4) |
| P2-04 | Personalizar `.exe` embebido. | archivado (incompatible con manifiesto) |
| P2-05 | Mini blog Blogger. | archivado (sustituido por WEB) |
| P2-06 | Edición Full. | archivado (manifiesto 4.1) |
| P2-07 | Edición Rescue. | archivado (F5-03) |
| P2-08 | Manual de instalación PDF ausente. | archivado (F5-05) |
| P2-09 | Restos Vipre / `00.antivirus.txt`. | archivado |
| P2-10 | URLs McAfee copiadas por error. | archivado (F1-08) |

### P3 — Higiene Lite

| ID | Ítem | Estado |
| --- | --- | --- |
| P3-01 | `.gitignore`, no versionar `bin/`/`obj/`/`.vs`. | hecho (repo actual; F3-01 sigue siendo el proyecto 4.0) |
| P3-02 | Aplanar `BAIOS/BAIOS/`, renombrar `test2`. | archivado (F5-06) |
| P3-03 | Alinear versión UI 6 Alpha / AssemblyInfo / tags. | archivado |
| P3-04 | Quitar binarios antivirus duplicados en `Resources/`. | archivado |
| P3-05 | Entorno: el clone no tiene que vivir en `htdocs`. | archivado |
| P3-06 | Handlers huérfanos en `3.menu.cs`. | hecho |
| P3-07 | Migrar créditos embebidos a docs. | archivado (parcial; catálogo cubre el contenido) |
