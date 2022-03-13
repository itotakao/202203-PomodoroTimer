using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Application
{
    public class ApplicationData : MonoBehaviour
    {
        public static ApplicationData Current { get; private set; }

        public float AnimationTimeSec => 0.25f;

        private void Awake()
        {
            Current = this;
        }
    }
}