using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Application
{
    public class BackgroundDialog : MonoBehaviour, IAssetsFacadeHomeUtilityEvnets
    {
        [SerializeField]
        private Transform rootTransform;

#if UNITY_EDITOR
        public void SetInspectorUI()
        {
            rootTransform = transform.GetChild(0);
        }
#endif

        public void OnIdle()
        {
            Close();
        }


        public void OnAbout()
        {
            Open();
        }

        public void OnSetting()
        {
            Open();
        }

        public void OnStart()
        {
            Close();
        }

        public void OnPause()
        {
            Close();
        }

        public void OnRestart()
        {
            Close();
        }
        public void OnFinish()
        {
            Open();
        }

        private void Open()
        {
            rootTransform.gameObject.SetActive(true);
        }

        private void Close()
        {
            rootTransform.gameObject.SetActive(false);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(BackgroundDialog))]
    public class BackgroundDialogEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set UI"))
            {
                BackgroundDialog t = target as BackgroundDialog;
                t.SetInspectorUI();
            }
        }
    }
#endif
}