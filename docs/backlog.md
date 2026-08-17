# Backlog

Trabajo de **BAIOS 4**. Fuente de producto: [visión](vision.md). Versiones: [roadmap](roadmap.md).

Leyenda de tipo (Keep a Changelog): **Add**, **Change**, **Fix**, **Upgrade**, **Remove**.

Estados: `pendiente` · `parcial` · `hecho` · `fuera_de_alcance_ahora` · `archivado`.

Orden: **DOC + F1 + F2 ahora**. F3 en adelante no se abre hasta que visión, arquitectura y catálogo vigente estén firmes. El prototipo `test2` no recibe features 4.0; sí admite higiene (compilar / git).

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

Siguiente trabajo de definición: F1 (confirmar vigencia en vivo) y F2 (cerrar stack y contratos). No abrir F3.

## F1 — Rescate

Inventario ya extraído en el catálogo. Aquí se **confirma vigencia**, no se re-parsean los HTML.

| ID | Tipo | Ítem | Estado |
| --- | --- | --- | --- |
| F1-01 | Change | Marcar cada ficha: `vigente` / `abandonada` / `alternativa` / `fuera_de_4.0`. Primera pasada hecha en el catálogo; falta confirmar en vivo. | parcial |
| F1-02 | Add | Núcleo 4.0 de lanzamiento: AdwCleaner, Microsoft Safety Scanner, Autoruns (ficha nueva). | parcial |
| F1-03 | Change | Lista 4.1+: HijackThis, KVRT, Norton PE, HitmanPro, ZHPCleaner, Emsisoft Emergency Kit, Spybot — licencia y si son portable de verdad. | pendiente |
| F1-04 | Remove | Confirmar fuera: Stinger/Trellix, K7, NoBot, IObit, ClamWin, Zemana, Immunet, Adaware, CureIt pendiente. | pendiente |
| F1-05 | Change | LiveCD históricos → candidatos 4.2+ (solo URLs oficiales). | pendiente |
| F1-06 | Change | Online (VirusTotal / ESET): enlaces en modo Técnico, no pantallas Form4/Form5. | pendiente |
| F1-07 | Change | Documentar qué aportaba v1–v3 de verdad: launcher, acuerdo de uso, avisos de falsos positivos. | pendiente |
| F1-08 | Fix | URLs copiadas por error en fichas históricas (Safety Scanner / ZHP / Kaspersky → McAfee). Antes P2-10. | pendiente |

## F2 — Definición de 4.0 (antes de código)

| ID | Tipo | Ítem | Estado |
| --- | --- | --- | --- |
| F2-01 | Add | Decisión de stack: recomendación .NET 8 self-contained + WPF; WinUI 3 como alternativa. Cerrar en [arquitectura](arquitectura.md). | pendiente |
| F2-02 | Add | Estructura de solución: `BAIOS.exe` + Core (Security, Diagnostics, Maintenance, Networking) + Modules + Tools + Reports + Config. | pendiente |
| F2-03 | Add | Contrato del manifiesto de herramientas (campos, hashes, admin, interpretación de salida). | pendiente |
| F2-04 | Add | Modelo de permisos: qué corre elevado, UAC, qué no. | pendiente |
| F2-05 | Add | Pantallas y comportamiento por módulo y por modo (Hogar vs Técnico). | pendiente |
| F2-06 | Add | Esquema del reporte de sesión (secciones, severidades, recomendaciones). | pendiente |

## F3 — MVP 4.0 (código; no abrir aún)

| ID | Tipo | Ítem | Estado |
| --- | --- | --- | --- |
| F3-01 | Add | Proyecto nuevo (no `test2`): solución BAIOS. El `.gitignore` del repo actual ya está (P3-01); F3-01 es la solución 4.0, no reabrir Lite. | pendiente |
| F3-02 | Add | Selector de modo Hogar / Técnico (o default Técnico si se detecta perfil USB). | pendiente |
| F3-03 | Add | Inicio: dashboard de estado del equipo. | pendiente |
| F3-04 | Add | Seguridad nativa: Defender (estado, rápido, completo; offline si aplica). | pendiente |
| F3-05 | Add | Diagnóstico nativo: hardware, disco/SMART, servicios, procesos, inicio, drivers. | pendiente |
| F3-06 | Add | Mantenimiento nativo con confirmación: temporales, cachés, papelera; DISM; SFC; CHKDSK advertido. | pendiente |
| F3-07 | Add | Red: IP, DNS, gateway, ping, tracert, flush DNS, DHCP, conectividad. | pendiente |
| F3-08 | Add | Reporte de sesión texto/HTML. | pendiente |
| F3-09 | Add | Lanzar AdwCleaner, Microsoft Safety Scanner, Autoruns (sin auto-update). | pendiente |
| F3-10 | Add | Flujo «diagnóstico completo» en modo Técnico. | pendiente |
| F3-11 | Add | Acuerdo de uso, aviso de no borrar a ciegas, alerta de falsos positivos (herencia útil de v3). | pendiente |

## F4 — Motor de herramientas (4.1)

| ID | Tipo | Ítem | Estado |
| --- | --- | --- | --- |
| F4-01 | Add | Manifiesto JSON y repositorio local de herramientas. | pendiente |
| F4-02 | Add | Descarga, sha256, versionado; reemplazo sin recompilar. | pendiente |
| F4-03 | Add | Logs de ejecución por herramienta. | pendiente |
| F4-04 | Upgrade | Incorporar fichas F1-03 vigentes. | pendiente |
| F4-05 | Add | HijackThis si F1 lo confirma, con alerta de falsos positivos. | pendiente |

## F5 — Producto 4.0 / 4.2

| ID | Tipo | Ítem | Estado |
| --- | --- | --- | --- |
| F5-01 | Add | Instalador y actualizador del motor. | pendiente |
| F5-02 | Add | Perfil USB / Technician Edition (empaquetado, mismo código). | pendiente |
| F5-03 | Add | Rescue: catálogo de enlaces LiveCD oficiales (no ISO embebidas). | pendiente |
| F5-04 | Add | UX, logs de aplicación, About con GPL-3.0 y enlace a `LICENSE`. | pendiente |
| F5-05 | Add | Documentación de usuario (sustituye el PDF de instalación perdido). | pendiente |
| F5-06 | Change | Aplanar `BAIOS/BAIOS/` y retirar el prototipo `test2` cuando 4.0 compile. | pendiente |
| F5-07 | Add | Edición empresarial. | fuera_de_alcance_ahora |

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
| P2-05 | Mini blog Blogger. | archivado |
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
