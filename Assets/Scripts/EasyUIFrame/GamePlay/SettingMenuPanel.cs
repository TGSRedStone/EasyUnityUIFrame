using EasyUIFrame.Frame;
using UnityEngine;
using UnityEngine.UI;

namespace EasyUIFrame.GamePlay
{
    public class SettingMenuPanel : BaseUIPanel
    {
        public Button BackBotton;
        public Button SaveButton;
        
        private static readonly string Path = "Prefab/SettingPanel";
        private static readonly string Name = "SettingPanel";
        private static UIType uiType = new UIType(Path, Name);
        public SettingMenuPanel() : base(uiType)
        {
            
        }

        public override void OnCreate()
        {
            base.OnCreate();
            BackBotton = UIHelper.GetInstance().AddOrGetComponentInChild<Button>(GO, "BackButton");
            SaveButton = UIHelper.GetInstance().AddOrGetComponentInChild<Button>(GO, "SaveButton");
            
            BackBotton.onClick.AddListener(BackMainMenuPanel);
            SaveButton.onClick.AddListener(SaveAndBackMainMenuPanel);
        }

        private void BackMainMenuPanel()
        {
            UIManager.Instance.Pop();
        }

        private void SaveAndBackMainMenuPanel()
        {
            Debug.Log("保存设置");
            UIManager.Instance.Pop();
        }
    }
}