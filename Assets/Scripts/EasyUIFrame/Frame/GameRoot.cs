using EasyUIFrame.Frame.UI;
using EasyUIFrame.GamePlay.UI;
using EasyUIFrame.GamePlay.UI.UIPanel;

namespace EasyUIFrame.Frame
{
    public class GameRoot : Singleton<GameRoot>
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
            UIManager.Instance.OnInit();
            UIManager.Instance.Push(new MainMenuPanel());
        }
    }
}