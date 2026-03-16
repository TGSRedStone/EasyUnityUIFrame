# EasyStartUI 插件使用手册

## 1. 插件核心约定

EasyStartUI 是一个基于 `Stack` 的轻量 UI 框架，核心约定如下：

1. 每个 UI 面板必须对应一个 Prefab。
2. Prefab 放在 `Resources/Prefab/` 下，运行时通过 `Resources.Load` 加载。
3. 每个面板都需要手动编写一个继承 `BaseUIPanel` 的 C# 类。
4. 面板类通过 `UIType(path, name)` 声明资源路径和唯一名称。


## 2. 当前目录结构

- `Assets/Plugins/EasyStartUI/Frame/`
  - 框架核心：`EasyStartUI`、`EasyStartUIRoot`、`EasyStartUIManager`、`BaseUIPanel`、`UIHelper`、`UIType`
- `Assets/Plugins/EasyStartUI/Resources/Prefab/`
  - 面板 Prefab：`MainMenuPanel.prefab`、`SettingPanel.prefab`、`GameUIPanel.prefab`
- `Assets/Plugins/EasyStartUI/GamePlay/UI/UIPanel/`
  - 手写面板类：`MainMenuPanel.cs`、`SettingMenuPanel.cs`、`GameUIPanel.cs`
- `Assets/Plugins/EasyStartUI/GamePlay/UI/DemoStartup.cs`
  - 示例启动脚本，在 `RootScene` 里打开主菜单


## 3. 框架工作流程

1. 业务调用 `EasyStartUI.Open(new XxxPanel())`。
2. `EasyStartUI` 自动确保 `Canvas`、`EventSystem`、`EasyStartUIRoot` 存在。
3. `EasyStartUIManager.Push` 根据 `UIType.Path` 从 `Resources` 实例化 Prefab。
4. 首次打开会调用 `OnCreate`；再次打开同名面板会调用 `OnRefresh`。
5. `Pop/PopAll` 负责关闭并销毁面板对象。


## 4. 面板生命周期说明

`BaseUIPanel` 主要生命周期：

- `OnCreate()`：首次创建时调用，用于缓存控件、注册按钮事件
- `OnEnable()`：重新显示时调用
- `OnDisable()`：被新面板遮挡或关闭前调用
- `OnDestroy()`：关闭并销毁时调用
- `OnRefresh(BaseUIPanel panel)`：重复打开同名面板时调用
- `OnUpdate(float dt)`：每帧更新，由 `EasyStartUIRoot.Update` 驱动

默认通过 `CanvasGroup` 控制交互与显示（`alpha/interactable/blocksRaycasts`）。


## 5. 现有 3 个 UIPanel 示例（手写 + Prefab 对应关系）

### 5.1 MainMenuPanel

- 脚本：`GamePlay/UI/UIPanel/MainMenuPanel.cs`
- Prefab：`Resources/Prefab/MainMenuPanel.prefab`
- `UIType.Path`：`Prefab/MainMenuPanel`
- `UIType.Name`：`MainMenuPanel`
- 依赖子节点名：`StartButton`、`SettingButton`、`ExitButton`、`BackGround`

### 5.2 SettingMenuPanel

- 脚本：`GamePlay/UI/UIPanel/SettingMenuPanel.cs`
- Prefab：`Resources/Prefab/SettingPanel.prefab`
- `UIType.Path`：`Prefab/SettingPanel`
- `UIType.Name`：`SettingPanel`
- 依赖子节点名：
  - `BackButton`、`SaveButton`
  - `SensitivityCount`、`MusicVolumeCount`、`FOVCount`、`SFXVolumeCount`
  - `SensitivitySlider`、`MusicVolumeSlider`、`FOVSlider`、`SFXVolumeSlider`
- 额外资源：`Resources/ScriptObjects/SettingData.asset`

### 5.3 GameUIPanel

- 脚本：`GamePlay/UI/UIPanel/GameUIPanel.cs`
- Prefab：`Resources/Prefab/GameUIPanel.prefab`
- `UIType.Path`：`Prefab/GameUIPanel`
- `UIType.Name`：`GameUIPanel`
- 依赖子节点名：`SpeedValue`、`ExitButton`、`BackMainMenuButton`


## 6. 如何新增一个 UIPanel（标准流程）

1. 在 `Resources/Prefab/` 创建新 Prefab。  
   示例：`InventoryPanel.prefab`
2. 在 `GamePlay/UI/UIPanel/` 新建脚本 `InventoryPanel.cs`，继承 `BaseUIPanel`。
3. 在脚本中声明 `UIType`：
   - `path` 是 `Resources` 相对路径，不带扩展名，例如 `Prefab/InventoryPanel`
   - `name` 全局唯一，例如 `InventoryPanel`
4. 在 `OnCreate` 使用 `UIHelper.AddOrGetComponentInChild<T>(GO, "节点名")` 绑定控件。
5. 通过 `EasyStartUI.Open(new InventoryPanel())` 打开，`EasyStartUI.Back()` 返回。

参考模板：

```csharp
using EasyUIFrame.Frame;
using EasyUIFrame.Frame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace EasyUIFrame.GamePlay.UI.UIPanel
{
    public class InventoryPanel : BaseUIPanel
    {
        private static readonly string path = "Prefab/InventoryPanel";
        private static readonly string name = "InventoryPanel";
        private static readonly UIType uiType = new UIType(path, name);

        private Button closeButton;

        public InventoryPanel() : base(uiType, false)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            closeButton = UIHelper.AddOrGetComponentInChild<Button>(GO, "CloseButton");
            if (closeButton == null)
            {
                Debug.LogError("InventoryPanel init failed: CloseButton not found.");
                return;
            }

            closeButton.onClick.AddListener(() => Pop());
        }
    }
}
```


## 7. 使用注意事项

1. `UIType.Name` 用作管理器字典 key，不能重复，否则会与已有面板冲突。
2. `UIHelper.AddOrGetComponentInChild` 依赖字符串查找子节点，Prefab 中节点名必须与代码一致。
3. `UIType.Path` 必须可被 `Resources.Load<GameObject>(path)` 找到。
4. 面板切换默认会禁用上一个面板（`keepActive = false`）。如果希望多个面板同时可交互，构造函数传 `true`。


## 8. 场景与启动建议

- 当前示例中 `RootScene` 挂载了 `DemoStartup`，会在 `Start` 打开 `MainMenuPanel`。
- `MainScene` 和 `GameScene` 不依赖场景内固定 Canvas，框架会自动创建 Canvas/EventSystem。
- 推荐将项目入口设置为 `RootScene`，并在此处统一做 UI 首屏打开。


## 9. 常见问题

1. 面板打开失败并提示 `UI prefab not found`：检查 `UIType.Path` 与 Prefab 路径是否匹配。
2. 按钮点击无效：检查是否存在 `EventSystem`、以及面板是否被 `CanvasGroup` 设为不可交互。
3. 找不到控件并报 `required ... are missing`：检查 Prefab 子节点名称是否与代码完全一致。

