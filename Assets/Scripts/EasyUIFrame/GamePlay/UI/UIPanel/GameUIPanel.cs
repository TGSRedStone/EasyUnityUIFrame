using System.Collections;
using System.Collections.Generic;
using EasyUIFrame.Frame;
using EasyUIFrame.Frame.UI;
using EasyUIFrame.GamePlay.GameScene;
using EasyUIFrame.GamePlay.UI.UIPanel;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIPanel : BaseUIPanel
{
    private static readonly string path = "Prefab/GameUIPanel";
    private static readonly string name = "GameUIPanel";
    private static UIType uiType = new UIType(path, name);

    private TMP_Text text;
    private Button ExitButton;
    private Button BackMainMenuButton;
    
    public GameUIPanel() : base(uiType, false)
    {
        
    }

    public override void OnCreate()
    {
        base.OnCreate();
        text = UIHelper.GetInstance().AddOrGetComponentInChild<TMP_Text>(GO, "SpeedValue");
        ExitButton = UIHelper.GetInstance().AddOrGetComponentInChild<Button>(GO, "ExitButton");
        BackMainMenuButton = UIHelper.GetInstance().AddOrGetComponentInChild<Button>(GO, "BackMainMenuButton");
        
        ExitButton.onClick.AddListener(ExitMainMenuPanel);
        BackMainMenuButton.onClick.AddListener(BackMainMenuPanel);
    }

    private void BackMainMenuPanel()
    {
        PopAll();
        Push(new MainMenuPanel());
        SceneManager.UnloadSceneAsync("GameScene");
    }

    private void ExitMainMenuPanel()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();      
#endif
    }

    public override void OnUpdate(float deltaTime)
    {
        text.text = PlayerControl.PlayerSpeed.ToString();
    }
}
