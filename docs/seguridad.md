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

- Solo URLs https del manifiesto.
- sha256 del artefacto descargado **antes** de instalar; del ejecutable **antes** de lanzar (o del exe registrado si el hash publicado es del zip).
- Fallo de hash: no instalar / no ejecutar + `Tools/integrity.log`.
- Lanzamientos (éxito o no) en `Tools/<id>/execution.log`. HijackThis pide confirmación: los hallazgos suelen ser falsos positivos.
- Sin sha256 en la ficha: no hay canal de descarga; un binario puesto a mano sí se puede lanzar.

## Integridad (4.2, motor)

- `engine.json`: misma regla https + sha256. Fallo de hash: no aplicar; `logs/app.log`.
- Arranque, modo y excepciones en `logs/app.log`.

## Descargas y terceros

HTTPS. No embebidos masivos de AV en el repo. LiveCD: nunca ISO en BAIOS.

Al lanzar: fabricante, versión si se conoce, EULA. Aviso de falsos positivos (HijackThis en 4.1; genérico: no borrar a ciegas). Capturar código de salida; no parsear UI ajena.

## Datos

Hostname y hardware en el informe. Sin telemetría ni cuentas en 4.0.

## Código

Motor: GPL-3.0. Vulnerabilidades del **launcher/motor** a [blinter.baios@gmail.com](mailto:blinter.baios@gmail.com). Las de AdwCleaner/Defender van al fabricante.

Hasta publicar 4.0, **ninguna línea de producción recibe parches** en el sentido de un canal de soporte formal. El prototipo `test2` / 6 Alpha ya no está en el árbol.
