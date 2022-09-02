using UnityEngine;

namespace EasyUIFrame.Frame.UI
{
    public abstract class BaseUIPanel
    {
        public UIType UIType;

        public Transform GO;

        protected BaseUIPanel(UIType uiType)
        {
            this.UIType = uiType;
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
        /// <param name="float4"></param>
        public virtual void OnRefresh(BaseUIPanel baseUIPanel)
        {
            
        }

        protected void Push(BaseUIPanel baseUIPanel)
        {
            UIManager.Instance.Push(baseUIPanel);
        }

        protected void Pop()
        {
            UIManager.Instance.Pop();
        }
        
    }
}