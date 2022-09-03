using System.Collections.Generic;
using EasyUIFrame.GamePlay.UI.UIData;
using UnityEngine;

namespace EasyUIFrame.Frame.UI
{
    public class UIManager : Singleton<UIManager>
    {
        //TODO: 增加多Canvas支持
        public Canvas canvas;
        //TODO: 更优雅的管理持久化数据
        public SettingDataScriptableObject SettingData;

        private Stack<BaseUIPanel> uiStack;
        private Dictionary<string, BaseUIPanel> uiObjectsDict;
        
        public void OnInit()
        {
            uiStack = new Stack<BaseUIPanel>();
            uiObjectsDict = new Dictionary<string, BaseUIPanel>();
            SettingData = Resources.Load<SettingDataScriptableObject>("ScriptObjects/SettingData");
        }

        /// <summary>
        /// 加载UI预制体
        /// </summary>
        /// <param name="uiType"></param>
        /// <returns></returns>
        private Transform LoadGameObject(UIType uiType)
        {
            if (canvas == null)
            {
                Debug.LogError("canvas不存在");
                return null;
            }
            var uiObj = Instantiate(Resources.Load<GameObject>(uiType.Path), canvas.transform).transform;
            return uiObj;
        }
        
        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="baseUIPanel"></param>
        public void Push(BaseUIPanel baseUIPanel)
        {
            if (uiStack.Count > 0)
            {
                uiStack.Peek().OnDisable();
            }

            //字典中不存在对应物体则实例化一个并写入字典，否则就用新的去刷新旧的
            if (!uiObjectsDict.ContainsKey(baseUIPanel.UIType.Name))
            {
                var pushObj = LoadGameObject(baseUIPanel.UIType);
                uiObjectsDict.Add(baseUIPanel.UIType.Name, baseUIPanel);
                baseUIPanel.GO = pushObj;
                if (baseUIPanel.GO != null)
                {
                    //UI创建完毕
                    baseUIPanel.OnCreate();
                }
            }
            else
            {
                var bup = uiObjectsDict[baseUIPanel.UIType.Name];
                bup.OnRefresh(baseUIPanel);
                baseUIPanel = bup;
            }
            
            if (uiStack.Count > 0)
            {
                //防止点击过快导致多次将同一对象入栈
                if (uiStack.Peek().UIType.Name != baseUIPanel.UIType.Name)
                {
                    uiStack.Push(baseUIPanel);
                }
            }
            else
            {
                //栈中没有对象时可直接入栈
                uiStack.Push(baseUIPanel);
            }
        }

        /// <summary>
        /// 出栈
        /// </summary>
        public void Pop()
        {
            if (uiStack.Count > 0)
            {
                uiStack.Peek().OnDisable();
                uiStack.Peek().OnDestory();
                Destroy(uiObjectsDict[uiStack.Peek().UIType.Name].GO.gameObject);
                uiObjectsDict.Remove(uiStack.Peek().UIType.Name);
                uiStack.Pop();
            }

            if (uiStack.Count > 0)
            {
                uiStack.Peek().OnEnable();
            }
        }

        /// <summary>
        /// 清空栈
        /// </summary>
        public void PopAll()
        {
            for (int i = 0; i < uiStack.Count; i++)
            {
                uiStack.Peek().OnDisable();
                uiStack.Peek().OnDestory();
                Destroy(uiObjectsDict[uiStack.Peek().UIType.Name].GO.gameObject);
                uiObjectsDict.Remove(uiStack.Peek().UIType.Name);
                uiStack.Pop();
            }
        }
    }
}