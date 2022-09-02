using EasyUIFrame.Frame;
using EasyUIFrame.Frame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace EasyUIFrame.GamePlay.UI.UIPanel
{
    public class SettingMenuPanel : BaseUIPanel
    {
        private Button backBotton;
        private Button saveButton;
        
        private static readonly string Path = "Prefab/SettingPanel";
        private static readonly string Name = "SettingPanel";
        private static UIType uiType = new UIType(Path, Name);
        public SettingMenuPanel() : base(uiType)
        {
            
        }

        public override void OnCreate()
        {
            base.OnCreate();
            backBotton = UIHelper.GetInstance().AddOrGetComponentInChild<Button>(GO, "BackButton");
            saveButton = UIHelper.GetInstance().AddOrGetComponentInChild<Button>(GO, "SaveButton");
            
            backBotton.onClick.AddListener(BackMainMenuPanel);
            saveButton.onClick.AddListener(SaveAndBackMainMenuPanel);
        }

        private void BackMainMenuPanel()
        {
            Pop();
        }

        private void SaveAndBackMainMenuPanel()
        {
            Debug.Log("保存设置");
            Pop();
        }
    }
}