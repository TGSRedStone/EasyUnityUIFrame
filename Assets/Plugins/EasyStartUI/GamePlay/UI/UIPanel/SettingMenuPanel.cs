using System;
using EasyUIFrame.Frame.UI;
using EasyUIFrame.GamePlay.UI.UIData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EasyUIFrame.GamePlay.UI.UIPanel
{
    public class SettingMenuPanel : BaseUIPanel
    {
        private const string SensitivityPrefKey = "EasyUIFrame.Settings.Sensitivity";
        private const string MusicVolumePrefKey = "EasyUIFrame.Settings.MusicVolume";
        private const string FovPrefKey = "EasyUIFrame.Settings.FOV";
        private const string SfxVolumePrefKey = "EasyUIFrame.Settings.SFXVolume";

        private SettingDataScriptableObject settingData;
        private Button backButton;
        private Button saveButton;
        private TMP_Text sensitivityCount;
        private TMP_Text musicVolumeCount;
        private TMP_Text fovCount;
        private TMP_Text sfxVolumeCount;
        private Slider sensitivitySlider;
        private Slider musicVolumeSlider;
        private Slider fovSlider;
        private Slider sfxVolumeSlider;

        private static readonly string path = "Prefab/SettingPanel";
        private static readonly string name = "SettingPanel";
        private static readonly UIType uiType = new UIType(path, name);

        public SettingMenuPanel() : base(uiType, false)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            settingData = Resources.Load<SettingDataScriptableObject>("ScriptObjects/SettingData");

            backButton = UIHelper.AddOrGetComponentInChild<Button>(GO, "BackButton");
            saveButton = UIHelper.AddOrGetComponentInChild<Button>(GO, "SaveButton");
            sensitivityCount = UIHelper.AddOrGetComponentInChild<TMP_Text>(GO, "SensitivityCount");
            musicVolumeCount = UIHelper.AddOrGetComponentInChild<TMP_Text>(GO, "MusicVolumeCount");
            fovCount = UIHelper.AddOrGetComponentInChild<TMP_Text>(GO, "FOVCount");
            sfxVolumeCount = UIHelper.AddOrGetComponentInChild<TMP_Text>(GO, "SFXVolumeCount");
            sensitivitySlider = UIHelper.AddOrGetComponentInChild<Slider>(GO, "SensitivitySlider");
            musicVolumeSlider = UIHelper.AddOrGetComponentInChild<Slider>(GO, "MusicVolumeSlider");
            fovSlider = UIHelper.AddOrGetComponentInChild<Slider>(GO, "FOVSlider");
            sfxVolumeSlider = UIHelper.AddOrGetComponentInChild<Slider>(GO, "SFXVolumeSlider");

            if (backButton == null || saveButton == null ||
                sensitivityCount == null || musicVolumeCount == null || fovCount == null || sfxVolumeCount == null ||
                sensitivitySlider == null || musicVolumeSlider == null || fovSlider == null || sfxVolumeSlider == null)
            {
                Debug.LogError("SettingMenuPanel init failed: required UI references are missing.");
                return;
            }

            sensitivitySlider.onValueChanged.AddListener(v => sensitivityCount.text = Math.Round(v, 2).ToString("F2"));
            musicVolumeSlider.onValueChanged.AddListener(v => musicVolumeCount.text = Mathf.RoundToInt(v).ToString());
            fovSlider.onValueChanged.AddListener(v => fovCount.text = Mathf.RoundToInt(v).ToString());
            sfxVolumeSlider.onValueChanged.AddListener(v => sfxVolumeCount.text = Mathf.RoundToInt(v).ToString());
            backButton.onClick.AddListener(BackMainMenuPanel);
            saveButton.onClick.AddListener(SaveAndBackMainMenuPanel);

            if (settingData == null)
            {
                Debug.LogWarning("SettingData not found at Resources/ScriptObjects/SettingData, using slider defaults/PlayerPrefs.");
            }

            var sensitivityValue = ResolveFloatSetting(SensitivityPrefKey, settingData != null ? settingData.Sensitivity : sensitivitySlider.value, sensitivitySlider.minValue, sensitivitySlider.maxValue);
            var musicVolumeValue = ResolveIntSetting(MusicVolumePrefKey, settingData != null ? settingData.MusicVolume : Mathf.RoundToInt(musicVolumeSlider.value), musicVolumeSlider.minValue, musicVolumeSlider.maxValue);
            var fovValue = ResolveIntSetting(FovPrefKey, settingData != null ? settingData.FOV : Mathf.RoundToInt(fovSlider.value), fovSlider.minValue, fovSlider.maxValue);
            var sfxVolumeValue = ResolveIntSetting(SfxVolumePrefKey, settingData != null ? settingData.SFXVolume : Mathf.RoundToInt(sfxVolumeSlider.value), sfxVolumeSlider.minValue, sfxVolumeSlider.maxValue);

            sensitivitySlider.value = sensitivityValue;
            musicVolumeSlider.value = musicVolumeValue;
            fovSlider.value = fovValue;
            sfxVolumeSlider.value = sfxVolumeValue;

            sensitivityCount.text = Math.Round(sensitivitySlider.value, 2).ToString("F2");
            musicVolumeCount.text = Mathf.RoundToInt(musicVolumeSlider.value).ToString();
            fovCount.text = Mathf.RoundToInt(fovSlider.value).ToString();
            sfxVolumeCount.text = Mathf.RoundToInt(sfxVolumeSlider.value).ToString();
        }

        private void BackMainMenuPanel()
        {
            Pop();
        }

        private void SaveAndBackMainMenuPanel()
        {
            var sensitivityValue = (float)Math.Round(sensitivitySlider.value, 2);
            var musicVolumeValue = Mathf.RoundToInt(musicVolumeSlider.value);
            var fovValue = Mathf.RoundToInt(fovSlider.value);
            var sfxVolumeValue = Mathf.RoundToInt(sfxVolumeSlider.value);

            if (settingData != null)
            {
                settingData.Sensitivity = sensitivityValue;
                settingData.MusicVolume = musicVolumeValue;
                settingData.FOV = fovValue;
                settingData.SFXVolume = sfxVolumeValue;
            }

            PlayerPrefs.SetFloat(SensitivityPrefKey, sensitivityValue);
            PlayerPrefs.SetInt(MusicVolumePrefKey, musicVolumeValue);
            PlayerPrefs.SetInt(FovPrefKey, fovValue);
            PlayerPrefs.SetInt(SfxVolumePrefKey, sfxVolumeValue);
            PlayerPrefs.Save();
            Pop();
        }

        private static float ResolveFloatSetting(string key, float defaultValue, float min, float max)
        {
            var value = PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : defaultValue;
            return Mathf.Clamp(value, min, max);
        }

        private static int ResolveIntSetting(string key, int defaultValue, float min, float max)
        {
            var value = PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : defaultValue;
            var minValue = Mathf.RoundToInt(min);
            var maxValue = Mathf.RoundToInt(max);
            return Mathf.Clamp(value, minValue, maxValue);
        }
    }
}
