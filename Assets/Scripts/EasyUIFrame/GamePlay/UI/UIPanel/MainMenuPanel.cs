using EasyUIFrame.Frame;
using EasyUIFrame.Frame.UI;
using UnityEditor;
using UnityEngine.UI;

namespace EasyUIFrame.GamePlay.UI.UIPanel
{
    public class MainMenuPanel : BaseUIPanel
    {
        private Button startButton;
        private Button settingButton;
        private Button exitButton;
        private Image backGround;
        
        private static readonly string Path = "Prefab/MainMenuPanel";
        private static readonly string Name = "MainMenuPanel";
        private static UIType uiType = new UIType(Path, Name);
        
        public MainMenuPanel() : base(uiType)
        {
            
        }

        public override void OnCreate()
        {
            base.OnCreate();
            backGround = UIHelper.GetInstance().AddOrGetComponentInChild<Image>(GO, "BackGround");
            startButton = UIHelper.GetInstance().AddOrGetComponentInChild<Button>(GO, "StartButton");
            settingButton = UIHelper.GetInstance().AddOrGetComponentInChild<Button>(GO, "SettingButton");
            exitButton = UIHelper.GetInstance().AddOrGetComponentInChild<Button>(GO, "ExitButton");
            
            settingButton.onClick.AddListener(OpenSettingPanel);
            exitButton.onClick.AddListener(ExitMainMenuPanel);
        }

        private void OpenSettingPanel()
        {
            Push(new SettingMenuPanel());
        }

        private void ExitMainMenuPanel()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();      
#endif
        }
        
    }
}