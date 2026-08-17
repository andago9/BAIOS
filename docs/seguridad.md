# Seguridad de BAIOS 4

Política pública: [`SECURITY.md`](../SECURITY.md). Permisos **cerrados** en F2-04.

## Principio

Elevar solo lo necesario. No entrenar a desactivar Defender por defecto. Integridad de binarios de terceros en 4.1 (sha256).

## Permisos (F2-04)

| Acción | Admin | Notas |
| --- | --- | --- |
| Dashboard de lectura (espacio, estado Defender, IP) | No | Si WMI falla, marcar aviso |
| Análisis Defender (`MpCmdRun`) | Sí | UAC al pulsar |
| DISM, SFC | Sí | Confirmación en UI |
| CHKDSK | Sí | Advertencia de reinicio |
| Flush DNS / ipconfig /renew | Sí en la práctica | Encapsulado; si falla sin admin, explicarlo |
| AdwCleaner, MSERT | Sí (`requiresAdmin`) | Ficha |
| Autoruns | Sí recomendado | Sin admin, vista incompleta |
| Abrir URL online | No | Navegador del usuario |

Sin servicio residente en 4.0. Si falta elevación: la UI lista qué no se puede hacer; no fallar en silencio.

## Integridad (4.1)

- Solo URLs del manifiesto.
- sha256 antes de ejecutar.
- Fallo de hash: no ejecutar + log.

4.0: el usuario aporta el binario o se documenta la URL; no hay canal de update.

## Descargas y terceros

HTTPS. No embebidos masivos de AV en el repo. LiveCD: nunca ISO en BAIOS.

Al lanzar: fabricante, versión si se conoce, EULA. Aviso de falsos positivos (HijackThis en 4.1; genérico: no borrar a ciegas). Capturar código de salida; no parsear UI ajena.

## Datos

Hostname y hardware en el informe. Sin telemetría ni cuentas en 4.0.

## Código

Motor: GPL-3.0. Vulnerabilidades del **launcher/motor** a [blinter.baios@gmail.com](mailto:blinter.baios@gmail.com). Las de AdwCleaner/Defender van al fabricante.

Hasta publicar 4.0, **ninguna línea de producción recibe parches**. `test2` / 6 Alpha no es versión soportada.
