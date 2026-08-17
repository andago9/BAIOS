# Manual de usuario — BAIOS 4

BAIOS (*Blinter All In One Security*) orquesta diagnóstico, seguridad y mantenimiento en Windows. **No es un antivirus**: no sustituye Microsoft Defender ni otros motores.

Licencia del motor: [GNU GPL v3](../LICENSE). Las herramientas de terceros conservan la suya.

## Requisitos

- Windows x64.
- Para **compilar**: [SDK .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0).
- El paquete self-contained **no** pide instalar .NET en el PC de destino.

## Instalar o copiar

Hay dos formas; el código es el mismo.

### Carpeta portable

1. En el repo: `powershell -File scripts/publish.ps1`
2. Copia `dist/BAIOS/` donde quieras (disco fijo o USB).
3. Ejecuta `BAIOS.exe`.

### Perfil USB / Technician Edition

1. El mismo script genera `dist/BAIOS.Technician/` (`modeDefault` = Technician).
2. Cópiala a un USB y ejecuta desde ahí.
3. Si Windows ve la unidad como extraíble, BAIOS **preselecciona Técnico** aunque el json diga Home.

### Instalación en el perfil de usuario (sin administrador)

```text
powershell -File scripts/publish.ps1
powershell -File scripts/install.ps1
```

Copia a `%LOCALAPPDATA%\BAIOS` y crea un acceso directo en el menú Inicio.

## Arranque

Bienvenida → acuerdo de uso (si no aceptas, no entra al motor) → Hogar o Técnico → shell.

Pantallas: Inicio, Seguridad, Diagnóstico, Mantenimiento, Red, Herramientas, Reportes. **Cambiar modo** no cierra la sesión.

Al salir aparece un diálogo corto de créditos. **Acerca de** (en el shell) muestra la GPL, atribución (Andago, velosergio) y el log de la aplicación.

## Modos

| Modo | Para quién | Qué cambia |
| --- | --- | --- |
| Hogar | Uso doméstico | Menos detalle; núcleo de herramientas (AdwCleaner, MSERT, Autoruns) |
| Técnico | Técnico de sistemas | Tablas, diagnóstico completo, todo el manifiesto, Rescue, actualizar motor |

## Herramientas

Coloca los binarios en `Tools/<id>/` junto al exe. Si la ficha del `manifest.json` tiene URL https y `sha256` de 64 hex, usa **Instalar / Actualizar**. Un hash que no coincida **impide** instalar y ejecutar.

Cada lanzamiento deja una línea en `Tools/<id>/execution.log`. En Técnico: **Ver log** y **Abrir carpeta**.

HijackThis pide confirmación: los hallazgos suelen ser falsos positivos; no deshabilites ni borres a ciegas. Los instaladores (Spybot, Malwarebytes) piden consentimiento extra.

Un `Tools/<id>/icon.png` opcional (o el nombre en el campo `icon` del manifiesto) se muestra en la ficha. BAIOS no embebe logos de fabricantes.

## Rescue / LiveCD

Solo en Técnico: enlaces a páginas **oficiales**. BAIOS **nunca** descarga ni embebe ISO. ESET SysRescue Live y otros productos EOL no se ofrecen como descarga.

## Actualizar el motor

`engine.json` junto al exe declara versión, URL y sha256. En Técnico, **Acerca de → Actualizar motor**. Sin sha256 no hay descarga (igual que las herramientas). Tras una descarga verificada, **cierra y vuelve a abrir** BAIOS para aplicar el recambio.

`engineUrl` en `config.json` (https) refresca el `engine.json` remoto.

## Logs

| Archivo | Qué |
| --- | --- |
| `logs/app.log` | Arranque, modo, errores, actualizaciones del motor |
| `Tools/integrity.log` | Hashes de herramientas |
| `Tools/<id>/execution.log` | Cada intento de lanzar esa herramienta |

## Informe

En Reportes se guarda HTML + TXT de la sesión (hallazgos OK / aviso / fallo). En Hogar se pide consentimiento porque el informe incluye el nombre del equipo.

## Contacto

blinter.baios@gmail.com · [Issues](https://github.com/andago9/BAIOS/issues)
