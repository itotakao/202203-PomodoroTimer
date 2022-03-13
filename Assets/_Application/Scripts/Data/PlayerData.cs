using UnityEngine;

namespace _Application
{
    public class PlayerData : MonoBehaviour
    {
        public static PlayerData Current { get; private set; }

        public int WorkTimeMin { get; private set; }
        public int BreakTimeMin { get; private set; }
        public int SpecialBreakTimeMin { get; private set; }
        public bool IsCountUp { get; private set; } = true;
        public bool IsHighSpec { get; private set; } = true;

        public bool IsConvertMinToSec { get; private set; } = true;

        private void Awake()
        {
            Current = this;
            SetDefault();
        }

        private void SetDefault()
        {
            WorkTimeMin = 25;
            BreakTimeMin = 5;
            SpecialBreakTimeMin = 30;
        }

        public void SetWorkTime(int value)
        {
            WorkTimeMin = value;
        }

        public void SetBreakTime(int value)
        {
            BreakTimeMin = value;
        }

        public void SetSpecialBreakTime(int value)
        {
            SpecialBreakTimeMin = value;
        }

        public void SetCountUp(bool value)
        {
            IsCountUp = value;
        }

        public void SetHighSpec(bool value)
        {
            IsHighSpec = value;
        }

        public void SetConvertMinToSec(bool value)
        {
            IsConvertMinToSec = value;
        }
    }
}