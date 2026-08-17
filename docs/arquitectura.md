# Arquitectura de BAIOS 4

Esqueleto. Cerrar decisiones en [F2](backlog.md). Producto: [visión](vision.md).

## Stack (recomendación, no cerrada)

| Pieza | Recomendación | Alternativa |
| --- | --- | --- |
| Runtime | .NET 8 (Windows) | .NET 9 cuando sea LTS del equipo |
| UI | WPF | WinUI 3 (Fluent; más peso para USB) |
| Distribución 4.0 | Self-contained, unpackaged | MSIX más adelante (4.2) |
| Lenguaje | C# | — |
| Licencia del motor | GNU GPL v3 | Las herramientas de terceros conservan la suya |

WPF self-contained encaja con USB/portable sin Windows App SDK. WinUI 3 se documenta aquí para no reabrir el debate en F3 sin motivo.

## Estructura de solución (objetivo)

```
BAIOS
├── BAIOS.exe
├── Core
│   ├── Security
│   ├── Diagnostics
│   ├── Maintenance
│   └── Networking
├── Modules
│   ├── Defender
│   ├── Adware
│   ├── Disk
│   ├── Network
│   └── System
├── Tools
│   ├── AdwCleaner
│   ├── Autoruns
│   └── ...
├── Reports
└── Config
```

Un ejecutable; el resto son librerías por dominio. No un único `.cs` de miles de líneas. El prototipo `test2` (WinForms 4.7.2) **no** es esta estructura.

## Comunicación

- **Motor → SO:** WMI/CIM, APIs de Windows, `MpCmdRun` / Defender, `ipconfig`/`ping`/`tracert` encapsulados, DISM/SFC/CHKDSK con confirmación.
- **Motor → herramientas:** proceso hijo según ficha (ruta, args, admin). En 4.0 solo lanzar. En 4.1, manifiesto + sha256.
- **Motor → reportes:** modelo de hallazgos (OK / aviso / fallo) que la UI y el HTML consumen. Ver [reportes](reportes.md).
- **Modos Hogar / Técnico:** misma API del motor; la UI cambia densidad y el flujo guiado.

## Permisos

Ver [seguridad](seguridad.md). Principio: elevar solo lo que lo necesita (Defender, DISM, SFC, CHKDSK, algunas portables). El dashboard de lectura puede correr sin admin y marcar qué falta.

## Config

- Modo por defecto (Hogar / Técnico).
- Ruta de `Tools/` y de `Reports/`.
- En 4.1: URL o copia local del manifiesto.

## Fuera de este documento (hasta F2-01 / F2-02)

Diagrama de proyectos `.csproj`, convención de namespaces, y si el perfil USB es un publish profile o un flag de config.
