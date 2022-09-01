using System;
using EasyUIFrame.Frame;
using UnityEngine;
using UnityEngine.UI;

namespace EasyUIFrame.GamePlay
{
    public class MainMenuPanel : BaseUIPanel
    {
        private static readonly string path = "Prefab/MainMenuPanel";
        private static readonly string name = "MainMenuPanel";
        private static UIType uiType = new UIType(path, name);

        public Button startButton;
        public Button settingButton;
        public Button exitButton;
        public Image backGround;
        
        public MainMenuPanel() : base(uiType)
        {
            
        }

        public override void OnCreate()
        {
            base.OnCreate();
            backGround.color = Color.green;
        }
    }
}