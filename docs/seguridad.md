# Seguridad de BAIOS 4

Esqueleto. Política pública resumida en [`SECURITY.md`](../SECURITY.md). Decisiones de permisos en [F2-04](backlog.md).

## Principio

BAIOS ejecuta acciones privilegiadas y lanza binarios de terceros. Debe ser más cuidadoso que un launcher: integridad, elevación mínima, y no entrenar al usuario a desactivar Defender «por defecto».

## Permisos

| Acción | Admin |
| --- | --- |
| Dashboard de lectura (WMI básico, espacio, estado Defender) | No obligatorio |
| Análisis Defender, DISM, SFC, CHKDSK, flush DNS / DHCP según política | Sí |
| Portables del núcleo (AdwCleaner, MSERT, Autoruns) | Según ficha (`requiresAdmin`) |

Si falta elevación: marcar en UI qué no se puede hacer; no fallar en silencio. UAC explícito; no un servicio residente en 4.0.

## Integridad de herramientas (4.1)

- Descargar solo URLs del manifiesto.
- Verificar sha256 antes de ejecutar.
- No ejecutar si el hash falla.
- Registrar el evento en log.

4.0: el usuario aporta el binario o se documenta la URL oficial; no hay canal de update.

## Descargas

HTTPS. No embebidos masivos de antivirus en el repo (el prototipo v3 tenía duplicados en `Resources/`; no repetir). LiveCD: nunca ISO dentro de BAIOS.

## Ejecución de terceros

- Mostrar fabricante, versión, EULA.
- Aviso de falsos positivos (HijackThis y genérico: no borrar a ciegas).
- No recomendar desactivar Defender de forma permanente. Si una herramienta lo pide, aviso puntual y restaurar.
- Capturar código de salida; no parsear UI ajena salvo que F2 lo defina.

## Datos y reportes

Hostname y hardware en el informe. Sin telemetría en 4.0. Sin cuentas. Ver [reportes](reportes.md).

## Código y suministro

- Licencia del motor: GPL-3.0. Terceros: la suya.
- No versionar secretos ni binarios de AV.
- Reportar vulnerabilidades del **motor BAIOS** (no de AdwCleaner, Defender, etc.) según [`SECURITY.md`](../SECURITY.md).

## Versiones soportadas (producto)

Hasta que exista un 4.0 publicado, **ninguna versión de producción recibe parches**. El prototipo 6 Alpha / `test2` está congelado y no es una línea soportada.
