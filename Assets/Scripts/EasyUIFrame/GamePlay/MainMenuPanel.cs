using EasyUIFrame.Frame;
using UnityEditor;
using UnityEngine.UI;

namespace EasyUIFrame.GamePlay
{
    public class MainMenuPanel : BaseUIPanel
    {
        public Button StartButton;
        public Button SettingButton;
        public Button ExitButton;
        public Image BackGround;
        
        private static readonly string Path = "Prefab/MainMenuPanel";
        private static readonly string Name = "MainMenuPanel";
        private static UIType uiType = new UIType(Path, Name);
        
        public MainMenuPanel() : base(uiType)
        {
            
        }

        public override void OnCreate()
        {
            base.OnCreate();
            BackGround = UIHelper.GetInstance().AddOrGetComponentInChild<Image>(GO, "BackGround");
            StartButton = UIHelper.GetInstance().AddOrGetComponentInChild<Button>(GO, "StartButton");
            SettingButton = UIHelper.GetInstance().AddOrGetComponentInChild<Button>(GO, "SettingButton");
            ExitButton = UIHelper.GetInstance().AddOrGetComponentInChild<Button>(GO, "ExitButton");
            
            SettingButton.onClick.AddListener(OpenSettingPanel);
            ExitButton.onClick.AddListener(ExitMainMenuPanel);
        }

        private void OpenSettingPanel()
        {
            UIManager.Instance.Push(new SettingMenuPanel());
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