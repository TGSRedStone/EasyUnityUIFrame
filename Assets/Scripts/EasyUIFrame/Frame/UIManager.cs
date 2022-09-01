using System.Collections.Generic;
using UnityEngine;

namespace EasyUIFrame.Frame
{
    public class UIManager : Singleton<UIManager>
    {
        public Stack<BaseUIPanel> uiStack;
        public Dictionary<string, GameObject> uiObjectsDict;
        //TODO: 增加多Canvas支持
        public Canvas Canvas;

        public void OnInit()
        {
            uiStack = new Stack<BaseUIPanel>();
            uiObjectsDict = new Dictionary<string, GameObject>();
        }

        /// <summary>
        /// 加载UI预制体
        /// </summary>
        /// <param name="uiType"></param>
        /// <returns></returns>
        public GameObject LoadGameObject(UIType uiType)
        {
            if (uiObjectsDict.ContainsKey(uiType.Name))
            {
                return uiObjectsDict[uiType.Name];
            }
            else
            {
                GameObject uiObj = GameObject.Instantiate(Resources.Load<GameObject>(uiType.Path), Canvas.transform);
                return uiObj;
            }
        }
        
        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="baseUIPanel"></param>
        public void Push(BaseUIPanel baseUIPanel)
        {
            GameObject pushObj = LoadGameObject(baseUIPanel.uiType);
            baseUIPanel.go = pushObj;
            
            //栈中没有对象时可直接入栈
            if (uiStack.Count == 0)
            {
                uiStack.Push(baseUIPanel);
            }
            else
            {
                //防止点击过快导致多次将同一对象入栈
                if (uiStack.Peek().uiType.Name != baseUIPanel.uiType.Name)
                {
                    uiStack.Push(baseUIPanel);
                }
            }
            //UI创建完毕
            baseUIPanel.OnCreate();
        }

        /// <summary>
        /// 出栈
        /// </summary>
        public void Pop()
        {
            if (uiStack.Count > 0)
            {
                uiStack.Peek().OnClose();
                uiStack.Peek().OnDestory();
                Destroy(uiObjectsDict[uiStack.Peek().uiType.Name]);
                uiObjectsDict.Remove(uiStack.Peek().uiType.Name);
                uiStack.Pop();
            }
        }

        /// <summary>
        /// 清空栈
        /// </summary>
        public void PopAll()
        {
            for (int i = 0; i < uiStack.Count; i++)
            {
                Pop();
            }
        }
    }
}