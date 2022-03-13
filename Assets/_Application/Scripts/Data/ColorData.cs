using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Application
{
    public class ColorData : MonoBehaviour
    {
        public static ColorData Current { get; private set; }

        private StatusUI StatusUI => StatusUI.Current;
        private Timer Timer => Timer.Current;


        public Color32 NonActiveColor => new Color32(195, 195, 195, 255);

        public Color32 WorkTimeColor => new Color32(227, 100, 126, 255);
        public Color32 BreakTimeColor => new Color32(30, 154, 131, 255);
        public Color32 SpecialBreakTimeColor => new Color32(30, 118, 154, 255);

        public Color32 WorkTimeButtonColor => new Color32(255, 35, 50, 255);
        public Color32 BreakTimeButtonColor => new Color32(30, 175, 90, 255);
        public Color32 SpecialBreakTimeButtonColor => new Color32(30, 118, 154, 255);

        private void Awake()
        {
            Current = this;
        }


        public Color32 GetUIColor()
        {
            if (StatusUI.IsMaxCount)
            {
                return SpecialBreakTimeButtonColor;
            }

            return (Timer.CurrentStatus == Timer.Status.Work ? WorkTimeButtonColor : BreakTimeButtonColor);
        }
    }
}