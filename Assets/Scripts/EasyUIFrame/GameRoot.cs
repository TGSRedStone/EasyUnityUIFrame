using System;
using System.Runtime.CompilerServices;
using EasyUIFrame.Frame;
using EasyUIFrame.GamePlay;
using UnityEngine;

namespace EasyUIFrame
{
    public class GameRoot : Singleton<GameRoot>
    {
        
        private void Start()
        {
            UIManager.Instance.OnInit();
            UIManager.Instance.Push(new MainMenuPanel());
        }
    }
}