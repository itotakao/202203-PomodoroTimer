using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using _Util;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Application
{
    public class StatusUI : MonoBehaviour, IAssetsFacadeSetup, IAssetsFacadeHomeUtilityEvnets
    {
        public static StatusUI Current { get; private set; }

        private ColorData ColorData => ColorData.Current;

        const int MaxCount = 9;

        public bool IsMaxCount => (CurrentCount >= MaxCount);
        [field: SerializeField]
        public int CurrentCount { get; private set; }

        [SerializeField]
        private Image[] counterImageList;

#if UNITY_EDITOR
        public void SetInspectorUI()
        {
            Transform items = transform.Find("Items");
            counterImageList = items.GetComponentsInChildren<Image>();
        }
#endif

        public void OnSetup()
        {
            Current = this;
        }

        public void AddCounter()
        {
            if (IsMaxCount)
            {
                counterImageList[CurrentCount - 1].color = Color.clear;
                return;
            }

            CurrentCount += 1;

            Color color;
            if (IsMaxCount)
            {
                color = ColorData.SpecialBreakTimeColor;
            }
            else if (CurrentCount % 2 == 0)
            {
                color = ColorData.BreakTimeColor;
            }
            else
            {
                color = ColorData.WorkTimeColor;
            }

            counterImageList[CurrentCount - 1].color = color;

            bool isFirstCount = (CurrentCount <= 1);
            if (isFirstCount)
            {
                return;
            }

            counterImageList[CurrentCount - 2].color = Color.clear;
        }

        public void ResetCounter()
        {
            CurrentCount = 0;
            counterImageList.ToList().ForEach(x => x.color = Color.white);
        }

        public void OnIdle()
        {
            ResetCounter();
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
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(StatusUI))]
    public class StatusUIEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set UI"))
            {
                StatusUI t = target as StatusUI;
                t.SetInspectorUI();
            }
        }
    }
#endif
}