using System;
using EasyUIFrame.Frame;
using EasyUIFrame.Frame.UI;
using EasyUIFrame.GamePlay.UI.UIData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EasyUIFrame.GamePlay.UI.UIPanel
{
    public class SettingMenuPanel : BaseUIPanel
    {
        private SettingDataScriptableObject settingData;
        private Button backBotton;
        private Button saveButton;
        private TMP_Text sensitivityCount;
        private TMP_Text musicVolumeCount;
        private TMP_Text fOVCount;
        private TMP_Text sfXVolumeCount;
        private Slider sensitivitySlider;
        private Slider musicVolumeSlider;
        private Slider fOVSlider;
        private Slider sFXVolumeSlider;
        
        private static readonly string path = "Prefab/SettingPanel";
        private static readonly string name = "SettingPanel";
        private static UIType uiType = new UIType(path, name);
        public SettingMenuPanel() : base(uiType, false)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();
            settingData = GameRoot.UIManager.SettingData;
            backBotton = UIHelper.GetInstance().AddOrGetComponentInChild<Button>(GO, "BackButton");
            saveButton = UIHelper.GetInstance().AddOrGetComponentInChild<Button>(GO, "SaveButton");
            sensitivityCount = UIHelper.GetInstance().AddOrGetComponentInChild<TMP_Text>(GO, "SensitivityCount");
            musicVolumeCount = UIHelper.GetInstance().AddOrGetComponentInChild<TMP_Text>(GO, "MusicVolumeCount");
            fOVCount = UIHelper.GetInstance().AddOrGetComponentInChild<TMP_Text>(GO, "FOVCount");
            sfXVolumeCount = UIHelper.GetInstance().AddOrGetComponentInChild<TMP_Text>(GO, "SFXVolumeCount");
            sensitivitySlider = UIHelper.GetInstance().AddOrGetComponentInChild<Slider>(GO, "SensitivitySlider");
            musicVolumeSlider = UIHelper.GetInstance().AddOrGetComponentInChild<Slider>(GO, "MusicVolumeSlider");
            fOVSlider = UIHelper.GetInstance().AddOrGetComponentInChild<Slider>(GO, "FOVSlider");
            sFXVolumeSlider = UIHelper.GetInstance().AddOrGetComponentInChild<Slider>(GO, "SFXVolumeSlider");

            sensitivitySlider.onValueChanged.AddListener(a => sensitivityCount.text = Math.Round(a,2).ToString());
            musicVolumeSlider.onValueChanged.AddListener(a => musicVolumeCount.text = Math.Ceiling(a).ToString());
            fOVSlider.onValueChanged.AddListener(a => fOVCount.text = Math.Ceiling(a).ToString());
            sFXVolumeSlider.onValueChanged.AddListener(a => sfXVolumeCount.text = Math.Ceiling(a).ToString());
            backBotton.onClick.AddListener(BackMainMenuPanel);
            saveButton.onClick.AddListener(SaveAndBackMainMenuPanel);

            if (settingData != null)
            {
                sensitivityCount.text = settingData.Sensitivity.ToString();
                musicVolumeCount.text = settingData.MusicVolume.ToString();
                fOVCount.text = settingData.FOV.ToString();
                sfXVolumeCount.text = settingData.SFXVolume.ToString();
                sensitivitySlider.value = settingData.Sensitivity;
                musicVolumeSlider.value = settingData.MusicVolume;
                fOVSlider.value = settingData.FOV;
                sFXVolumeSlider.value = settingData.SFXVolume;
            }
            else
            {
                Debug.LogError("没有获取到SettingData");
            }
        }

        private void BackMainMenuPanel()
        {
            Pop();
        }

        private void SaveAndBackMainMenuPanel()
        {
            settingData.Sensitivity = (float) Math.Round(sensitivitySlider.value,2);
            settingData.MusicVolume = (int) musicVolumeSlider.value;
            settingData.FOV = (int) fOVSlider.value;
            settingData.SFXVolume = (int) sFXVolumeSlider.value;
            Pop();
        }
    }
}