using System.Collections.Generic;
using EasyUIFrame.Frame.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EasyUIFrame.Frame
{
    public class EasyStartUIManager : MonoBehaviour
    {
        private Canvas canvas;
        private EventSystem eventSystem;

        private readonly Stack<BaseUIPanel> uiStack = new Stack<BaseUIPanel>();
        private readonly Dictionary<string, BaseUIPanel> uiObjectsDict = new Dictionary<string, BaseUIPanel>();
        private readonly List<BaseUIPanel> updatePanels = new List<BaseUIPanel>();

        public void OnInit(Canvas canvas, EventSystem eventSystem)
        {
            uiStack.Clear();
            uiObjectsDict.Clear();

            if (canvas != null)
            {
                this.canvas = canvas;
            }
            else
            {
                Debug.LogError("UIManager init failed because no Canvas was found.");
            }

            if (eventSystem != null)
            {
                this.eventSystem = eventSystem;
            }
            else
            {
                Debug.LogWarning("UIManager init warning: no EventSystem found.");
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (uiObjectsDict.Count == 0)
            {
                return;
            }

            updatePanels.Clear();
            updatePanels.AddRange(uiObjectsDict.Values);

            for (var i = 0; i < updatePanels.Count; i++)
            {
                var panel = updatePanels[i];
                if (panel == null)
                {
                    continue;
                }

                panel.OnUpdate(deltaTime);
            }
        }

        private Transform LoadGameObject(UIType uiType)
        {
            if (canvas == null)
            {
                Debug.LogError($"Canvas is missing. Cannot load panel: {uiType.Name}");
                return null;
            }

            var prefab = Resources.Load<GameObject>(uiType.Path);
            if (prefab == null)
            {
                Debug.LogError($"UI prefab not found at Resources/{uiType.Path}");
                return null;
            }

            return Instantiate(prefab, canvas.transform).transform;
        }

        internal void Push(BaseUIPanel baseUIPanel)
        {
            if (baseUIPanel == null || baseUIPanel.UIType == null)
            {
                Debug.LogError("Push failed: panel or panel type is null.");
                return;
            }

            var key = baseUIPanel.UIType.Name;
            var currentTop = uiStack.Count > 0 ? uiStack.Peek() : null;
            if (currentTop != null && currentTop.UIType != null && currentTop.UIType.Name == key)
            {
                currentTop.OnRefresh(baseUIPanel);
                currentTop.OnEnable();
                return;
            }

            var isNewPanel = false;
            if (!uiObjectsDict.TryGetValue(key, out var targetPanel) || targetPanel == null)
            {
                var pushObj = LoadGameObject(baseUIPanel.UIType);
                if (pushObj == null)
                {
                    return;
                }

                baseUIPanel.GO = pushObj;
                baseUIPanel.OnCreate();
                uiObjectsDict[key] = baseUIPanel;
                targetPanel = baseUIPanel;
                isNewPanel = true;
            }
            else
            {
                targetPanel.OnRefresh(baseUIPanel);
            }

            RemovePanelFromStack(targetPanel);

            if (uiStack.Count > 0 && !targetPanel.KeepActive)
            {
                uiStack.Peek().OnDisable();
            }

            if (!isNewPanel)
            {
                targetPanel.OnEnable();
            }

            uiStack.Push(targetPanel);
        }

        private void RemovePanelFromStack(BaseUIPanel panel)
        {
            if (panel == null || uiStack.Count == 0 || !uiStack.Contains(panel))
            {
                return;
            }

            var tempStack = new Stack<BaseUIPanel>();
            while (uiStack.Count > 0)
            {
                var top = uiStack.Pop();
                if (!ReferenceEquals(top, panel))
                {
                    tempStack.Push(top);
                }
            }

            while (tempStack.Count > 0)
            {
                uiStack.Push(tempStack.Pop());
            }
        }

        internal void Pop()
        {
            if (uiStack.Count == 0)
            {
                return;
            }

            var top = uiStack.Pop();
            if (top != null)
            {
                top.OnDisable();
                top.OnDestroy();

                var key = top.UIType.Name;
                if (uiObjectsDict.TryGetValue(key, out var panelInDict))
                {
                    if (panelInDict != null && panelInDict.GO != null)
                    {
                        Destroy(panelInDict.GO.gameObject);
                    }

                    uiObjectsDict.Remove(key);
                }
            }

            if (uiStack.Count > 0)
            {
                uiStack.Peek().OnEnable();
            }
        }

        internal void PopAll()
        {
            while (uiStack.Count > 0)
            {
                Pop();
            }
        }

    }
}
