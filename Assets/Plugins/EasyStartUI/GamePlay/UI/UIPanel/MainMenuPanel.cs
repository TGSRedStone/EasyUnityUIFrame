using EasyUIFrame.Frame;
using EasyUIFrame.Frame.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EasyUIFrame.GamePlay.UI.UIPanel
{
    public class MainMenuPanel : BaseUIPanel
    {
        private Button startButton;
        private Button settingButton;
        private Button exitButton;
        private Image backGround;

        private static readonly string path = "Prefab/MainMenuPanel";
        private static readonly string name = "MainMenuPanel";
        private static readonly UIType uiType = new UIType(path, name);

        public MainMenuPanel() : base(uiType, false)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            backGround = UIHelper.AddOrGetComponentInChild<Image>(GO, "BackGround");
            startButton = UIHelper.AddOrGetComponentInChild<Button>(GO, "StartButton");
            settingButton = UIHelper.AddOrGetComponentInChild<Button>(GO, "SettingButton");
            exitButton = UIHelper.AddOrGetComponentInChild<Button>(GO, "ExitButton");

            if (startButton == null || settingButton == null || exitButton == null)
            {
                Debug.LogError("MainMenuPanel init failed: required buttons are missing.");
                return;
            }

            startButton.onClick.AddListener(StartGame);
            settingButton.onClick.AddListener(OpenSettingPanel);
            exitButton.onClick.AddListener(ExitMainMenuPanel);
        }

        private void StartGame()
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            EasyStartUI.CloseAll();
            EasyStartUI.Open(new GameUIPanel());
        }

        private void OpenSettingPanel()
        {
            Push(new SettingMenuPanel());
        }

        private void ExitMainMenuPanel()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
