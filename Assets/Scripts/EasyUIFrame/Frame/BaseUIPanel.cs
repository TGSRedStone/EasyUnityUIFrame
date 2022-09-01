using UnityEngine;

namespace EasyUIFrame.Frame
{
    public abstract class BaseUIPanel
    {
        public UIType uiType;

        public GameObject go;

        protected BaseUIPanel(UIType uiType)
        {
            this.uiType = uiType;
        }

        /// <summary>
        ///创建UI时调用
        /// </summary>
        public virtual void OnCreate()
        {
            UIHelper.GetInstance().AddOrGetComponent<CanvasGroup>(go).interactable = true;
        }

        /// <summary>
        /// 打开UI时调用
        /// </summary>
        public virtual void OnOpen()
        {
            UIHelper.GetInstance().AddOrGetComponent<CanvasGroup>(go).interactable = true;
        }

        /// <summary>
        /// 关闭UI时调用
        /// </summary>
        public virtual void OnClose()
        {
            UIHelper.GetInstance().AddOrGetComponent<CanvasGroup>(go).interactable = false;
        }

        /// <summary>
        /// 销毁UI时调用
        /// </summary>
        public virtual void OnDestory()
        {
            UIHelper.GetInstance().AddOrGetComponent<CanvasGroup>(go).interactable = false;
        }
        
    }
}