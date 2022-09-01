using UnityEngine;

namespace EasyUIFrame.Frame
{
    public class UIHelper
    {
        private static UIHelper Instance;
        public static UIHelper GetInstance() 
        {
            if (Instance == null)  
            {
                Instance = new UIHelper();    
            }
            return Instance;
        }
        
        public T AddOrGetComponent<T>(GameObject go) where T : Component
        {
            if (go.TryGetComponent(typeof(T), out Component component))
            {
                return component as T;
            }
            else
            {
                Debug.LogWarning($"未能获取{go}身上的{typeof(T)}组件自动添加了一个，请检查此问题");
                return go.AddComponent<T>();
            }
            
            Debug.LogError($"未能获取{go}所需的{typeof(T)}组件");
            return null;
        }
    }
}
