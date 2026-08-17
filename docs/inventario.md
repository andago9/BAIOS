# Inventario de disco

Registro de trabajo: qué hay (o había) en la carpeta local de herramientas, no qué BAIOS declara como producto. El catálogo de fichas y vigencia es [catalogo.md](catalogo.md). Visión: [vision.md](vision.md). Créditos y autoría: [creditos.md](creditos.md).

Este archivo **nombra** programas y evidencias. No versiona binarios, ISOs ni los ~10 GB de la carpeta local. Tras clasificar una tanda, se borra en disco lo marcado `desuso` + `borrar`; el Markdown queda como constancia.

No mezclar con [backlog.md](backlog.md): el backlog sigue siendo F1–F5. Hashes, EULA y URLs oficiales viven en el catálogo cuando el programa se conserve.

Última tanda clasificada: **notepad-16** (iconos/Pexels, «eset si / panda si», EXIF/MalwareTracker/MetaDefender, manifiesto elhacker.net, Jotti/Dr.Web).

## Leyenda

Estado: `en_uso` · `pendiente` · `desuso` · `duplicado` (mismo programa, varias copias).

Acción: `conservar` · `revisar` · `borrar`.

Cruce con catálogo: `sí` · `no` · `ficha nueva`.

## Resumen

Conteos de filas clasificadas (no incluye bandeja ni el apartado «no son programas»).

| Estado | Ítems |
| --- | --- |
| en_uso | 12 |
| pendiente | 50 |
| desuso | 79 |
| duplicado | 39 |

## En uso

Lo que el prototipo lanza o que 4.0/4.1 conservará. Los online **no son binarios**: se conservan como enlace en modo Técnico, no como copia en los 10 GB.

| Nombre | Evidencia | Estado | Acción | Catálogo | Origen | Nota |
| --- | --- | --- | --- | --- | --- | --- |
| AdwCleaner | 8.5.1 (notepad-01, 2025); historial repo 8.3.0.0 fab. 29/06/2021 Act. BAIOS 19/08/2021 | en_uso | conservar | sí | notepad-01 | Núcleo 4.0. El catálogo aún cita 8.3.2.0 embebido v3. Actualizar ficha en F1. |
| Microsoft Safety Scanner | historial repo 1.347.50.0 fab. 10/05/2021 Act. BAIOS 19/08/2021 | en_uso | conservar | sí | notepad-01 | Núcleo 4.0. Caduca ~10 días. Portable, no escáner online. |
| HijackThis | historial repo 2.9.0.26 fab. 05/08/2020 Act. BAIOS 19/08/2021 | en_uso | conservar | sí | notepad-01 | 4.1+; alerta de falsos positivos. Créditos también 2.9.0.18 y 2.10.0.14. |
| HitmanPro | historial repo 3.8.23 Act. BAIOS 19/08/2021 (sin fecha fabricante) | en_uso | conservar | sí | notepad-01 | 4.1+; prueba ~30 días. Créditos también 3.8.16. |
| Kaspersky VRT | historial repo 20.0.8.0 fab. 19/08/2021 Act. BAIOS 19/08/2021 | en_uso | conservar | sí | notepad-01 | 4.1+. Créditos también 15.0.22.0. No confundir con el instalador ni Rescue CD. |
| ZHPCleaner | historial repo 2021.8.24.323 fab. 24/08/2021 | en_uso | conservar | sí | notepad-01 | 4.1+. Descarga solo nicolascoolman (.eu / .com). |
| Spybot Search & Destroy | ficha 11; 2.7.64.0; 31/3/2020; Baios 05/jan/2021 (notepad-04) | en_uso | conservar | sí | notepad-01 | 4.1+. Notepad-02 lo marca «no portable». La carpeta Beta es el mismo producto. |
| Emsisoft Emergency Kit | ficha 10; 2020.3.2.10048; remediation-kit (notepad-02/08) | en_uso | conservar | sí | notepad-01 | 4.1+. El hub «remediation-kit» es la misma familia, no otro producto. |
| ESET Online Scanner | 21. Eset Online; notepad-16: «eset si» | en_uso | conservar | sí | notepad-01 | Enlace Técnico (F1-06). Confirmado en notas. No guardar instalador ni ISO. |
| VirusTotal | 25. Virus Total; «virus total online scanner» (notepad-03) | en_uso | conservar | sí | notepad-01 | Enlace Técnico. Carga de archivos; no binario local. |
| Hybrid Analysis | 30. Hybrid Analysis | en_uso | conservar | sí | notepad-01 | Enlace Técnico opcional. No binario local. |
| Jotti | 31. VirusScan (jotti.org) | en_uso | conservar | sí | notepad-01 | Enlace Técnico opcional. No binario local. |

## Pendiente de revisión

Aparece en notas o en disco; vigencia o utilidad dudosa. No borrar de los 10 GB hasta cerrar F1.

| Nombre | Evidencia | Estado | Acción | Catálogo | Origen | Nota |
| --- | --- | --- | --- | --- | --- | --- |
| Malwarebytes (instalador) | ficha 08; 4.1.0.56; 31/03/2020; Baios 05/jan/2021; `09. Malwarebytes.exe` 2.02 MB ×2 (notepad-12) | pendiente | revisar | sí | notepad-01 | 4.1+ opcional. Conservar **una** copia; borrar el duplicado `(1)`. Distinto de AdwCleaner. |
| Panda (online) | 23. Panda; notepad-16: «panda si» | pendiente | revisar | sí | notepad-01 | Catálogo: alternativa (no enlace 4.0). Las notas lo marcan sí como enlace; no promover a núcleo hasta F1. |
| SpyShelter | spyshelter.com (anotado junto a bitdefender) | pendiente | revisar | ficha nueva | notepad-01 | No está en el catálogo. No es Bitdefender. |
| SUPERAntiSpyware | 13. SUPERAntiSpyware; free-edition.html (notepad-03) | pendiente | revisar | ficha nueva | notepad-01 | No está en el catálogo ni en el menú v3 documentado. |
| SpywareBlaster | 18. SpywareBlaster; brightfort.com (notepad-03) | pendiente | revisar | ficha nueva | notepad-01 | No está en el catálogo. |
| USBAV | 19. USBAV; usbavfree.com (notepad-03) | pendiente | revisar | ficha nueva | notepad-01 | Confirma producto. ¿Distinto de Sophos SBAV (LiveCD)? |
| Argente Utilities | 20. Argente Utilities; infospyware (notepad-02) | pendiente | revisar | ficha nueva | notepad-01 | Utilidades, no antimalware. Probable fuera de BAIOS 4. |
| F-Secure Online Scanner | 22. F Secure; f-secure.com/es/free-tools (notepad-02) | pendiente | revisar | ficha nueva | notepad-01 | Escáner / hub de herramientas gratis. |
| Trend Micro HouseCall | 24. Trend; free-tools.html (notepad-02) | pendiente | revisar | ficha nueva | notepad-01 | Online. En catálogo Trend solo aparece como Rescue Disk (4.2+). |
| Antiscan.me | 26. Antiscan.me | pendiente | revisar | ficha nueva | notepad-01 | Carga de archivos. |
| Avira Analysis | 27. Avira Analysis | pendiente | revisar | ficha nueva | notepad-01 | Envío de muestras, no el instalador Avira. |
| Dr.Web (envío online) | 28. DrWeb | pendiente | revisar | ficha nueva | notepad-01 | Distinto de CureIt portable. |
| FortiGuard Online Scanner | 29. Fortiguard Online | pendiente | revisar | ficha nueva | notepad-01 | Carga de archivos. |
| Farbar Recovery Scan Tool | techspot FRST (notepad-03) | pendiente | revisar | ficha nueva | notepad-03 | Clásico de técnico. No estaba en el catálogo. |
| Comodo Cleaning Essentials | «Comodo cleaning essentials» (notepad-03) | pendiente | revisar | ficha nueva | notepad-03 | Portable de limpieza. Distinto del instalador Comodo (desuso). |
| GMER | gmer.net (notepad-03) | pendiente | revisar | ficha nueva | notepad-03 | Anti-rootkit. Confirmar si el sitio y el binario siguen vivos. |
| TDSSKiller | infospyware/tdsskiller (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Portable Kaspersky anti-rootkit. Distinto de KVRT. |
| Webroot (herramienta de eliminación) | «herramienta de eliminacion webroot» (notepad-03) | pendiente | revisar | ficha nueva | notepad-03 | Removal tool, no el AV residente Webroot. |
| Disinfect't | «Disinfect't from heise Security» (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Herramienta Heise. Confirmar vigencia. |
| ComboFix | infospyware/combofix (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Histórica; abandonada y puede romper el sistema. No usar a ciegas. |
| AVZ | z-oleg.com/secur/avz (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Clásica rusa. Confirmar vigencia y licencia. |
| WinsockFix | infospyware/winsockfix (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Reparación de red, no antimalware. |
| Revo Uninstaller | infospyware/revo-uninstaller (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Desinstalador. Probable fuera del núcleo de seguridad. |
| OTM | infospyware/otm (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | OldTimer; limpieza de técnico. |
| ERUNT | infospyware/erunt (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Backup de registro (viejo). |
| Glary Utilities | glarysoft.com/downloads (notepad-03) | pendiente | revisar | ficha nueva | notepad-03 | Utilidades. Probable fuera de BAIOS 4. |
| RogueKiller | solo aparece vía megawarez (notepad-04) | pendiente | revisar | ficha nueva | notepad-04 | Producto real (Adlice). **No** usar megawarez. Si F1 lo evalúa, solo web oficial. |
| Armadito AV | github.com/armadito/armadito-av; «contactar» (notepad-04) | pendiente | revisar | ficha nueva | notepad-04 | Proyecto OSS; no guardar binario en los 10 GB. Contacto ≠ incluirlo. |
| eScan MWAV toolkit | escanav.com mwav-tools (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Toolkit gratis. Distinto de eScan Enterprise (desuso). |
| GridinSoft Anti-Malware | gridinsoft.com/antimalware (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Confirmar licencia y si es portable. |
| VBA32 | anti-virus.by (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | En catálogo hay VBA32 Rescue (LiveCD 4.2+), no este sitio como portable. |
| Outpost Firewall Free | infospyware/outpost (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Cortafuegos, no motor BAIOS. |
| ZoneAlarm | zonealarm free-antivirus + infospyware firewall (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | AV y/o firewall. No hay ficha. |
| Quick Heal (herramientas) | quickheal.com/useful-tools (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Hub de herramientas; no hay ficha. |
| G Data | gdata.es/descargas (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Hub de descargas; no hay ficha. |
| WiseCleaner | wisecleaner.com/products (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Utilidades. Probable fuera de BAIOS 4. |
| Rising Antivirus | rising-global.com (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | No hay ficha. |
| NPAV | npav.net/downloads (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Net Protector. No hay ficha. |
| AhnLab | AHNLAB (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Marca sin URL de herramienta concreta. |
| nProtect | onlinesecurity.nprotect.com (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Escáner online. |
| Enigma Software | enigmasoftware.es/productos; «no portable» (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | SpyHunter y similares. Notepad: no portable. |
| BKAV | BKAV (notepad-02) | pendiente | revisar | ficha nueva | notepad-02 | Marca sin URL de herramienta concreta. |
| Enlaces Técnico extra | Metadefender (opswat.com); Virscan; Kaspersky online; sandboxes; URL Void; Diario/Metashield/Iris-H; malwaretracker.com/doc.php; exif.regex.info (notepad-05/16) | pendiente | revisar | ficha nueva | notepad-05 | Solo **enlaces**, no binarios. F1-06 ya cubre VT/ESET/Jotti/Hybrid. No reabrir Form4. |
| Removal tools extra | MSRT (Microsoft), Sophos VRT, F-Secure Easy Clean, Norman Malware Cleaner, Avira Removal Tool, Kaspersky Security Scan (notepad-05) | pendiente | revisar | ficha nueva | notepad-05 | Distintos de KVRT y de CCE. MSRT es Microsoft; el resto a confirmar portable vs instalador. |
| Rogue extras | RKill, Unhide, ERA Remover, Remove Fake AV (notepad-05) | pendiente | revisar | ficha nueva | notepad-05 | Clásicos de técnico. RogueKiller ya tiene fila. No embeber packs. |
| MBR / rootkit extra | aswMBR, Rootkit Revealer, F-Secure BlackLight, Bootkit Removal, MBR Fix (notepad-05) | pendiente | revisar | ficha nueva | notepad-05 | GMER y TDSSKiller ya tienen fila. Muchos están abandonados. |
| Sysinternals extra | Process Explorer, Process Monitor, TCPView; Driver View; SysInspector; OTL (=OTM) (notepad-05/06) | pendiente | revisar | ficha nueva | notepad-05 | Autoruns ya es núcleo 4.0. ProcExp/Procmon/TCPView encajan en diagnóstico Técnico. Process Hacker/Lasso no. |
| Inventario hardware | GetSystem Info, CPU-Z, SIW, HWInfo, Speccy, CrystalDiskInfo, HD Tune (notepad-06) | pendiente | revisar | ficha nueva | notepad-06 | F3-05 es nativo (SMART/hardware). No hace falta guardar las siete copias; como mucho una portable de disco si el nativo no llega. |
| BSOD | BlueScreenView, WhoCrashed (notepad-06) | pendiente | revisar | ficha nueva | notepad-06 | Útil en modo Técnico. No es núcleo 4.0. |
| InfoSpyware extra | JRT, IFS, MBAR, MBAE, Panda USB Vaccine, KSS (notepad-12) | pendiente | revisar | ficha nueva | notepad-12 | Fichas de blog, no del menú v3. MBAR/MBAE están discontinuados. MRT = MSRT (ya en removal extra). |

## Desuso

Candidatos a borrar de la carpeta local. Los instaladores de antivirus residentes no los lanza BAIOS. Las ISO Rescue no se embeben ([F5-03](backlog.md)): solo URLs oficiales en 4.2+.

| Nombre | Evidencia | Estado | Acción | Catálogo | Origen | Nota |
| --- | --- | --- | --- | --- | --- | --- |
| NoBot | historial repo 1.0.5.8 fab. 09/05/2020 Act. BAIOS 19/08/2021 | desuso | borrar | sí | notepad-01 | Abandonada; fuera. Créditos también 1.0.5.7. |
| IObit Malware Fighter | 11. IObit 8.8.0.850 (historial repo) | desuso | borrar | sí | notepad-01 | `fuera_de_4.0`. |
| ClamWin | ficha 09; Fecha 31/3/2020; versión vacía; Baios 05/jan/2021 (notepad-04) | desuso | borrar | sí | notepad-01 | Abandonada. GitHub clamwin = el mismo producto. |
| Zemana AntiMalware | ficha 14; 3.1.495; 31/3/2020; Baios 05/jan/2021 (notepad-04) | desuso | borrar | sí | notepad-01 | Abandonada. |
| Immunet | ficha 16; Version/Fecha vacíos; immunet.com (notepad-04) | desuso | borrar | sí | notepad-03 | Abandonada. No inventar versión. |
| Adaware | ficha 15; Lavasoft trial (notepad-02/08) | desuso | borrar | sí | notepad-03 | `fuera_de_4.0`. El enlace secure.lavasoft.com es instalador de prueba, no portable. |
| Vipre Rescue | ficha 12; 7.0.7.8; 31/3/2020; prueba 30 días; Baios 05/jan/2021 (notepad-04) | desuso | borrar | sí | notepad-01 | Retirado en v2.1.0. Web histórica en créditos; no relanzar. |
| Norton Power Eraser | historial repo 6.0.1.2095 fab. 06/05/2021 Act. BAIOS 19/08/2021 | desuso | borrar | sí | notepad-07 | Catálogo: abandonada, EOL 2026-04-30. Estaba en el repo v3; no relanzar. |
| McAfee Stinger | historial repo 12.2.0.304 fab. 16/08/2021 Act. BAIOS 19/08/2021 | desuso | borrar | sí | notepad-07 | F1-04 cerrado: fuera. Créditos también 12.1.0.3432. |
| K7 Disinfector | historial repo 12.0.0.240 | desuso | borrar | sí | notepad-07 | F1-04 cerrado: fuera. |
| Dr.Web CureIt! | ficha vacía; historial repo sin versión (15. Ejecutables) | desuso | borrar | sí | notepad-07 | Ficha nunca hecha; catálogo fuera. |
| Kaspersky (instalador) | 33. Kaspersky | desuso | borrar | no | notepad-01 | AV residente. Conservar solo KVRT si F1 lo confirma. |
| McAfee (instalador) | 34. McAfee | desuso | borrar | no | notepad-01 | AV residente. Stinger va en pendiente (F1-04). |
| Bitdefender (instalador) | 35. Bitdefender | desuso | borrar | no | notepad-01 | AV residente. |
| Avast (instalador) | 36. Avast | desuso | borrar | no | notepad-01 | AV residente. |
| AVG (instalador) | 37. AVG | desuso | borrar | no | notepad-01 | AV residente. |
| Avira (instalador) | 38. Avira | desuso | borrar | no | notepad-01 | AV residente. El envío «Avira Analysis» sigue en pendiente. |
| ESET (instalador) | 39. Eset | desuso | borrar | no | notepad-01 | AV residente. Conservar el enlace ESET Online. |
| Comodo (instalador) | 40. Comodo; free-antivirus.php (notepad-03) | desuso | borrar | no | notepad-01 | AV residente. CCE (limpieza) sigue en pendiente. Rescue Disk: 4.2+ solo URL. |
| Panda (instalador) | 41. Panda | desuso | borrar | no | notepad-01 | AV residente. El online sigue en pendiente. |
| BullGuard (instalador) | 43. Bullguard | desuso | borrar | no | notepad-01 | AV residente. |
| Dr.Web (instalador) | 44. Dr Web; drweb.com (notepad-02/08) | desuso | borrar | no | notepad-01 | AV residente. CureIt también desuso (ficha nunca hecha). |
| Sophos (instalador) | 45. Sophos | desuso | borrar | no | notepad-01 | AV residente. HitmanPro es el portable. |
| Norton (instalador) | 46. Norton | desuso | borrar | no | notepad-01 | AV residente. Conservar NPE. |
| Trend Micro (instalador) | 47. TrendMicro | desuso | borrar | no | notepad-01 | AV residente. HouseCall sigue en pendiente. |
| Microsoft Defender (paquete) | «antivirus microsoft» / MICROSOFT (notepad-01/02) | desuso | borrar | sí | notepad-01 | No es portable: módulo nativo del motor. |
| CrowdStrike | Crowdstrike (notepad-02) | desuso | borrar | no | notepad-02 | EDR empresarial. Fuera de BAIOS. |
| Elastic | Elastic (notepad-02) | desuso | borrar | no | notepad-02 | EDR empresarial. |
| FireEye | FireEye (notepad-02) | desuso | borrar | no | notepad-02 | EDR empresarial (Mandiant/Trellix). |
| Cylance | CYLANCE (notepad-02) | desuso | borrar | no | notepad-02 | EDR empresarial. |
| Check Point | CHECKPOINT (notepad-02) | desuso | borrar | no | notepad-02 | Empresarial. |
| Heimdal Security | HEIMDAL SECURITY (notepad-02) | desuso | borrar | no | notepad-02 | Empresarial / suite. |
| Northguard | NORTHGUARD (notepad-02) | desuso | borrar | no | notepad-02 | No hay ficha ni lanzamiento. |
| Baidu Antivirus | baidu antivirus (notepad-02) | desuso | borrar | no | notepad-02 | No entra en BAIOS. |
| Tencent PC Manager | tencent pc manager (notepad-02) | desuso | borrar | no | notepad-02 | Suite; no entra. |
| 360 Total Security | 360totalsecurity tools (notepad-02) | desuso | borrar | no | notepad-02 | Suite; no entra. |
| Iolo System Mechanic | iolo.com (URL de afiliado, notepad-02) | desuso | borrar | no | notepad-02 | Optimizador; no es herramienta de orquestación. |
| Advanced System Protector | systweak.com (notepad-02) | desuso | borrar | no | notepad-02 | Optimizador / PUP frecuente. |
| Surfshark | SURFSHARK (notepad-02) | desuso | borrar | no | notepad-02 | VPN, no antimalware. |
| Total AV | TOTAL AV (notepad-02) | desuso | borrar | no | notepad-02 | AV residente de consumo. |
| Total Defense | TOTAL DEFENSE (notepad-02) | desuso | borrar | no | notepad-02 | AV residente. |
| PC Matic | PC MATIC (notepad-02) | desuso | borrar | no | notepad-02 | AV residente. |
| Avetix | avetix-antivirus-free software.informer (notepad-02) | desuso | borrar | no | notepad-02 | Fuente poco fiable; no hay ficha. |
| Varist | varist.com (notepad-02) | desuso | borrar | no | notepad-02 | No hay ficha. |
| eScan Enterprise | ESCAN ENTERPRISE (notepad-02) | desuso | borrar | no | notepad-02 | Edición empresarial. El toolkit MWAV sigue en pendiente. |
| eSafe | esafe.com.cy (notepad-02) | desuso | borrar | no | notepad-02 | No hay ficha. |
| Universal Antivirus Remover | «universal antivirus remover» (notepad-03) | desuso | borrar | no | notepad-03 | No es un escáner; riesgo de romper el AV residente. |
| EICAR | «eicar antivirus» (notepad-03) | desuso | borrar | no | notepad-03 | Archivo de prueba, no un producto a guardar en el kit. |
| HelpNDoc | helpndoc.com/es/descarga (notepad-03) | desuso | borrar | no | notepad-03 | Autoría de ayuda; ya está en [creditos.md](creditos.md). No va en los 10 GB de herramientas. |
| Inno Setup | jrsoftware.org/isinfo.php (notepad-03) | desuso | borrar | no | notepad-03 | Compilador de instaladores. No es herramienta de sesión. |
| LiveCD / Rescue (ISO de fabricante) | notepad-03 ssdm2pro + notepad-05: Kaspersky, AVG, Panda, Bitdefender, F-Secure, Avira/Antivir, Dr.Web, Spybot Live, eScan, Comodo, Acronis (notepad-05) | desuso | borrar | sí | notepad-03 | 4.2+: **solo URLs oficiales**. Emsisoft Emergency Kit no es LiveCD (está en uso como portable). |
| Hiren's Boot CD | HIREN BOOT; youtube.be/1AoJJ5YbkzQ (notepad-05) | desuso | borrar | no | notepad-05 | Recopilación tipo MediCat. **Borrar ISO** de los 10 GB. No es producto BAIOS. |
| SARDU | Live CD miscelánea (notepad-05) | desuso | borrar | no | notepad-05 | Generador de USB multiboot. Fuera. |
| Boot miscelánea | GParted, BartPE, MSDaRT, Inquisitor, NST, SystemRescue, BootRepair, UBCD, Super Fdisk, Memtest, Trinity Rescue Kit (notepad-05) | desuso | borrar | no | notepad-05 | ISO de recuperación genérica. No embeber. Memtest/disco pueden ser nativos después; no el ISO. |
| Distros pentest / forense | Kali, BlackArch, Parrot, Wifislax, Wifiway, ArchStrike, Qubes, Tails, CAINE, Samurai, Backbox, Tsurugi, Fedora Security Labs (notepad-05) | desuso | borrar | no | notepad-05 | Fuera de alcance. BAIOS no es un lab de pentest. |
| Lab RE / dump / PE | Ghidra, IDA, Olly, PEiD, PE Tools, LordPE, HxD, WinHex, Resource Hacker, ExeScope, PDF parsers, UPX/unpackers, ExifTool/metadatos, decompiladores (VB/Delphi/ILSpy/Reflector), RegShot, SysAnalyzer, Malcode Analysis Pack, Sandboxie, VMware, VirtualBox (notepad-05/06) | desuso | borrar | no | notepad-05 | Análisis de malware, no orquestación de sesión. Fuera de 4.x. Borrar el pack del disco. |
| Gestores y wipe | Process Hacker, Process Lasso, Free/Total Commander; Unlocker, File/Reg Assassin, Eraser, HardWipe, Pocket Killbox, HashTab (notepad-06) | desuso | borrar | no | notepad-06 | BAIOS no borra a ciegas ni sustituye el Explorador. Hash: el manifiesto 4.1 lleva sha256, no HashTab. |
| Envío de muestras | Portales Microsoft, Emsisoft, Sophos, ESET, Avira, Google, Symantec, ClamAV, ThreatExpert (muerto), etc. (notepad-06) | desuso | borrar | no | notepad-06 | Bookmarks de fabricante. No son programas del kit. ThreatExpert ya estaba muerto en notepad-05. |
| Red ofensiva / sniffers | Wireshark, Nessus, Cain&Abel, Fiddler, Angry IP, WinPcap, GFI Languard (notepad-05) | desuso | borrar | no | notepad-05 | 4.0 ya cubre ping/DNS nativos. Cain&Abel no entra. |
| IDS / firewall extra | Snort, OSSEC, Suricata; Ashampoo, Online Armor, PC Tools Firewall, Comodo firewall (notepad-05) | desuso | borrar | no | notepad-05 | Outpost/ZoneAlarm siguen en pendiente. El resto no es el motor. |
| Correo temporal / proxies / encode | 10 minute mail, YopMail, HideMyAss, KProxy, Elhacker encoder, etc. (notepad-05) | desuso | borrar | no | notepad-05 | No son herramientas de diagnóstico BAIOS. |
| Vacunas y decryptores en disco | Vacunas de marca; Polifix, Rannoh/Rector/Xorist; ThreatExpert (muerto) (notepad-05) | desuso | borrar | no | notepad-05 | Vacunas ≠ programa a guardar. Decryptores: enlace a sitio oficial si F1 lo pide, no el pack en los 10 GB. |
| WinPE Sergei Strelec | ssdm2pro/winpe-10-8-sergei-strelec (notepad-03) | desuso | borrar | no | notepad-03 | WinPE de terceros. No es Rescue de fabricante; no entra en BAIOS. |
| MediCat DVD | ssdm2pro/medicat-dvd (notepad-03) | desuso | borrar | no | notepad-03 | Recopilación tipo Hiren. Fuera: no ISO embebidas. |
| Windows Recovery Tools (PE) | ssdm2pro/windows-recovery-tools-bootable-pe (notepad-03) | desuso | borrar | no | notepad-03 | WinPE de terceros. Fuera. |
| Defender Control | sordum.org/9480; «dblock»; BlueLife (notepad-03/04) | desuso | borrar | no | notepad-04 | Apaga Defender. BAIOS no desactiva la protección residente de forma permanente. |
| Defender Exclusion Tool | sordum.org/10636 (notepad-04) | desuso | borrar | no | notepad-04 | Exclusiones de Defender. Fuera del producto. |
| Sordum Temp Cleaner | sordum.org/9190 (notepad-04) | desuso | borrar | no | notepad-04 | El mantenimiento nativo de 4.0 cubre temporales. |
| Copias megawarez | megawarez.org: RogueKiller, Steganos, GridinSoft (notepad-04) | desuso | borrar | no | notepad-04 | Fuente no oficial / warez. Borrar del disco. No es origen de F1. |
| Steganos Privacy Suite | megawarez (notepad-04) | desuso | borrar | no | notepad-04 | Suite de privacidad, no orquestación. Además venía de warez. |
| Escáneres online muertos | CA, freedom.net, Sunbelt, SpywareGuide, Bitdefender scan8 IE, McAfee MFS, Kaspersky sp/virusscanner (notepad-04) | desuso | borrar | no | notepad-04 | URLs de ~2000s. HouseCall/Panda vigentes van en pendiente; VirusTotal/ESET en uso. |
| AutoPlay Media Studio 8 | «autoplay»; Definicion de Software v1.0.0 (notepad-04) | desuso | borrar | no | notepad-04 | Herramienta de *build* v2, no kit de sesión. Ya está en [creditos.md](creditos.md). |
| CCleaner / RegSeeker | infospyware/ccleaner, regseeker (notepad-12) | desuso | borrar | no | notepad-12 | 4.0 limpia temporales nativo. No embeber CCleaner. |
| K7 utilitarios Full | `05. UTILITARIOS`: K7AutorunTweaker, k7downadupremover, K7TdssRemover (notepad-12) | desuso | borrar | no | notepad-12 | Van con K7 (fuera). TDSSKiller de Kaspersky sigue en pendiente. |
| Lab forense / stego / cripto | Autopsy, Volatility, EnCase, REMnux, SIFT, Sleuthkit, Helix, TrueCrypt, PGP, Steghide, Cryptool, Gpg4win, WinFE, SpinRite (notepad-14) | desuso | borrar | no | notepad-14 | Lab de forense, no sesión BAIOS. ISO/suites fuera. TrueCrypt está abandonado. |
| Navegadores y complementos | Chrome/Firefox/Edge/Opera/Safari; Tor, UltraSurf; WOT, NoScript, Adblock, Ghostery (notepad-14) | desuso | borrar | no | notepad-14 | El usuario ya tiene navegador. BAIOS no distribuye Tor ni extensiones. |
| Desinstaladores de AV | Cleanups de Kaspersky, Norton, McAfee, MB CleanUp Tool, OneCare, etc. (notepad-14) | desuso | borrar | no | notepad-14 | No es un kit de quitar el AV residente. Si F1 pide uno, solo el oficial del fabricante que se esté usando. |
| Remoto / parental / drivers / firmware | PuTTY, TeamViewer, k9, packs de drivers OEM, firmware de routers ISP (notepad-14) | desuso | borrar | no | notepad-14 | Fuera de alcance. No embeber catálogos de drivers. |
| HDD/BIOS unlock y undelete | HDD Unlock, Atapwd, CMOS Pwd, Recuva, PhotoRec, TestDisk, Defraggler (notepad-14) | desuso | borrar | no | notepad-14 | BAIOS no desbloquea BIOS/HDD ni sustituye recuperación de datos. TestDisk/PhotoRec no van en el AIO. |

## Duplicados

Misma marca o la misma lista copiada otra vez. No son programas distintos.

| Nombre | Evidencia | Estado | Acción | Catálogo | Origen | Nota |
| --- | --- | --- | --- | --- | --- | --- |
| Kaspersky | total av: kaspersky | duplicado | borrar | — | notepad-01 | Ver KVRT (en uso) e instalador (desuso). |
| Malwarebytes | total av: malwarbytes | duplicado | borrar | — | notepad-01 | Ver AdwCleaner (en uso) e instalador (pendiente). |
| McAfee | total av: mcfee | duplicado | borrar | — | notepad-01 | Ver Stinger (pendiente) e instalador (desuso). |
| Norton | total av: norton | duplicado | borrar | — | notepad-01 | Ver NPE (en uso) e instalador (desuso). |
| Bitdefender | total av: bitdefender | duplicado | borrar | — | notepad-01 | Ver SpyShelter (pendiente, otra herramienta) e instalador (desuso). |
| Avast | total av: avast | duplicado | borrar | — | notepad-01 | Ver instalador (desuso). |
| Microsoft Defender | total av: antivirus microsoft | duplicado | borrar | — | notepad-01 | Ver fila desuso «paquete». |
| ESET | total av: eset | duplicado | borrar | — | notepad-01 | Ver ESET Online (en uso) e instalador (desuso). |
| Ejecutables 11–20 | bloque EJECUTABLES notepad-03 | duplicado | borrar | — | notepad-03 | Misma lista que notepad-01. |
| Online 21–31 | bloque ONLINE notepad-03 | duplicado | borrar | — | notepad-03 | Misma lista que notepad-01. |
| EMSISOFT | columna revisado | duplicado | borrar | — | notepad-02 | Ver Emergency Kit (en uso). |
| SOPHOS | columna revisado (dos veces) | duplicado | borrar | — | notepad-02 | Ver HitmanPro (en uso) e instalador (desuso). |
| LAVASOFT | columna revisado + trial | duplicado | borrar | — | notepad-02 | Ver Adaware (desuso). |
| NOD32 | infospyware/nod32 | duplicado | borrar | — | notepad-02 | Ver ESET. |
| ClamAV | clamav.net | duplicado | borrar | — | notepad-02 | Motor; el producto en notas es ClamWin (desuso). |
| Spybot (infospyware) | infospyware/spybot | duplicado | borrar | — | notepad-02 | Ver Spybot (en uso). |
| SpywareBlaster (infospyware) | infospyware/spywareblaster | duplicado | borrar | — | notepad-02 | Ver SpywareBlaster (pendiente). |
| K7 SECURITY | columna revisado | duplicado | borrar | — | notepad-02 | Ver K7 Disinfector (desuso). |
| Comodo free AV | antivirus.comodo.com/free-antivirus.php | duplicado | borrar | — | notepad-03 | Ver instalador Comodo (desuso) vs CCE (pendiente). |
| Flaticon / Acerca de | srip, Smashicons, diálogo versión y créditos | duplicado | borrar | — | notepad-03 | Ya está en [creditos.md](creditos.md). No es programa del kit. |
| Spybot Beta | download.spybot.info/Beta (notepad-03) | duplicado | borrar | — | notepad-03 | Misma línea Spybot (en uso); no guardar un segundo árbol «Beta» en los 10 GB. |
| Fichas 09–16 repetidas | ClamWin, Emsisoft, Spybot, Vipre, Safety Scanner, Zemana, CureIt, Immunet, Adaware (notepad-04, 3–4 copias) | duplicado | borrar | — | notepad-04 | Mismas fichas; se unificó versión/fecha en la fila canónica. |
| Marcas notepad-04 | Avast, AVG, Eset, Kaspersky, McAfee, Bitdefender, GDATA, Avira, K7, Norton, Panda, TOTAL Av, Vipre, TrendMicro, ahnlab, check point | duplicado | borrar | — | notepad-04 | Ya clasificadas en tandas 01–02. |
| GitHub ClamWin | github.com/clamwin/clamwin; «contactar» (notepad-04) | duplicado | borrar | — | notepad-04 | Ver ClamWin (desuso). |
| Notepad++ | «notepad++» (notepad-04) | duplicado | borrar | — | notepad-04 | Autoría; ya está en [creditos.md](creditos.md). |
| Árbol notepad-05 (ya clasificado) | ESET/Panda/Trend/F-Secure online, VT, Jotti, Hybrid, FortiGuard, Antiscan, Dr.Web online, AdwCleaner, Hitman, Spybot, NPE, KVRT, Stinger, ClamWin, Immunet, Adaware, CCE, ComboFix, GMER, TDSSKiller, AVZ, WinsockFix, RogueKiller, Windows Defender (notepad-05) | duplicado | borrar | — | notepad-05 | Reaparece en otra taxonomía; la fila canónica ya existe. |
| Emsisoft como Live CD | «Emisoft Emergency Kit» bajo Live CD (notepad-05) | duplicado | borrar | — | notepad-05 | Es portable (en uso), no ISO. |
| Lab notepad-06 (ya clasificado) | OTM, PEiD, RunScanner, ThreatExpert, envío a fabricantes ya listados (notepad-06) | duplicado | borrar | — | notepad-06 | OTM tiene fila pendiente; PEiD/RE van en desuso lab. |
| Historial repo 11–47 | Ejecutables, Online, Instaladores (notepad-07) | duplicado | borrar | — | notepad-07 | Misma lista que notepad-01. Solo 01–10 aportaban versiones. |
| Hubs REVISAR EN DETALLE | 15 URLs de fabricante (notepad-08 = columna notepad-02) | duplicado | borrar | — | notepad-08 | Veredicto por URL en la nota de la tanda; no son 15 programas nuevos. |
| LISTAS rankings | mejor-antivirus, AV-Comparatives, Guru99, VB100, AV-Test, Expert Insights, DragonLatam, TrustPort, CSA 2021, DragonJAR, SafetyDetectives (notepad-09) | duplicado | borrar | — | notepad-09 | No son herramientas. Si hay HTML guardado en los 10 GB, borrar. |
| Columna revisado | CrowdStrike…Immunet (notepad-10 = notepad-02) | duplicado | borrar | — | notepad-10 | Marcas ya clasificadas. NOD32 = ESET. Sophos dos veces = HitmanPro + instalador. |
| Columna OPCIONADO | Zemana, Enigma, Spybot, IObit, KVRT, InfoSpyware, eScan Enterprise, ComboFix, VBA32, 360, GridinSoft, Avetix, ClamWin/ClamAV, AVZ, TDSSKiller, Outpost, ZoneAlarm, SpywareBlaster (notepad-11) | duplicado | borrar | — | notepad-11 | URLs de notepad-02. Artículos InfoSpyware/Latina no son programas. |
| Fichas 08–16 + Sordum/Hiren | Malwarebytes 4.1.0.56, ClamWin, EEK, Spybot, Vipre, MSERT, Zemana, CureIt, Immunet, Adaware, dblock, usbav, megawarez, Hiren (notepad-12) | duplicado | borrar | — | notepad-12 | Mismas fichas; árbol Full es la evidencia de disco. |
| InfoSpyware índice | Listas antivirus/spyware/rootkits/cortafuegos/herramientas (notepad-12) | duplicado | borrar | — | notepad-12 | Bookmarks del blog. JRT/MBAR/MBAE van en pendiente «InfoSpyware extra». |
| Ejecutables 08–16 otra vez | Malwarebytes, ClamWin, EEK, Spybot, Vipre, MSERT, Zemana, CureIt, Immunet, Adaware, InfoSpyware, ssdm2pro, CCE, FRST, GMER (notepad-13) | duplicado | borrar | — | notepad-13 | Igual que notepad-12 sin tamaños de disco. |
| Taxonomía notepad-14 | Servicios Online, Live CD, Seguridad, Redes, Análisis (notepad-05/06 otra vez) | duplicado | borrar | — | notepad-14 | Hiren/YouTube ya en desuso. Autoruns ya es núcleo. Lo nuevo está en las filas forense/navegadores/unlock. |
| Historial repo otra vez | Portables 01–10 + ejecutables/online/instaladores 11–47 (notepad-15 = notepad-07) | duplicado | borrar | — | notepad-15 | Versiones ya en [creditos.md](creditos.md). |
| Jotti / Dr.Web ES | virusscan.jotti.org/es; drweb-av.es (notepad-16) | duplicado | borrar | — | notepad-16 | Jotti ya en uso. drweb-av.es es el sitio del instalador (desuso). |

## Árbol Full (`CD_Root`) — notepad-12

Listado relativo (sin `H:\`). Tamaños del notepad. Acción según las tablas de arriba. Los `+` del notepad se ignoran.

**Borrar ya (~1,3 GB):** Stinger 19 + NoBot 2 + NPE 13 + IObit 54 + ClamWin 226 + CureIt 243 + Zemana 12 + EEK duplicado 297 + Emsisoft CLI 273 + instaladores AV (salvo una copia MB) + AutoPlay `.cdd`/botones + K7 zips. SUPERAntiSpyware 181 MB sigue en pendiente.

### Portables (`AutoPlay/Docs/01. PORTABLES`)

| Archivo | Tamaño | Acción |
| --- | --- | --- |
| `01.adwcleaner.exe` | 8.14 MB | conservar (núcleo 4.0; versión del binario a comprobar) |
| `02.HiJackThis.exe` | 7.02 MB | conservar (4.1+) |
| `03. McAfee Stinger.exe` | 18.88 MB | borrar |
| `04. NoBot.exe` | 1.65 MB | borrar |
| `05. HitmanPro.zip` | 13.38 MB | conservar (4.1+) |
| `06. Kaspesky VRT.exe` | 107.22 MB | conservar (4.1+); el nombre tiene typo Kaspesky |
| `07. Norton Power Ereaser.exe` | 13.21 MB | borrar (EOL) |
| `08. MicrosoftSafetyScan.zip` | 342.43 MB | conservar **una** copia fresca; el zip caduca ~10 días, no archivar esta |
| `10. ZHPCleaner.exe` | 3.14 MB | conservar (4.1+) |

No hay `09` en esta carpeta: Malwarebytes está en instaladores.

### Ejecutables (`02. EJECUTABLES`)

| Archivo | Tamaño | Acción |
| --- | --- | --- |
| `11. IObit Malware Fighter.exe` | 54.34 MB | borrar |
| `12. spybotsd.exe` | 66.67 MB | revisar (4.1+ opcional) |
| `13. SUPERAntiSpyware.exe` | 180.92 MB | revisar |
| `14. Clamwin.exe` | 225.86 MB | borrar |
| `14. EmsisoftEmergencyKit.exe` | 343.34 MB | conservar **una** EEK (número 14 duplicado con ClamWin) |
| `15. DrWeb CureIt!.exe` | 243.23 MB | borrar |
| `16. EmsisoftEmergencyKit.exe` | 297.35 MB | borrar (duplicado de 14) |
| `17. zemana_AntiMalware_Setup.exe` | 12.15 MB | borrar |
| `18. SpywareBlaster.exe` | 4.23 MB | revisar |
| `19. USBAV.exe` | 20.1 MB | revisar |
| `20. Argente Utilities.zip` | 7.93 MB | revisar |
| `EmsisoftCommandlineScanner64.exe` | 272.83 MB | borrar (duplicado de EEK) |

### Online (`03. ONLINE`)

Lanzadores locales. Preferible enlace en Técnico, no el `.exe` viejo.

| Archivo | Tamaño | Acción |
| --- | --- | --- |
| `esetonlinescanner.exe` | 11.15 MB | revisar (en uso como enlace; este binario puede caducar) |
| `F-SecureOnlineScanner.exe` | 11.83 MB | revisar |
| `HousecallLauncher.exe` | 2.74 MB | revisar |
| `PandaCloudCleaner.exe` | 36.42 MB | revisar |
| `BAIOSEscudo.ico` | 211 KB | no es herramienta |

### Instaladores (`04. INSTALADORES`)

| Archivo | Acción |
| --- | --- |
| `09. Malwarebytes.exe` / `(1)` 2.02 MB ×2 | conservar una |
| `10. Kaspersky_Installer` ×2, McAfee, Bitdefender, Avast, AVG, Avira, Comodo `cav_installer`, ESET NOD32 live, Panda, Vipre ×2 | borrar |

### Utilitarios (`05. UTILITARIOS`)

| Archivo | Acción |
| --- | --- |
| K7AutorunTweaker / downadup / TdssRemover (zips) | borrar |
| `sysinspector_nt32_esl.exe` 5.96 MB | revisar (SysInspector, pendiente) |

### Build AutoPlay (no es el kit 4.0)

`BAIOS.exe` 6.86 MB, `BAIOS.cdd` 2.86 MB, `Buttons/`, `Icons/`, `Images/`, `debug.log`: prototipo v2. No versionar en Git. Conservar fuera del repo si hace falta arqueología; no forma parte de los 10 GB de motores.

Docs ahí (`CHANGELOG.txt`, `Creditos.txt`, `Detalles de Software.txt`, `00. ANTIVIRUS.txt/.xlsx`): ya consolidados en [changelog.md](changelog.md), [creditos.md](creditos.md), [catalogo.md](catalogo.md) y este inventario. No hace falta el xlsx en disco.

## Referencias (no son programas)


Bookmarks de labs, rankings y blogs. No lanzan nada; no ocupan ficha. Si hay capturas o HTML guardados en los 10 GB, se pueden borrar.

- Labs / rankings (notepad-02): AV-Comparatives, AV-Test, Virus Bulletin / VB100, Guru99, Expert Insights, Computing Security Awards, SafetyDetectives, Wikipedia TrustPort, DragonJAR «equipo ideal», DragonLatam malware 2021, mejor-antivirus.es.
- Artículo infospyware: no tener dos antivirus instalados.
- Blogs Rescue (notepad-03): ssdm2pro (WinPE Strelec, MediCat, varios Rescue). Sustituir por URLs oficiales en el catálogo 4.2+; no usar el blog como fuente.
- Per Antivirus (nota periodística notepad-02): no es una herramienta a guardar.
- Changelog Excel (notepad-04): `#ERROR!` / `#NAME?`, v1.0.0, v1.1.0, «2.1.1 Alpha», radio de riesgos, «Copyrigth license free». La fuente de verdad es [changelog.md](changelog.md); no reparsear la hoja rota.
- Lifewire bootable antivirus; hiberhernandez descargas anti-malware; SoftZone alternativas a CCleaner; hilos de foro elhacker.net (notepad-04). Bookmarks, no binarios.
- «desactivar protección» (YouTube + cyberforum.ru, notepad-04): fuera de producto. BAIOS no documenta ni guarda herramientas para apagar el AV residente.
- Hiren's (notepad-05): `youtube.be/1AoJJ5YbkzQ` es un vídeo, no una ISO oficial. No sustituye fuente de fabricante.
- Consolidar changelog / créditos / README (notepad-12): ya hecho en [changelog.md](changelog.md), [creditos.md](creditos.md) y [README.md](../README.md). El flujo AutoPlay (bienvenida → acuerdo → menú Portables/Ejecutables/Online, «desactivar Defender») es herencia v2; no se restaura. Defender no se apaga.
- Tutoriales (notepad-14): modo seguro, BSOD, hosts, cheat sheets, guías AVZ/OTL/Procmon. Son documentación externa, no binarios. No copiar foros enteros al repo.
- Manifiesto Staff elhacker.net (notepad-16): pack de foro (XP–W10, móvil, forense, LiveCD, stego, drivers). **No es la visión BAIOS 4** ([vision.md](vision.md)). Windows 9 no existe; XP no entra.
- Assets UI (notepad-16): IconArchive (iconsmind), Pexels, Radial Chart Image Generator. Diseño, no kit. Atribución si se usaron: [creditos.md](creditos.md) (hoy Flaticon).

## Bandeja de entrada

Pegar aquí cada notepad en crudo (un bloque por archivo). Tras clasificar la tanda, vaciar este apartado y dejar el aviso de «vacía».

No pegar rutas absolutas de la máquina; si el notepad las trae, recortar a nombre de archivo o ruta relativa al unificar.

**Estado:** vacía. Tandas notepad-01 a notepad-16 clasificadas (2026-08-17).

```
(pegar el siguiente notepad debajo de esta línea)
```
