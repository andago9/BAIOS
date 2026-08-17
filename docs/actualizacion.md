# Sistema de actualización

Esqueleto. Contrato cerrado en [F2-03](backlog.md); implementación en [F4](backlog.md). En **4.0 no hay auto-update** de portables: se lanzan las copias o descargas que el usuario ya tenga.

## Problema que corrige

En BAIOS 3, actualizar AdwCleaner (u otra portable) implicaba recompilar o redistribuir el AIO. El motor y las herramientas deben versionarse por separado.

## Dos planos

| Plano | Qué se actualiza | Cuándo |
| --- | --- | --- |
| Motor | `BAIOS.exe` y librerías Core | 4.2 (instalador / actualizador) |
| Herramientas | Binarios bajo `Tools/` según manifiesto | 4.1 |

## Manifiesto (borrador)

```json
{
  "name": "AdwCleaner",
  "version": "X.X.X",
  "architecture": "x64",
  "download": "https://…",
  "sha256": "…",
  "category": "adware",
  "publisher": "Malwarebytes",
  "licenseUrl": "https://…",
  "homeUrl": "https://…",
  "requiresAdmin": true,
  "arguments": [],
  "resultHints": "Revisar cuarentena; no borrar a ciegas"
}
```

Campos mínimos F2-03: identidad, versión, arch (`x64` / `x86` / `ARM64`), URL oficial, sha256, categoría, admin, cómo interpretar el resultado. Opcionales: EULA, manual, requisitos.

## Flujo 4.1

1. Leer manifiesto (embebido o remoto de confianza).
2. Comparar con `Tools/<id>/` local.
3. Si hay versión nueva: descargar a temporal, verificar sha256, sustituir.
4. Si el hash no coincide: no ejecutar, registrar error. Ver [seguridad](seguridad.md).
5. No recompilar BAIOS.

## Lo que no hace el actualizador

- No parchea herramientas de terceros.
- No descarga LiveCD.
- No actualiza Windows ni Defender (eso es el módulo nativo / Windows Update).
- En 4.0 no comprueba versiones de portables.
