using UnityEngine;

namespace EasyUIFrame.GamePlay.UI.UIData
{
    [CreateAssetMenu(menuName="Data/Create SettingData")]
    public class SettingDataScriptableObject : ScriptableObject
    {
        [Range(1, 100)]
        public int MusicVolume;
        [Range(1, 100)]
        public int SFXVolume;
        [Range(1, 10)]
        public float Sensitivity;
        [Range(60, 120)]
        public int FOV;
    }
}
