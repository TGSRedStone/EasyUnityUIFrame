using EasyUIFrame.Frame;
using EasyUIFrame.GamePlay.UI.UIPanel;
using UnityEngine;

namespace EasyUIFrame.GamePlay.UI
{
    [DefaultExecutionOrder(1000)]
    public class DemoStartup : MonoBehaviour
    {
        private void Start()
        {
            EasyStartUI.Open(new MainMenuPanel());
            Destroy(this);
        }
    }
}
