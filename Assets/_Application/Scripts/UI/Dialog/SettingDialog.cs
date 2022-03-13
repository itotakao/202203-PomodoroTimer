using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Application
{
    public class SettingDialog : MonoBehaviour, IAssetsFacadeHomeUtilityEvnets
    {
        private AudioManager AudioManager => AudioManager.Current;
        private HomeState HomeState => HomeState.Current;
        private PlayerData PlayerData => PlayerData.Current;

        [SerializeField]
        private Transform rootTransform;

        [SerializeField]
        private Button closeButton;
        [SerializeField]
        private TMP_InputField workTimeInputField;
        [SerializeField]
        private TMP_InputField breakTimeInputField;
        [SerializeField]
        private TMP_InputField specialBreakTimeInputField;
        [SerializeField]
        private Toggle countUpToggle;
        [SerializeField]
        private Toggle highSpecToggle;
        [SerializeField]
        private Toggle convertMinToSecToggle;
        [SerializeField]
        private Button startButtton;

#if UNITY_EDITOR
        public void SetInspectorUI()
        {
            rootTransform = transform.GetChild(0);

            closeButton = rootTransform.Find("Button-Close").GetComponent<Button>();
            UnityEditor.Events.UnityEventTools.RemovePersistentListener(closeButton.onClick, OnPressedCloseButton);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(closeButton.onClick, OnPressedCloseButton);

            workTimeInputField = rootTransform.Find("InputField-WorkTime").GetComponent<TMP_InputField>();
            breakTimeInputField = rootTransform.Find("InputField-BreakTime").GetComponent<TMP_InputField>();
            specialBreakTimeInputField = rootTransform.Find("InputField-SpecialBreakTime").GetComponent<TMP_InputField>();
            countUpToggle = rootTransform.Find("Toggle-CountUp").GetComponent<Toggle>();
            highSpecToggle = rootTransform.Find("Toggle-HighSpec").GetComponent<Toggle>();
            convertMinToSecToggle = rootTransform.Find("Toggle-ConvertMinToSec").GetComponent<Toggle>();

            startButtton = rootTransform.Find("Button-Start").GetComponent<Button>();
            UnityEditor.Events.UnityEventTools.RemovePersistentListener(startButtton.onClick, OnPressedStartButton);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(startButtton.onClick, OnPressedStartButton);
        }
#endif

        public void Open()
        {
            rootTransform.gameObject.SetActive(true);
        }

        public void Close()
        {
            rootTransform.gameObject.SetActive(false);
        }

        private void OnPressedCloseButton()
        {
            AudioManager.SE.Play("button");
            HomeState.SetStatus(HomeState.Status.Idle);
        }

        private void OnPressedStartButton()
        {
            AudioManager.SE.Play("button");
            SetData();
            HomeState.SetStatus(HomeState.Status.Start);
        }

        private void SetData()
        {
            PlayerData.SetWorkTime(int.Parse(workTimeInputField.text));
            PlayerData.SetBreakTime(int.Parse(breakTimeInputField.text));
            PlayerData.SetSpecialBreakTime(int.Parse(specialBreakTimeInputField.text));
            PlayerData.SetCountUp(countUpToggle.isOn);
            PlayerData.SetHighSpec(highSpecToggle.isOn);
            PlayerData.SetConvertMinToSec(convertMinToSecToggle.isOn);
        }

        public void OnIdle()
        {
            Close();
        }

        public void OnAbout()
        {
            // Do Nothing
        }

        public void OnSetting()
        {
            Open();
        }

        public void OnStart()
        {
            Close();
        }

        public void OnStop()
        {
            // Do Nothing
        }

        public void OnPause()
        {
            // Do Nothing
        }

        public void OnRestart()
        {
            // Do Nothing
        }

        public void OnFinish()
        {
            // Do Nothing
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(SettingDialog))]
    public class SettingDialogEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set UI"))
            {
                SettingDialog t = target as SettingDialog;
                t.SetInspectorUI();
            }
        }
    }
#endif
}