using EasyUIFrame.Frame.UI;
using EasyUIFrame.GamePlay.GameScene;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EasyUIFrame.GamePlay.UI.UIPanel
{
    public class GameUIPanel : BaseUIPanel
    {
        private static readonly string path = "Prefab/GameUIPanel";
        private static readonly string name = "GameUIPanel";
        private static readonly UIType uiType = new UIType(path, name);

        private TMP_Text text;
        private Button exitButton;
        private Button backMainMenuButton;

        public GameUIPanel() : base(uiType, false)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            text = UIHelper.AddOrGetComponentInChild<TMP_Text>(GO, "SpeedValue");
            exitButton = UIHelper.AddOrGetComponentInChild<Button>(GO, "ExitButton");
            backMainMenuButton = UIHelper.AddOrGetComponentInChild<Button>(GO, "BackMainMenuButton");

            if (exitButton == null || backMainMenuButton == null)
            {
                Debug.LogError("GameUIPanel init failed: required buttons are missing.");
                return;
            }

            if (text == null)
            {
                Debug.LogWarning("GameUIPanel: SpeedValue text not found, speed display will be skipped.");
            }

            exitButton.onClick.AddListener(ExitMainMenuPanel);
            backMainMenuButton.onClick.AddListener(BackMainMenuPanel);
        }

        private void BackMainMenuPanel()
        {
            PopAll();
            Push(new MainMenuPanel());
            SceneManager.LoadScene("MainScene");
        }

        private void ExitMainMenuPanel()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public override void OnUpdate(float deltaTime)
        {
            if (text != null)
            {
                text.text = PlayerControl.PlayerSpeed.ToString();
            }
        }
    }
}
