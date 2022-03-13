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
    public class FinishDialog : MonoBehaviour, IAssetsFacadeHomeUtilityEvnets
    {
        private AudioManager AudioManager => AudioManager.Current;
        private HomeState HomeState => HomeState.Current;

        [SerializeField]
        private Transform rootTransform;
        [SerializeField]
        private Button startButtton;

#if UNITY_EDITOR
        public void SetInspectorUI()
        {
            rootTransform = transform.GetChild(0);

            startButtton = rootTransform.Find("Button-Start").GetComponent<Button>();
            UnityEditor.Events.UnityEventTools.RemovePersistentListener(startButtton.onClick, OnPressedButton);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(startButtton.onClick, OnPressedButton);
        }
#endif

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
            Open();
        }

        private void Open()
        {
            AudioManager.SE.Play("complete-timer");
            rootTransform.gameObject.SetActive(true);
        }

        private void Close()
        {
            rootTransform.gameObject.SetActive(false);
        }

        private void OnPressedButton()
        {
            AudioManager.SE.Play("button");
            HomeState.SetStatus(HomeState.Status.Idle);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(FinishDialog))]
    public class FinishDialogEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set UI"))
            {
                FinishDialog t = target as FinishDialog;
                t.SetInspectorUI();
            }
        }
    }
#endif
}