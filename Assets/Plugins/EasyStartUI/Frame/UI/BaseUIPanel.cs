using UnityEngine;

namespace EasyUIFrame.Frame.UI
{
    public abstract class BaseUIPanel
    {
        public UIType UIType { get; }
        public Transform GO { get; internal set; }
        public bool KeepActive { get; }

        protected BaseUIPanel(UIType uiType, bool keepActive = false)
        {
            UIType = uiType;
            KeepActive = keepActive;
        }

        public virtual void OnCreate()
        {
            SetPanelInteractable(true);
        }

        public virtual void OnEnable()
        {
            SetPanelInteractable(true);
        }

        public virtual void OnDisable()
        {
            SetPanelInteractable(false);
        }

        public virtual void OnDestroy()
        {
            SetPanelInteractable(false);
        }

        public virtual void OnRefresh(BaseUIPanel baseUIPanel)
        {
        }

        public virtual void OnUpdate(float deltaTime)
        {
        }

        private void SetPanelInteractable(bool interactable)
        {
            if (GO == null)
            {
                Debug.LogError($"Panel transform is null for '{UIType?.Name}'.");
                return;
            }

            var canvasGroup = UIHelper.AddOrGetComponent<CanvasGroup>(GO, true);
            if (canvasGroup != null)
            {
                canvasGroup.interactable = interactable;
                canvasGroup.blocksRaycasts = interactable;
                canvasGroup.alpha = interactable ? 1f : 0f;
            }
        }

        protected void Push(BaseUIPanel baseUIPanel)
        {
            EasyStartUI.Open(baseUIPanel);
        }

        protected void Pop()
        {
            EasyStartUI.Back();
        }

        protected void PopAll()
        {
            EasyStartUI.CloseAll();
        }
    }
}
