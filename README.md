# EasyUnityUIFrame

A lightweight stack-based UI framework for Unity, designed for quick prototyping and GameJam workflows.

## Why this plugin is fast to use

- One static entry point: `EasyUI`
- Auto-bootstrap runtime objects when needed (`GameRoot`, `UIManager`, `Canvas`, `EventSystem`)
- Stack-based panel flow with minimal API

## Quick Start (30 seconds)

1. Create a panel class deriving from `BaseUIPanel`.
2. Put your panel prefab under `Assets/Resources/Prefab/`.
3. Open panel with one line:

```csharp
EasyUI.Open(new MainMenuPanel());
```

No manual scene setup is required for first use.

## Minimal Panel Example

```csharp
using EasyUIFrame.Frame;
using EasyUIFrame.Frame.UI;

public class MainMenuPanel : BaseUIPanel
{
    private static readonly UIType uiType = new UIType("Prefab/MainMenuPanel", "MainMenuPanel");

    public MainMenuPanel() : base(uiType) {}
}
```

## Runtime API

- `EasyUI.Open(BaseUIPanel panel)`
- `EasyUI.Back()`
- `EasyUI.CloseAll()`
- `EasyUI.SetCanvas(Canvas canvas)` (explicit canvas binding in multi-canvas scenes)
- `EasyUI.SetEventSystem(EventSystem eventSystem)` (explicit event system binding)

## Folder Layout

- `Assets/Scripts/EasyUIFrame/Frame`: runtime core (`EasyUIFrame.Runtime`)
- `Assets/Scripts/EasyUIFrame/GamePlay`: sample content (`EasyUIFrame.Samples`)
- `Assets/Tests/EditMode`: minimal edit mode tests

## Compatibility Note

- `BaseUIPanel.OnDestory()` is preserved for backward compatibility.
- New code should implement `OnDestroy()`.
- `GameRoot` and `UIManager` are runtime internals; use `EasyUI` as the public API.

## License

See [LICENSE](LICENSE).
