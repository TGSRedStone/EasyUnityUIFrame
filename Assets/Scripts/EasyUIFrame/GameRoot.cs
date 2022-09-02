using EasyUIFrame.Frame;
using EasyUIFrame.GamePlay;

namespace EasyUIFrame
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