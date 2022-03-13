using UnityEngine;
using UnityEngine.UI;
using Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace _Application
{
    public class AboutDialog : MonoBehaviour, IAssetsFacadeHomeUtilityEvnets
    {
        private AudioManager AudioManager => AudioManager.Current;
        private HomeState HomeState => HomeState.Current;

        [SerializeField]
        private Transform rootTransform;
        [SerializeField]
        private Button closeButton;
        [SerializeField]
        private Button startButtton;

#if UNITY_EDITOR
        public void SetInspectorUI()
        {
            rootTransform = transform.GetChild(0);

            closeButton = rootTransform.Find("Button-Close").GetComponent<Button>();
            UnityEditor.Events.UnityEventTools.RemovePersistentListener(closeButton.onClick, OnPressedButton);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(closeButton.onClick, OnPressedButton);

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
            Open();
        }

        public void OnSetting()
        {
            Close();
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

        private void Open()
        {
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
    [CustomEditor(typeof(AboutDialog))]
    public class AboutDialogEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set UI"))
            {
                AboutDialog t = target as AboutDialog;
                t.SetInspectorUI();
            }
        }
    }
#endif
}