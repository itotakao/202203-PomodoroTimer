using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using _Util;
using Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Application
{
    public class Timer : MonoBehaviour, IAssetsFacadeSetup, IAssetsFacadeUpdate, IAssetsFacadeHomeUtilityEvnets
    {
        public static Timer Current { get; private set; }

        private AudioManager AudioManager => AudioManager.Current;
        private AssetsFacade AssetsFacade => AssetsFacade.Current;
        private HomeState HomeState => HomeState.Current;
        private PlayerData PlayerData => PlayerData.Current;
        private ColorData ColorData => ColorData.Current;
        private StatusUI StatusUI => StatusUI.Current;

        private DateTime InitializationDate => DateTime.MaxValue;
        private bool IsStartCount => (DateTime.Compare(startDate, InitializationDate) != 0);

        public enum Status
        {
            None,
            Work,
            Break,
            SpecialBreak
        }
        [field: SerializeField]
        public Status CurrentStatus { get; private set; } = Status.None;

        [SerializeField]
        private Image countBarImage;
        [SerializeField]
        private TextMeshProUGUI timerText;
        [SerializeField]
        private TextMeshProUGUI statusText;

        private int maxTimeMin = -1;
        private float pauseTimeSec = 0;
        private DateTime startDate = DateTime.MaxValue;

#if UNITY_EDITOR
        public void SetInspectorUI()
        {
            countBarImage = transform.Find("Image-CountBar").GetComponent<Image>();
            timerText = transform.Find("Text-Timer").GetComponent<TextMeshProUGUI>();
            statusText = transform.Find("Text-Status").GetComponent<TextMeshProUGUI>();
        }
#endif

        public void OnSetup()
        {
            Current = this;

            SetDefault();
        }

        public void OnUpdate()
        {
            if (!IsStartCount || !PlayerData.IsHighSpec)
            {
                return;
            }

            UpdateTimer();
        }

        IEnumerator CoUpdate()
        {
            while (true)
            {
                if (PlayerData.IsHighSpec)
                {
                    break;
                }

                if (IsStartCount)
                {
                    UpdateTimer();

                    //if (StatusUI.IsMaxCount)
                    //{
                    //    break;
                    //}
                }

                yield return new WaitForSeconds(1);
            }
        }

        public void StartTimer()
        {
            StatusUI.AddCounter();

            Status useStatus;
            if (CurrentStatus == Status.None)
            {
                useStatus = Status.Work;
            }
            else if (StatusUI.IsMaxCount)
            {
                useStatus = Status.SpecialBreak;
            }
            else
            {
                useStatus = (CurrentStatus == Status.Work ? Status.Break : Status.Work);
            }

            SetStatus(useStatus);
            CurrentStatus = useStatus;
            SetTimerColor();
            startDate = DateTime.Now;

            AssetsFacade.InvokeUtilityEvnet("OnTimerUpdate");
        }

        public void StopTimer()
        {
            StatusUI.AddCounter();
            SetStatus(Status.None);
            startDate = InitializationDate;
        }

        public void PauseTimer()
        {
            pauseTimeSec = GetTotalSeconds();
            startDate = InitializationDate;
        }

        public void RestartTimer()
        {
            startDate = (DateTime.Now.AddSeconds(-pauseTimeSec));
        }

        private string GetCountTimeText()
        {
            TimeSpan diff = (DateTime.Now - startDate);
            int m = diff.Minutes;
            int s = diff.Seconds;

            if (PlayerData.IsCountUp)
            {
                return (m.ToString("00") + ":" + s.ToString("00"));
            }
            else
            {
                int maxTime = convertMaxTime();
                int totalSec = ((m * 60) + s);
                int diffSec = (maxTime - totalSec);

                TimeSpan span = new TimeSpan(0, 0, diffSec);
                return (span.Minutes.ToString("00") + ":" + span.Seconds.ToString("00"));
            }
        }

        private float GetTotalSeconds()
        {
            TimeSpan diff = (DateTime.Now - startDate);
            return (float)(diff.TotalSeconds);
        }

        public void OnIdle()
        {
            SetDefault();
            StopCoroutine("CoUpdate");
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
            StartTimer();
            if (!PlayerData.IsHighSpec)
            {
                StopCoroutine("CoUpdate");
                StartCoroutine("CoUpdate");
            }
        }

        public void OnPause()
        {
            PauseTimer();
        }

        public void OnRestart()
        {
            RestartTimer();
        }

        public void OnFinish()
        {
            StopTimer();
        }

        private void SetDefault()
        {
            timerText.text = "00:00";
            countBarImage.fillAmount = 0;

            startDate = InitializationDate;
            pauseTimeSec = 0;

            SetStatus(Status.None);
        }

        private void SetStatus(Status status)
        {
            CurrentStatus = status;
            SetStatusText(status);
            SetMaxTime(status);
        }

        private void UpdateTimer()
        {
            timerText.text = GetCountTimeText();
            int workTime = convertMaxTime();
            float ratio = (GetTotalSeconds() / workTime);
            countBarImage.fillAmount = ratio;

            if (ratio >= 1.0f)
            {
                if (StatusUI.IsMaxCount)
                {
                    HomeState.SetStatus(HomeState.Status.Finish);
                    return;
                }

                if (!PlayerData.IsHighSpec)
                {
                    timerText.text = "00:00";
                    countBarImage.fillAmount = 0;
                }

                AudioManager.SE.Play("complete-timer");
                StartTimer();
            }
        }

        private void SetStatusText(Status status)
        {
            string message = status switch
            {
                Status.None => "",
                Status.Work => "WORK",
                Status.Break => "BREAK",
                Status.SpecialBreak => "BREAK",
                _ => throw new System.ComponentModel.InvalidEnumArgumentException(),
            };
            statusText.text = message;
        }

        private void SetTimerColor()
        {
            countBarImage.color = ColorData.GetUIColor();
        }

        private void SetMaxTime(Status status)
        {
            int time = status switch
            {
                Status.None => PlayerData.WorkTimeMin,
                Status.Work => PlayerData.WorkTimeMin,
                Status.Break => PlayerData.BreakTimeMin,
                Status.SpecialBreak => PlayerData.SpecialBreakTimeMin,
                _ => throw new System.ComponentModel.InvalidEnumArgumentException(),
            };

            maxTimeMin = time;
        }

        private int convertMaxTime()
        {
            return (PlayerData.IsConvertMinToSec ? (maxTimeMin * 60) : maxTimeMin);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Timer))]
    public class TimerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set UI"))
            {
                Timer t = target as Timer;
                t.SetInspectorUI();
            }
        }
    }
#endif
}