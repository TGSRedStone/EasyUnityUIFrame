using System;
using System.Collections.Generic;
using EasyUIFrame.Frame.UI;
using EasyUIFrame.GamePlay.UI;
using EasyUIFrame.GamePlay.UI.UIPanel;
using UnityEngine;

namespace EasyUIFrame.Frame
{
    public class GameRoot : Singleton<GameRoot>
    {
        private static readonly List<BaseManager> Managers = new List<BaseManager>();

        public static UIManager UIManager { get; private set; }
        private void Start()
        {
            UIManager = GetManager<UIManager>();

            for (int i = 0; i < Managers.Count; i++)
            {
                Managers[i].OnInit();
            }
            
            UIManager.Push(new MainMenuPanel());
        }

        private void Update()
        {
            for (int i = 0; i < Managers.Count; i++)
            {
                Managers[i].OnUpdate(Time.deltaTime);
            }
        }

        public static T GetManager<T>() where T : BaseManager
        {
            for (int i = 0; i < Managers.Count; i++)
            {
                if (typeof(T) == Managers[i].GetType())
                {
                    return Managers[i] as T;
                }
            }
            Debug.LogError($"不存在{typeof(T).Name}管理器");
            return null;
        }

        public static void RegisterManager(BaseManager baseManager)
        {
            Managers.Add(baseManager);
        }
    }
}