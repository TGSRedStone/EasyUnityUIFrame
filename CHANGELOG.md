# Changelog

All notable changes to this project are documented in this file.

## [0.3.0] - 2026-03-15

### Added

- `EasyUI` static facade with simple API:
  - `Open(BaseUIPanel panel)`
  - `Back()`
  - `CloseAll()`
- Auto-bootstrap for runtime objects:
  - `GameRoot`
  - `UIManager`
  - `Canvas`
  - `EventSystem`

### Changed

- Reduced user-facing framework surface by routing panel operations through `EasyUI`.
- `GameRoot` now supports lazy runtime startup via `EasyUI`.
- Updated docs to one-line usage entry.
- Multi-canvas selection is now deterministic with explicit override support (`EasyUI.SetCanvas`).
- `UIManager` stack operations and `GameRoot` manager access are now internal to runtime assembly.

## [0.2.0] - 2026-03-15

### Added

- Runtime/Samples/EditMode test assembly definitions.
- Minimal EditMode tests for `UIType` validation and `OnDestroy` compatibility.
- Sample bootstrap script `DemoStartup` for menu entry.

### Changed

- Runtime is decoupled from sample-specific `SettingDataScriptableObject`.
- `UIType` is immutable and validates constructor arguments.
- `BaseUIPanel` adds `OnDestroy()` as the canonical lifecycle method.
- `OnDestory()` remains as compatibility bridge.
- `UIManager` hardens null checks and resource load error handling.
- `UIHelper` rewritten with clean logs and null guards.
