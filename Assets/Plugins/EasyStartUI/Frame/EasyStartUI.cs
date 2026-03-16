using EasyUIFrame.Frame.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EasyUIFrame.Frame
{
    public static class EasyStartUI
    {
        private const string EasyStartUIRootName = "EasyStartUIRoot";
        private const string EasyStartUICanvasName = "EasyStartUI_Canvas";
        private const string EasyStartUIEventSystemName = "EasyStartUI_EventSystem";

        private static Canvas canvas;
        private static EventSystem eventSystem;
        private static EasyStartUIRoot easyStartUIRoot;

        public static void Open(BaseUIPanel panel)
        {
            EnsureRuntimeObjects();
            var uiManager = easyStartUIRoot?.GetEasyStartUIManager();
            if (uiManager == null)
            {
                Debug.LogError("EasyStartUIManager is not ready. Open request ignored.");
                return;
            }

            uiManager.Push(panel);
        }

        public static void Back()
        {
            EnsureRuntimeObjects();
            var uiManager = easyStartUIRoot?.GetEasyStartUIManager();
            if (uiManager == null)
            {
                Debug.LogError("EasyStartUIManager is not ready. Back request ignored.");
                return;
            }

            uiManager.Pop();
        }

        public static void CloseAll()
        {
            EnsureRuntimeObjects();
            var uiManager = easyStartUIRoot?.GetEasyStartUIManager();
            if (uiManager == null)
            {
                Debug.LogError("EasyStartUIManager is not ready. CloseAll request ignored.");
                return;
            }

            uiManager.PopAll();
        }

        private static void EnsureRuntimeObjects()
        {
            var ensuredCanvas = EnsureCanvas();
            var ensuredEventSystem = EnsureEventSystem();

            var root = EasyStartUIRoot.Instance;
            if (root == null)
            {
                var easyStartUIRootObject = new GameObject(EasyStartUIRootName);
                easyStartUIRoot = easyStartUIRootObject.AddComponent<EasyStartUIRoot>();
                easyStartUIRoot.Configure(ensuredCanvas, ensuredEventSystem);
                return;
            }

            easyStartUIRoot = root;
        }

        private static Canvas EnsureCanvas()
        {
            if (canvas != null)
            {
                return canvas;
            }

            var existingCanvasObject = GameObject.Find(EasyStartUICanvasName);
            if (existingCanvasObject != null && existingCanvasObject.TryGetComponent(out Canvas namedCanvas))
            {
                canvas = namedCanvas;
                return canvas;
            }

            var existingCanvas = Object.FindObjectOfType<Canvas>();
            if (existingCanvas != null)
            {
                canvas = existingCanvas;
                Debug.LogWarning($"Reusing existing Canvas '{canvas.name}'.");
                return canvas;
            }

            var canvasObject = new GameObject(EasyStartUICanvasName);
            canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            return canvas;
        }

        private static EventSystem EnsureEventSystem()
        {
            if (eventSystem != null)
            {
                return eventSystem;
            }

            var existingEventSystemObject = GameObject.Find(EasyStartUIEventSystemName);
            if (existingEventSystemObject != null && existingEventSystemObject.TryGetComponent(out EventSystem namedEventSystem))
            {
                eventSystem = namedEventSystem;
                return eventSystem;
            }

            var existingEventSystem = Object.FindObjectOfType<EventSystem>();
            if (existingEventSystem != null)
            {
                eventSystem = existingEventSystem;
                Debug.LogWarning($"Reusing existing EventSystem '{eventSystem.name}'.");
                return eventSystem;
            }

            var eventSystemObject = new GameObject(EasyStartUIEventSystemName);
            eventSystem = eventSystemObject.AddComponent<EventSystem>();
            eventSystemObject.AddComponent<StandaloneInputModule>();
            return eventSystem;
        }
    }
}
