using UnityEngine;
using Audio;

namespace _Application
{
    public class PlayButtonSE : MonoBehaviour
    {
        private AudioManager AudioManager => AudioManager.Current;

        public void PlaySE()
        {
            AudioManager.SE.Play("button");
        }
    }
}