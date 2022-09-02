using UnityEngine;

namespace EasyUIFrame.Frame
{
    public abstract class BaseUIPanel
    {
        public UIType UIType;

        public GameObject GO;

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
        public virtual void OnOpen()
        {
            UIHelper.GetInstance().AddOrGetComponent<CanvasGroup>(GO).interactable = true;
        }

        /// <summary>
        /// 关闭UI时调用
        /// </summary>
        public virtual void OnClose()
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
        
    }
}