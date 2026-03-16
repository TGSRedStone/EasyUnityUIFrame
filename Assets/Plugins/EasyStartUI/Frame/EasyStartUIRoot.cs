using EasyUIFrame.Frame.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EasyUIFrame.Frame
{
    public class EasyStartUIRoot : Singleton<EasyStartUIRoot>
    {
        private EasyStartUIManager UIManager;

        protected new void Awake()
        {
            base.Awake();
            if (Instance != this)
            {
                return;
            }

            var easyStartUIManagerObject = new GameObject("EasyStartUIManager");
            easyStartUIManagerObject.transform.SetParent(transform, false);
            UIManager = easyStartUIManagerObject.AddComponent<EasyStartUIManager>();

            DontDestroyOnLoad(gameObject);
        }

        public void Configure(Canvas canvas, EventSystem eventSystem)
        {
            if (UIManager == null)
            {
                Debug.LogError("EasyStartUIManager is missing. Configure failed.");
                return;
            }

            UIManager.OnInit(canvas, eventSystem);

            if (canvas != null)
            {
                DontDestroyOnLoad(canvas.gameObject);
            }
            else
            {
                Debug.LogError("Configure failed: canvas is null.");
            }

            if (eventSystem != null)
            {
                DontDestroyOnLoad(eventSystem.gameObject);
            }
            else
            {
                Debug.LogWarning("Configure warning: eventSystem is null.");
            }
        }

        private void Update()
        {
            UIManager?.OnUpdate(Time.deltaTime);
        }

        public EasyStartUIManager GetEasyStartUIManager()
        {
            if (UIManager != null)
            {
                return UIManager;
            }

            Debug.LogError("EasyStartUIManager is not exist");
            return null;
        }
    }
}
