# Sistema de actualización

Contrato del manifiesto **cerrado** en F2-03. Implementación: [F4](backlog.md). En **4.0 no hay auto-update** de portables.

## Dos planos

| Plano | Qué | Cuándo |
| --- | --- | --- |
| Motor | `BAIOS.exe` + librerías | 4.2 |
| Herramientas | Binarios en `Tools/<id>/` | 4.1 |

## Manifiesto (contrato)

Un archivo `manifest.json` (array `tools`). Schema version `1`.

Campos **obligatorios** por herramienta:

| Campo | Tipo | Notas |
| --- | --- | --- |
| `id` | string | Estable (`adwcleaner`, `msert`, `autoruns`) |
| `name` | string | Nombre visible |
| `version` | string | SemVer o la que publique el fabricante |
| `architecture` | `x64` \| `x86` \| `arm64` | |
| `download` | URL https | Solo origen oficial |
| `sha256` | string | Hex minúsculas del archivo a ejecutar o del zip documentado |
| `category` | `adware` \| `scanner` \| `startup` \| `other` | |
| `publisher` | string | |
| `licenseUrl` | URL | |
| `homeUrl` | URL | |
| `requiresAdmin` | bool | |
| `arguments` | string[] | Puede ser `[]` |
| `resultHints` | string | Cómo leer el resultado (cuarentena, no borrar a ciegas, etc.) |

Opcionales: `manualUrl`, `eulaUrl`, `executable` (nombre dentro del zip), `expiresDays` (MSERT: 10), `portable` (bool).

Ejemplo:

```json
{
  "schemaVersion": 1,
  "tools": [
    {
      "id": "autoruns",
      "name": "Autoruns",
      "version": "14.3",
      "architecture": "x64",
      "download": "https://download.sysinternals.com/files/Autoruns.zip",
      "sha256": "(rellenar al publicar el manifiesto)",
      "category": "startup",
      "publisher": "Microsoft",
      "licenseUrl": "https://learn.microsoft.com/sysinternals/license",
      "homeUrl": "https://learn.microsoft.com/sysinternals/downloads/autoruns",
      "requiresAdmin": true,
      "arguments": [],
      "executable": "Autoruns64.exe",
      "portable": true,
      "resultHints": "Inventario de inicio. No deshabilitar entradas de Microsoft sin identificarlas."
    }
  ]
}
```

## Flujo 4.1

1. Leer manifiesto local; opcionalmente remoto de confianza (`manifestUrl` en config).
2. Comparar con `Tools/<id>/`.
3. Si hay versión nueva: descargar a temporal, verificar sha256, sustituir.
4. Hash incorrecto: no ejecutar; log de error. Ver [seguridad](seguridad.md).
5. No recompilar BAIOS.

## Lo que no hace

- No parchea terceros ni descarga LiveCD.
- No actualiza Windows ni Defender.
- En 4.0 no comprueba versiones de portables.
