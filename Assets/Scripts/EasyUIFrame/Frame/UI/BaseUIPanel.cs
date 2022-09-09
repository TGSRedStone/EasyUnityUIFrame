using UnityEngine;

namespace EasyUIFrame.Frame.UI
{
    public abstract class BaseUIPanel
    {
        public UIType UIType;

        public Transform GO;

        public bool KeepActive = false;

        protected BaseUIPanel(UIType uiType, bool keepActive = false)
        {
            this.UIType = uiType;
            this.KeepActive = keepActive;
        }

        /// <summary>
        ///创建UI时调用
        /// </summary>
        public virtual void OnCreate()
        {
            UIHelper.GetInstance().AddOrGetComponent<CanvasGroup>(GO).interactable = true;
        }

        /// <summary>
        /// 打开UI时调用
        /// </summary>
        public virtual void OnEnable()
        {
            UIHelper.GetInstance().AddOrGetComponent<CanvasGroup>(GO).interactable = true;
        }

        /// <summary>
        /// 关闭UI时调用
        /// </summary>
        public virtual void OnDisable()
        {
            UIHelper.GetInstance().AddOrGetComponent<CanvasGroup>(GO).interactable = false;
        }

        /// <summary>
        /// 销毁UI时调用
        /// </summary>
        public virtual void OnDestory()
        {
            UIHelper.GetInstance().AddOrGetComponent<CanvasGroup>(GO).interactable = false;
        }

        /// <summary>
        /// UI刷新时调用
        /// </summary>
        /// <param name="baseUIPanel"></param>
        public virtual void OnRefresh(BaseUIPanel baseUIPanel)
        {
            
        }

        /// <summary>
        /// 每帧调用
        /// </summary>
        /// <param name="deltaTime"></param>
        public virtual void OnUpdate(float deltaTime)
        {
            
        }

        protected void Push(BaseUIPanel baseUIPanel)
        {
            GameRoot.UIManager.Push(baseUIPanel);
        }

        protected void Pop()
        {
            GameRoot.UIManager.Pop();
        }

        protected void PopAll()
        {
            GameRoot.UIManager.PopAll();
        }
    }
}