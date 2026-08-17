# Catálogo de software

Herramientas que BAIOS agrupa, ha agrupado o lanzará. BAIOS **no las reemplaza**: las declara, las lanza y (desde 4.1) las actualiza por manifiesto. Visión: [vision.md](vision.md). Vigencia a confirmar en [F1](backlog.md).

Fuente histórica: [`archive/Detalles de Software.html`](archive/Detalles%20de%20Software.html) (~2022) y [`archive/Creditos.md`](archive/Creditos.md).

**Importante:** varias URLs del HTML antiguo están copiadas por error (páginas de McAfee en fichas de Microsoft, ZHP o Kaspersky). No se reproducen como oficiales. Corregir residuales: [F1-08](backlog.md).

Vipre / Viper **no se incluye** (retirado en v2.1.0).

Leyenda de ficha: `completa` · `incompleta` · `urls_sospechosas` · `pendiente` · `referenciada`.

Leyenda de vigencia (BAIOS 4): `vigente` · `abandonada` · `alternativa` · `fuera_de_4.0`. Primera pasada de F1; no sustituye probar cada binario.

## Resumen de vigencia

| Herramienta | Vigencia | Destino |
| --- | --- | --- |
| AdwCleaner | vigente | Núcleo 4.0 (lanzar) |
| Microsoft Safety Scanner | vigente | Núcleo 4.0 (lanzar) |
| Autoruns (Sysinternals) | vigente | Núcleo 4.0 (nuevo vs v3) |
| HijackThis | vigente | 4.1+; alerta de falsos positivos |
| Kaspersky KVRT | vigente | 4.1+ |
| Norton Power Eraser | vigente | 4.1+ |
| HitmanPro | vigente (prueba) | 4.1+ |
| ZHPCleaner | vigente | 4.1+ |
| Emsisoft Emergency Kit | vigente | 4.1+ |
| Spybot Search & Destroy | vigente | 4.1+ |
| Malwarebytes (instalador) | vigente | 4.1+ opcional; no portable típico |
| McAfee Stinger / Trellix | alternativa | Fuera salvo F1-04 contrario |
| K7 Disinfector | alternativa | Fuera salvo F1-04 contrario |
| NoBot | abandonada | Fuera |
| IObit Malware Fighter | fuera_de_4.0 | Fuera |
| ClamWin | abandonada | Fuera |
| Zemana AntiMalware | abandonada | Fuera |
| Immunet | abandonada | Fuera |
| Adaware | fuera_de_4.0 | Fuera |
| Dr. Web CureIt! | alternativa | Fuera (ficha nunca completada) |
| Vipre | fuera_de_4.0 | Retirado v2.1.0 |
| Escáneres online | vigente | Enlaces en modo Técnico (no Form4/Form5) |
| LiveCD / Rescue | vigente (enlaces) | 4.2+; nunca ISO embebidas |

Microsoft Defender no es una ficha de portable: es módulo nativo del motor ([visión](vision.md)).

---

## Núcleo 4.0 (lanzar, no actualizar)

### Autoruns (Sysinternals)

Nuevo respecto a BAIOS 3. No estaba en el TreeView ni en créditos.

| Campo | Valor |
| --- | --- |
| Categoría | Portable (Sysinternals) |
| Vigencia | vigente |
| Destino | Núcleo 4.0 |
| Estado de ficha | pendiente (alta nueva) |
| Fabricante | Microsoft |
| Web | <https://learn.microsoft.com/sysinternals/downloads/autoruns> |
| Nota | Inventario de inicio; BAIOS no reimplementa Autoruns. |

### AdwCleaner

| Campo | Valor |
| --- | --- |
| Categoría | Portable |
| Vigencia | vigente |
| Destino | Núcleo 4.0 |
| Estado de ficha | completa |
| Act. BAIOS | 2022-08-26 |
| Versión | 8.3.2.0 |
| Fecha del software | 2021-03-23 |
| Fabricante | Malwarebytes |
| Web / descarga | <https://es.malwarebytes.com/adwcleaner/> |
| Licencia | [EULA AdwCleaner](https://www.malwarebytes.com/adwcleaner/eula?x-source=adw&ADDITIONAL_x-source=adw) · [EULA free](https://es.malwarebytes.com/eula/) |
| Manual | [vídeo](https://www.youtube.com/watch?v=0mf_8X0z0OE) · [guía](https://www.infospyware.com/antispyware/adwcleaner/) |

### Microsoft Safety Scanner

| Campo | Valor |
| --- | --- |
| Categoría | Portable |
| Vigencia | vigente |
| Destino | Núcleo 4.0 |
| Estado de ficha | urls_sospechosas (el HTML ponía fabricante McAfee) — [F1-08](backlog.md) |
| Act. BAIOS | 2022-04-16 |
| Versión | 1.373.1011.0 |
| Fecha del software | 2022-04-11 |
| Fabricante | Microsoft |
| Web / descarga | [Safety Scanner](https://learn.microsoft.com/windows/security/threat-protection/intelligence/safety-scanner-download) |
| Privacidad | [declaración Microsoft](https://privacy.microsoft.com/en-us/privacystatement) |

---

## Portables del menú v3 (TreeView)

Lista histórica de `3.menu.Designer.cs`. El código está congelado.

### HijackThis

| Campo | Valor |
| --- | --- |
| Categoría | Portable |
| Vigencia | vigente |
| Destino | 4.1+ ([F4-05](backlog.md)) |
| Estado de ficha | completa |
| Act. BAIOS | 2022-04-16 |
| Versión | 2.10.0.20 |
| Fecha del software | 2022-08-01 |
| Fabricante | Dragokas |
| Web | <https://github.com/dragokas/hijackthis> |
| Descarga | <https://github.com/dragokas/hijackthis/raw/devel/binary/HiJackThis.exe> |
| Licencia | GPLv2 |
| Manual | [español (desactualizado)](https://www.bleepingcomputer.com/tutorials/como-usar-hijackthis/) · [inglés](https://dragokas.com/tools/help/hjt_tutorial.html) |
| Nota de uso | Puede generar **falsos positivos**. |

### McAfee Stinger

| Campo | Valor |
| --- | --- |
| Categoría | Portable |
| Vigencia | alternativa |
| Destino | Fuera salvo [F1-04](backlog.md) |
| Estado de ficha | completa |
| Act. BAIOS | 2022-04-16 |
| Versión | 12.2.0.464 |
| Fecha del software | 2022-08-24 |
| Fabricante | McAfee (hoy Trellix) |
| Web | [Stinger (histórico)](https://www.mcafee.com/enterprise/en-us/downloads/free-tools/stinger.html) |
| Descarga (histórica) | `stinger32.exe` en el Download Center de McAfee |
| Licencia | Royalty-free (política McAfee de la época) |
| Manual | How to use Stinger (inglés, URL histórica) |

### NoBot

| Campo | Valor |
| --- | --- |
| Categoría | Portable |
| Vigencia | abandonada |
| Destino | Fuera |
| Estado de ficha | incompleta (EULA sin enlace) |
| Act. BAIOS | 2022-04-16 |
| Versión | 1.0.6.0 |
| Fecha del software | 2022-01-01 |
| Fabricante | TazzyOpz |
| Web | <https://nobotsecurity.com/> |
| Descarga (histórica) | `https://nobotsecurity.com/Software/NoBot.exe` |
| Licencia | Free (sin URL de EULA) |
| Manual | [RedesZone](https://www.redeszone.net/2018/01/06/nobot-antivirus-portable/) |

### HitmanPro

| Campo | Valor |
| --- | --- |
| Categoría | Portable / ejecutable |
| Vigencia | vigente (prueba ~30 días) |
| Destino | 4.1+ |
| Estado de ficha | completa |
| Act. BAIOS | 2022-04-16 |
| Versión | 3.8.30 |
| Fecha del software | 2020-03-31 |
| Fabricante | Sophos |
| Web | <https://www.hitmanpro.com/en-us.aspx> |
| Descarga | [HitmanPro_x64.exe](https://download.sophos.com/endpoint/clients/HitmanPro_x64.exe) · [HitmanPro.exe](https://download.sophos.com/endpoint/clients/HitmanPro.exe) |
| Licencia | Prueba ~30 días; [términos Sophos](https://www.sophos.com/en-us/legal/sophos-end-user-terms-of-use) |
| Manual | [ForoSpyware](https://forospyware.com/t/manual-de-hitmanpro/1564) |

### Kaspersky Virus Removal Tool (KVRT / VRT)

| Campo | Valor |
| --- | --- |
| Categoría | Portable |
| Vigencia | vigente |
| Destino | 4.1+ |
| Estado de ficha | urls_sospechosas (el HTML copiaba EULA/manual de McAfee) |
| Act. BAIOS | 2020-03-31 |
| Versión | 20.0.10.0 |
| Fecha del software | 2022-08-25 |
| Fabricante | Kaspersky |
| Web | [KVRT](https://www.kaspersky.com/downloads/thank-you/free-virus-removal-tool) |
| Soporte | [KVRT 2020](https://support.kaspersky.com/kvrt2020) |
| Licencia / manual | Pendiente de URLs propias de Kaspersky ([F1-08](backlog.md)) |

### Norton Power Eraser

| Campo | Valor |
| --- | --- |
| Categoría | Portable |
| Vigencia | vigente |
| Destino | 4.1+ |
| Estado de ficha | urls_sospechosas (el HTML copiaba el manual de Stinger) |
| Act. BAIOS | 2020-03-31 |
| Versión | 6.0.1.2095 |
| Fecha del software | 2021-05-06 |
| Fabricante | Norton |
| Web | <https://us.norton.com/support/tools/npe.html> |
| Soporte | [solución v129164633](https://support.norton.com/sp/es/es/home/current/solutions/v129164633) |
| Licencia | [EULA PDF histórico](https://buy-download.norton.com/downloads/public/lam_spanish/tools/eula/cps_tool_4_0/End_User_License_Agreement.pdf) |
| Manual | Usar el soporte Norton, no el enlace de McAfee del HTML |

### K7 Disinfector

| Campo | Valor |
| --- | --- |
| Categoría | Portable |
| Vigencia | alternativa |
| Destino | Fuera salvo [F1-04](backlog.md) |
| Estado de ficha | incompleta |
| Act. BAIOS | (no consta en el HTML de detalles) |
| Versión | 12.0.0.240 (créditos) |
| Fabricante | K7 Computing |
| Web / EULA | <https://www.k7computing.com/us/eula> |
| Nota | Presente en el menú v3; ficha HTML ausente |

### ZHPCleaner

| Campo | Valor |
| --- | --- |
| Categoría | Portable |
| Vigencia | vigente |
| Destino | 4.1+ |
| Estado de ficha | urls_sospechosas (el HTML ponía web/EULA de McAfee) |
| Act. BAIOS | 2022-04-16 |
| Versión | V2022.8.25.66 |
| Fecha del software | 2022-04-22 |
| Fabricante | Nicolas Coolman |
| Descarga | <https://nicolascoolman.com/es/download/zhpcleaner/> |
| Web | Usar el sitio de Coolman, no McAfee |

---

## Otras herramientas (créditos / Full histórico)

No estaban en el TreeView de Lite. Destino según vigencia, no según «edición Full».

| Nombre | Versión (créditos) | Fecha | Fabricante / web | Vigencia | Destino |
| --- | --- | --- | --- | --- | --- |
| IObit Malware Fighter | 8.8.0.850 | — | [EULA IObit](https://www.iobit.com/en/eula-bd.php) | fuera_de_4.0 | Fuera |
| Malwarebytes | 4.1.0.56 | 2020-03-31 | <https://es.malwarebytes.com/> | vigente | 4.1+ opcional (instalador) |
| ClamWin | — | 2020-03-31 | <http://www.clamwin.com/content/view/18/46/> | abandonada | Fuera |
| Emsisoft Emergency Kit | 2020.3.2.10048 | 2020-03-31 | <https://www.emsisoft.com/en/home/emergencykit/> | vigente | 4.1+ |
| Spybot Search & Destroy | 2.7.64.0 | 2020-03-31 | <https://www.safer-networking.org/download/> | vigente | 4.1+ |
| Zemana AntiMalware | 3.1.495 | 2020-03-31 | <https://www.zemana.com/antimalware> | abandonada | Fuera |
| Immunet | — | — | <https://www.immunet.com/index> | abandonada | Fuera |
| Adaware | — | — | <https://www.adaware.com/free-antivirus-download> | fuera_de_4.0 | Fuera |
| Dr. Web CureIt! | — | — | — | alternativa | Fuera (ficha nunca hecha) |

---

## Online (histórico v1.3.0)

Las pantallas `Form4` / `Form5` no se restauran. En 4.0/4.1 son **enlaces en modo Técnico** ([F1-06](backlog.md)).

| Nombre | Vigencia | Destino |
| --- | --- | --- |
| ESET Online Scanner | vigente | Enlace Técnico |
| Panda Cloud Cleaner / online | alternativa | Revisar si el producto sigue existiendo |
| VirusTotal | vigente | Enlace Técnico |
| Jotti | vigente | Enlace Técnico (opcional) |
| Hybrid Analysis | vigente | Enlace Técnico (opcional) |

---

## LiveCD / Rescue (4.2+)

Solo URLs oficiales; nunca ISO embebidas ([F5-03](backlog.md)). Listado histórico, fichas incompletas:

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

---

## Galería Ejecutables (v3, congelada)

`Form3` muestra logos (AdwCleaner, HijackThis, HitmanPro, Norton, Microsoft Safety Scanner, K7, Zemana/Zone, Kaspersky, McAfee, Blinter). No hay lanzamiento real salvo un `pictureBox2_Click` incompleto. El pulido P1-05 está **archivado**; el lanzamiento vive en F3-09.
