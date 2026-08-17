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

Opcionales: `manualUrl`, `eulaUrl`, `executable` (nombre dentro del zip), `expiresDays` (MSERT: 10), `portable` (bool; `false` = instalador, confirmación extra), `falsePositiveWarning` (HijackThis), `trialDays` (HitmanPro), `icon` (archivo local en `Tools/<id>/`, p. ej. `icon.png`).

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

1. Leer manifiesto local (`manifest.json` junto al exe). Si `manifestUrl` es https, se puede refrescar la copia local (F4-02).
2. Comparar con `Tools/<id>/` (`installed.json` + versión).
3. Si hay versión nueva: descargar a temporal, verificar sha256, sustituir el directorio.
4. Hash incorrecto: no instalar, no ejecutar; línea en `Tools/integrity.log`.
5. No recompilar BAIOS.

**F4-01:** carga del manifiesto y carpetas `Tools/<id>/`.  
**F4-02:** descarga https, sha256, versionado y reemplazo. Sin sha256 en la ficha no hay descarga (se puede seguir colocando el binario a mano si el hash no está declarado).  
**F4-03:** cada intento de lanzamiento (éxito, UAC cancelado, hash bloqueado, binario ausente, cancelación en UI) se registra en `Tools/<id>/execution.log`. No se parsea stdout de terceros. Distinto de `Tools/integrity.log` (hashes).

El `sha256` es del **zip** si `download` termina en `.zip`; si no, del archivo a ejecutar.

## Flujo 4.2 (motor)

`engine.json` junto al exe: `version`, `download` (https), `sha256`. `engineUrl` en `config.json` refresca esa ficha (mismo patrón que el manifiesto).

Sin sha256 de 64 hex no hay descarga. Zip verificado → `update-pending/` → al **siguiente arranque** se sustituyen los archivos. Hash incorrecto: no aplicar; línea en `logs/app.log`.

Publicar: `scripts/publish.ps1` (`dist/BAIOS/` y `dist/BAIOS.Technician/`). Instalar sin admin: `scripts/install.ps1` → `%LOCALAPPDATA%\BAIOS`.

## Lo que no hace

- No parchea terceros ni descarga LiveCD.
- No actualiza Windows ni Defender.
- En 4.0 no comprueba versiones de portables. En 4.1 (F4-02) sí, contra el manifiesto. En 4.2 el motor se actualiza con `engine.json` (F5-01).
