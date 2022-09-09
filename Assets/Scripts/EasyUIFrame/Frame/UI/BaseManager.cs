using System;
using UnityEngine;

namespace EasyUIFrame.Frame.UI
{
    public abstract class BaseManager : MonoBehaviour
    {
        public abstract void OnInit();
        public abstract void OnUpdate(float deltaTime);
        
        protected void Awake()
        {
            GameRoot.RegisterManager(this);
        }
    }
}