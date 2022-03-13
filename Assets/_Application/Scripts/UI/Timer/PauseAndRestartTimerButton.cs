using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Application
{
    public class PauseAndRestartTimerButton : BaseButton, IAssetsFacadeHomeUtilityEvnets, IAssetsFacadeTimerUpdate
    {
        private AudioManager AudioManager => AudioManager.Current;
        private HomeState HomeState => HomeState.Current;
        private ColorData ColorData => ColorData.Current;

        [SerializeField]
        private Image buttonImage;
        [SerializeField]
        private GameObject pauseButtonObject;
        [SerializeField]
        private GameObject restartButtonObject;

#if UNITY_EDITOR
        public void SetInspectorUI()
        {
            buttonImage = GetComponent<Image>();
            pauseButtonObject = transform.GetChild(0).gameObject;
            restartButtonObject = transform.GetChild(1).gameObject;
        }
#endif

        protected override void OnPressedButton()
        {
            bool isCountTimer = (HomeState.CurrenStatus == HomeState.Status.Start || HomeState.CurrenStatus == HomeState.Status.Restart);
            SetVisibleUI(isCountTimer);
            AudioManager.SE.Play("button");

            if (isCountTimer)
            {
                HomeState.SetStatus(HomeState.Status.Pause);
            }
            else
            {
                HomeState.SetStatus(HomeState.Status.Restart);
            }
        }

        public void OnIdle()
        {
            SetVisibleUI(false);
        }

        public void OnAbout()
        {
            // Do Nothing
        }

        public void OnSetting()
        {
            // Do Nothing
        }

        public void OnStart()
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

        public void OnTimerUpdate()
        {
            SetColorUI();
        }

        private void SetVisibleUI(bool isCountTimer)
        {
            pauseButtonObject.SetActive(!isCountTimer);
            restartButtonObject.SetActive(isCountTimer);
        }

        private void SetColorUI()
        {
            buttonImage.color = ColorData.GetUIColor();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PauseAndRestartTimerButton))]
    public class PauseAndRestartTimerButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set UI"))
            {
                PauseAndRestartTimerButton t = target as PauseAndRestartTimerButton;
                t.SetInspectorUI();
            }
        }
    }
#endif
}