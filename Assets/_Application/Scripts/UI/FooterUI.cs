using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Util;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Application
{
    public class FooterUI : MonoBehaviour, IAssetsFacadeSetup, IAssetsFacadeHomeUtilityEvnets
    {
        private ApplicationData ApplicationData => ApplicationData.Current;

        [SerializeField]
        private GameObject idleObject;
        [SerializeField]
        private GameObject startObject;

#if UNITY_EDITOR
        public void SetInspectorUI()
        {
            idleObject = transform.GetChild(0).gameObject;
            startObject = transform.GetChild(0).gameObject;
        }
#endif

        public void OnSetup()
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
        }

        public void OnIdle()
        {
            SwipeAnimation(idleObject);
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
            SwipeAnimation(startObject);
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

        private void SwipeAnimation(GameObject targetObj)
        {
            StartCoroutine(CoSwipeAnimation(targetObj));
        }

        private IEnumerator CoSwipeAnimation(GameObject targetObj)
        {
            float beforePosX = transform.localPosition.x;
            float afterPosX = targetObj.transform.localPosition.x;
            bool isOver = (Mathf.Abs(beforePosX) > afterPosX);
            float animationTimeSec = 0.0f;
            while (true)
            {
                float afterX = Mathf.Lerp(beforePosX, afterPosX, animationTimeSec);
                afterX = (isOver ? afterX : -afterX);
                transform.localPosition = new Vector3(afterX, transform.localPosition.y, transform.localPosition.z);// afterXをマイナスしているのはアタッチしているオブジェクトを動かしてにスライドさせたいから
                animationTimeSec += (Time.deltaTime / ApplicationData.AnimationTimeSec);

                if (animationTimeSec >= 1.0f)
                {
                    transform.localPosition = new Vector3(-afterPosX, transform.localPosition.y, transform.localPosition.z); ;
                    break;
                }

                yield return null;
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(FooterUI))]
    public class FooterUIEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set UI"))
            {
                FooterUI t = target as FooterUI;
                t.SetInspectorUI();
            }
        }
    }
#endif
}