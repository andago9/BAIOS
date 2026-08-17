# Catálogo de software

Herramientas que BAIOS declara, lanza o (desde 4.1) actualiza por manifiesto. BAIOS **no las reemplaza**. Visión: [vision.md](vision.md).

Inventario de disco (binarios locales): [inventario.md](inventario.md). No mezclar con este catálogo.

Fuente histórica: [`archive/Detalles de Software.html`](archive/Detalles%20de%20Software.html), [`archive/Creditos.md`](archive/Creditos.md) y notepads de créditos (~marzo 2020, AIO 2021-08-19 y AIO 2022-01-05), fusionados en [créditos](creditos.md). Si el notepad cita otra versión, se anota como **versión en créditos**; no sustituye la ficha. Las URLs de McAfee copiadas por error en fichas ajenas **no se usan**.

Vipre / Viper no se incluye (retirado en v2.1.0).

Leyenda de ficha: `completa` · `incompleta` · `pendiente`.

Leyenda de vigencia (cerrada en F1, 2026-08-17): `vigente` · `abandonada` · `alternativa` · `fuera_de_4.0`.

F1 cierra **decisiones de producto** (qué entra, qué sale, URLs oficiales). Probar cada binario en un PC es [F3-09](backlog.md) / [F4-04](backlog.md), no reabre estas tablas.

Microsoft Defender no es ficha de portable: es módulo nativo del motor.

## Resumen de vigencia

| Herramienta | Vigencia | Destino | Portable |
| --- | --- | --- | --- |
| AdwCleaner | vigente | Núcleo 4.0 (lanzar) | sí |
| Microsoft Safety Scanner | vigente | Núcleo 4.0 (lanzar); caduca ~10 días | sí |
| Autoruns (Sysinternals) | vigente | Núcleo 4.0 (nuevo vs v3) | sí |
| HijackThis | vigente | 4.1+; alerta de falsos positivos | sí |
| Kaspersky KVRT | vigente | 4.1+ | sí |
| HitmanPro | vigente (prueba ~30 días) | 4.1+ | sí (exe) |
| ZHPCleaner | vigente | 4.1+ | sí |
| Emsisoft Emergency Kit | vigente | 4.1+ | sí |
| Spybot Search & Destroy | vigente | 4.1+ opcional | no (instalador típico) |
| Malwarebytes | vigente | 4.1+ opcional | no (instalador) |
| Norton Power Eraser | abandonada | Fuera (EOL 2026-04-30) | — |
| McAfee Stinger / Trellix | fuera_de_4.0 | Fuera | — |
| K7 Disinfector | fuera_de_4.0 | Fuera | — |
| NoBot | abandonada | Fuera | — |
| IObit Malware Fighter | fuera_de_4.0 | Fuera | — |
| ClamWin | abandonada | Fuera | — |
| Zemana AntiMalware | abandonada | Fuera | — |
| Immunet | abandonada | Fuera | — |
| Adaware | fuera_de_4.0 | Fuera | — |
| Dr. Web CureIt! | fuera_de_4.0 | Fuera (ficha nunca hecha) | — |
| Vipre | fuera_de_4.0 | Retirado v2.1.0 | — |
| ESET Online Scanner | vigente | Enlace modo Técnico | n/a |
| VirusTotal | vigente | Enlace modo Técnico | n/a |
| Jotti / Hybrid Analysis | vigente | Enlace Técnico opcional | n/a |
| Panda Cloud Cleaner | alternativa | No núcleo; no enlace 4.0 | n/a |
| LiveCD / Rescue | vigente (solo URLs) | 4.2+; nunca ISO embebidas | n/a |

---

## Núcleo 4.0 (lanzar, no actualizar)

### Autoruns (Sysinternals)

Nuevo respecto a BAIOS 3.

| Campo | Valor |
| --- | --- |
| Categoría | Portable (Sysinternals) |
| Vigencia | vigente |
| Destino | Núcleo 4.0 |
| Estado de ficha | completa |
| Versión documentada | v14.3 (publicada 2026-06-17 en Learn) |
| Fabricante | Microsoft (Mark Russinovich) |
| Web | <https://learn.microsoft.com/sysinternals/downloads/autoruns> |
| Descarga | <https://download.sysinternals.com/files/Autoruns.zip> |
| Live | <https://live.sysinternals.com/autoruns.exe> |
| Licencia | [Sysinternals Software License](https://learn.microsoft.com/sysinternals/license) (EULA al primer uso) |
| Admin | Recomendado (`requiresAdmin`: true) para ver todo el inicio |
| Arquitectura | x64 (`Autoruns64.exe`); hay x86 y ARM64 en el zip |
| Interpretación | Inventario de autostart; BAIOS no parsea el volcado en 4.0. Alerta: no deshabilitar entradas de Microsoft sin saber qué son. |
| Nota | Portable. No reimplementar Autoruns. |

### AdwCleaner

| Campo | Valor |
| --- | --- |
| Categoría | Portable |
| Vigencia | vigente |
| Destino | Núcleo 4.0 |
| Estado de ficha | completa (versión embebida v3 desactualizada) |
| Act. BAIOS (histórico) | 2022-08-26 / 8.3.2.0 |
| Versiones en créditos | 8.3.1.0 (2021-11-18, AIO 2022-01-05); 8.3.0.0 (2021-06-29, AIO 2021-08-19); 8.3.0.0 (2020-03-03) |
| Fabricante | Malwarebytes |
| Web / descarga | <https://www.malwarebytes.com/adwcleaner> · <https://es.malwarebytes.com/adwcleaner/> |
| Licencia | [EULA AdwCleaner](https://www.malwarebytes.com/adwcleaner/eula) · [EULA free](https://www.malwarebytes.com/eula) · [avisos de terceros](https://www.malwarebytes.com/support/thirdpartynotices?x-source=adw&ADDITIONAL_x-source=adw) |
| Manual | Documentación del fabricante; guía histórica [InfoSpyware](https://www.infospyware.com/antispyware/adwcleaner/) · [vídeo](https://www.youtube.com/watch?v=0mf_8X0z0OE) |
| Requisitos citados (créditos) | Windows 7 / 8 / 10 (32/64-bit) |
| Admin | true |
| Interpretación | Cuarentena PUP/adware; no borrar a ciegas. |
| Nota | En 4.0 se lanza la copia local o la descargada por el usuario. Update automático: 4.1. |

### Microsoft Safety Scanner (MSERT)

| Campo | Valor |
| --- | --- |
| Categoría | Portable |
| Vigencia | vigente |
| Destino | Núcleo 4.0 |
| Estado de ficha | completa |
| Fabricante | Microsoft (no McAfee; el HTML histórico estaba mal) |
| Web | <https://learn.microsoft.com/defender-endpoint/safety-scanner-download> |
| Descarga x64 | <https://go.microsoft.com/fwlink/?LinkId=212732> |
| Descarga x86 | <https://go.microsoft.com/fwlink/?LinkId=212733> |
| Privacidad | [declaración Microsoft](https://privacy.microsoft.com/privacystatement) · [módulo seguridad (créditos)](https://privacy.microsoft.com/en-us/privacystatement#mainsecurityandsafetyfeaturesmodule) |
| Versión en créditos | 1.347.50.0 (historial repo, fabricante 2021-05-10, Act. BAIOS 2021-08-19); fecha 2020-03-31 sin número en el notepad anterior |
| Admin | true |
| Caducidad | El binario **expira ~10 días** tras la descarga. En 4.0 hay que avisar de bajar una copia fresca. |
| Interpretación | Segunda opinión; no sustituye Defender. |

---

## 4.1+ (manifiesto)

### HijackThis

| Campo | Valor |
| --- | --- |
| Categoría | Portable |
| Vigencia | vigente |
| Destino | 4.1+ ([F4-05](backlog.md)) |
| Fabricante | Dragokas |
| Versiones en créditos | 2.10.0.14 (2021-12-26, AIO 2022-01-05); 2.9.0.26 (historial repo, fabricante 2020-08-05, Act. BAIOS 2021-08-19); 2.9.0.18 (2020-03-03 y AIO 2021-08-19, fabricante vacío en ese notepad; se conserva Dragokas) |
| Web | <https://github.com/dragokas/hijackthis> |
| Descarga | Releases / binario del repo (rama devel histórica: `HiJackThis.exe`) |
| Licencia | GPLv2 (EULA vacía en el notepad de créditos) |
| Manual | [tutorial Dragokas](https://dragokas.com/tools/help/hjt_tutorial.html) · [español histórico](https://www.bleepingcomputer.com/tutorials/como-usar-hijackthis/) |
| Portable | sí |
| Admin | true |
| Nota | **Falsos positivos** frecuentes. No entra en el núcleo 4.0. |

### Kaspersky Virus Removal Tool (KVRT)

| Campo | Valor |
| --- | --- |
| Categoría | Portable |
| Vigencia | vigente |
| Destino | 4.1+ |
| Fabricante | Kaspersky |
| Versión en créditos | 20.0.8.0 (historial repo 2021-08-19); 15.0.22.0 (2020-03-31) |
| Web / descarga | <https://www.kaspersky.com/downloads/free-virus-removal-tool> |
| Ayuda | <https://support.kaspersky.com/help/kvrt/2024/en-us/> · [KVRT 2020 (créditos)](https://support.kaspersky.com/kvrt2020) |
| Licencia | EULA de Kaspersky en el propio sitio / instalador (no usar páginas de McAfee) |
| Portable | sí (no requiere instalación) |
| Admin | true (recomendado) |
| Nota | En EE. UU. las descargas de Kaspersky pueden no estar disponibles; BAIOS no debe hospedar el binario. |

### HitmanPro

| Campo | Valor |
| --- | --- |
| Categoría | Portable (exe) |
| Vigencia | vigente (prueba ~30 días) |
| Destino | 4.1+ |
| Fabricante | Sophos |
| Versión en créditos | 3.8.23 (historial repo, Act. BAIOS 2021-08-19); 3.8.16 (2020-03-31) |
| Web | <https://www.hitmanpro.com/> · [en-us histórico](https://www.hitmanpro.com/en-us.aspx) |
| Descarga (histórica Sophos) | [HitmanPro_x64.exe](https://download.sophos.com/endpoint/clients/HitmanPro_x64.exe) · [HitmanPro.exe](https://download.sophos.com/endpoint/clients/HitmanPro.exe) — verificar URL actual en 4.1. El AIO 2021-08-19 no traía estas URLs; se conservan las del notepad 2022. |
| Licencia | Prueba; [términos Sophos](https://www.sophos.com/en-us/legal/sophos-end-user-terms-of-use) |
| Portable | sí (ejecutable) |
| Admin | true |
| Nota | No es freeware ilimitado. Mostrar EULA de prueba. |

### ZHPCleaner

| Campo | Valor |
| --- | --- |
| Categoría | Portable |
| Vigencia | vigente |
| Destino | 4.1+ |
| Fabricante | Nicolas Coolman |
| Versión en créditos | 2021.8.24.323 (historial repo, 2021-08-24) |
| Web oficial | <https://nicolascoolman.eu/en/zhpcleaner-officiel/> · <https://www.nicolascoolman.com/> |
| Descarga | Solo el sitio del autor (nunca McAfee) |
| Licencia | Freeware del autor |
| Portable | sí |
| Admin | según ficha 4.1 |
| Nota | Defender/SmartScreen a menudo lo marca; aviso de falso positivo del propio AV, no de ZHP sobre el sistema. |

### Emsisoft Emergency Kit

| Campo | Valor |
| --- | --- |
| Categoría | Portable (kit) |
| Vigencia | vigente |
| Destino | 4.1+ |
| Fabricante | Emsisoft |
| Versión en créditos | 2020.3.2.10048 (2020-03-31) |
| Web | <https://www.emsisoft.com/en/emergency-kit/> · [URL créditos](https://www.emsisoft.com/en/home/emergencykit/) |
| Licencia | Gratis para uso privado; Pro para comercial |
| Portable | sí (sin instalación) |
| Admin | true |
| Nota | Dual-engine; no ejecutar junto a Emsisoft Anti-Malware instalado. |

### Spybot Search & Destroy

| Campo | Valor |
| --- | --- |
| Categoría | Instalador |
| Vigencia | vigente |
| Destino | 4.1+ opcional |
| Fabricante | Safer-Networking |
| Versión en créditos | 2.7.64.0 (2020-03-31) |
| Web | <https://www.safer-networking.org/download/> |
| Portable | **no** (instalador típico) |
| Nota | No forma parte del núcleo. Solo si el manifiesto 4.1 admite instaladores con consentimiento. |

### Malwarebytes (producto completo)

| Campo | Valor |
| --- | --- |
| Categoría | Instalador |
| Vigencia | vigente |
| Destino | 4.1+ opcional |
| Versión en créditos | 4.1.0.56 (2020-03-31) |
| Web | <https://www.malwarebytes.com/> · <https://es.malwarebytes.com/> |
| Portable | **no** |
| Nota | Distinto de AdwCleaner. No embeber. |

---

## Fuera (F1-04 cerrado)

La vigencia no cambia. Las versiones del notepad se fusionan aquí para no perderlas; el detalle de enlaces está en [créditos](creditos.md).

| Nombre | Motivo | Versión en créditos |
| --- | --- | --- |
| Norton Power Eraser | **EOL 2026-04-30** ([anuncio Norton](https://support.norton.com/sp/en/us/home/current/solutions/v20251217064155639)). Deja de funcionar; no hay reemplazo standalone. Enlaces históricos: [NPE](https://us.norton.com/support/tools/npe.html) · [EULA PDF](https://buy-download.norton.com/downloads/public/lam_spanish/tools/eula/cps_tool_4_0/End_User_License_Agreement.pdf) · [soporte](https://support.norton.com/sp/es/es/home/current/solutions/v129164633) | — (2020-03-31; el notepad duplicaba la entrada). Ficha HTML: 6.0.1.2095 |
| McAfee Stinger / Trellix | Fuera de 4.x. Defender + MSERT cubren segunda opinión Microsoft. URLs históricas: [Stinger](https://www.mcafee.com/enterprise/en-us/downloads/free-tools/stinger.html) · [privacidad](https://www.mcafee.com/enterprise/en-us/about/legal/privacy.html) · [manual](http://www.mcafee.com/us/downloads/free-tools/how-to-use-stinger.aspx) | 12.1.0.3432 (2020-03-03). Ficha HTML: 12.2.0.464 |
| K7 Disinfector | Fuera; ficha incompleta. [EULA](https://www.k7computing.com/us/eula) | 12.0.0.240 |
| NoBot | Sitio/producto abandonado a efectos de BAIOS. [web](https://nobotsecurity.com/) | 1.0.5.7 (2020-03-31). Ficha HTML: 1.0.6.0 |
| IObit Malware Fighter | Instalador comercial; fuera de 4.0/4.1 núcleo. [EULA](https://www.iobit.com/en/eula-bd.php) | 8.8.0.850 |
| ClamWin | Abandonado como línea del AIO. [web](http://www.clamwin.com/content/view/18/46/) | — (2020-03-31) |
| Zemana AntiMalware | Producto/marca inactiva para este catálogo. [web](https://www.zemana.com/antimalware) | 3.1.495 (2020-03-31) |
| Immunet | Abandonado. [web](https://www.immunet.com/index) | — |
| Adaware | Instalador; fuera. [descarga](https://www.adaware.com/free-antivirus-download) | — (el notepad lo numeraba 15, duplicado con CureIt) |
| Dr. Web CureIt! | Ficha nunca completada; no se retoma. | — (créditos: **pendiente**) |
| Vipre Rescue | Retirado v2.1.0. Prueba 30 días. Web histórica hallada en notepad-04. | 7.0.7.8 (2020-03-31) |

---

## Online (F1-06 cerrado)

No hay pantallas Form4/Form5. En 4.0/4.1: **enlaces en modo Técnico** (el navegador por defecto). Hogar: como mucho VirusTotal, o oculto.

| Nombre | Vigencia | URL | 4.0 |
| --- | --- | --- | --- |
| VirusTotal | vigente | <https://www.virustotal.com/> | Enlace Técnico |
| ESET Online Scanner | vigente | <https://www.eset.com/int/home/online-scanner/> | Enlace Técnico |
| Jotti | vigente | <https://virusscan.jotti.org/> | Opcional Técnico |
| Hybrid Analysis | vigente | <https://www.hybrid-analysis.com/> | Opcional Técnico |
| Panda Cloud Cleaner | alternativa | No enlace 4.0 | Revisar en 4.1 si hace falta |

---

## LiveCD / Rescue (F1-05 cerrado)

Destino **4.2+** ([F5-03](backlog.md)): catálogo de **enlaces oficiales**, nunca ISO dentro de BAIOS ni del repo.

Listado histórico a revalidar URL por URL en F5 (pueden haber cambiado de nombre o desaparecido):

- Avira Rescue System
- Comodo Rescue Disk
- Dr.Web LiveDisk
- ESET SysRescue Live
- Kaspersky Rescue Disk
- Norton Bootable Recovery Tool
- Panda Rescue / Cloud Cleaner bootable
- Sophos Bootable Anti-Virus
- Trend Micro Rescue Disk
- VBA32 Rescue

---

## Herencia de v1–v3 (F1-07)

Qué aportaba de verdad el producto viejo; qué se conserva en 4.0.

| Versión | Aportaba | Se conserva en 4.0 |
| --- | --- | --- |
| v1 (.BAT) | Encadenar herramientas | Orquestación, no el BAT |
| v2 (Autoplay) | Menú visual, logos, autorun | Identidad visual / flujo, no Autoplay |
| v3 (WinForms) | Bienvenida → acuerdo → menú; avisos; catálogo | Acuerdo, no borrar a ciegas, falsos positivos, lanzar terceros |

No se conserva: menú Portables/Ejecutables/Online, Form4/Form5, embebido masivo de `.exe` en `Resources/`, restaurar Lite.

---

## Galería Ejecutables (v3, congelada)

`Form3` muestra logos. El pulido Lite está archivado. El lanzamiento real vive en F3-09 con las tres del núcleo.
