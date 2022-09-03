using EasyUIFrame.Frame.UI;
using EasyUIFrame.GamePlay.UI;
using EasyUIFrame.GamePlay.UI.UIPanel;
using UnityEngine;

namespace EasyUIFrame.Frame
{
    public class GameRoot : Singleton<GameRoot>
    {
        public ScriptableObject SettingData { get; private set; }

        private void Start()
        {
            DontDestroyOnLoad(this);
            UIManager.Instance.OnInit();
            UIManager.Instance.Push(new MainMenuPanel());
        }
    }
}