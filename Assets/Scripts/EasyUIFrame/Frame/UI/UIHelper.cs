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
        
        public T AddOrGetComponent<T>(Transform go) where T : Component
        {
            if (go.TryGetComponent(typeof(T), out Component component))
            {
                return component as T;
            }
            else
            {
                Debug.LogWarning($"未能获取{go}身上的{typeof(T)}组件自动添加了一个，请检查此问题");
                return go.gameObject.AddComponent<T>();
            }
        }
        
        /// <summary>
        /// 获取一个物体中对应名字的子物体
        /// </summary>
        /// <param name="go"></param>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddOrGetComponentInChild<T>(Transform go,string name) where T : Component 
        {
            var transforms = go.GetComponentsInChildren<Transform>();
            
            foreach (var transform in transforms) 
            {
                if (transform.name == name)  
                {
                    if (transform.TryGetComponent(typeof(T), out var tra))
                    {
                        return tra as T;
                    }
                }
            }

            Debug.LogWarning($"{go.name}中没找到{name}物体！");
            return null;
        } 
    }
}
