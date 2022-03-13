using UnityEngine;
using Audio;
namespace _Application
{
    public class SetHomeStatusButtton : BaseButton
    {
        private AudioManager AudioManager => AudioManager.Current;
        private HomeState HomeState => HomeState.Current;

        [SerializeField]
        private HomeState.Status selectStatus = HomeState.Status.None;

        protected override void OnPressedButton()
        {
            if (HomeState.Current == null)
            {
                Debug.LogError("HomeStateが存在しません。");
                return;
            }

            if (selectStatus == HomeState.Status.None)
            {
                Debug.LogError("selectStatusのステータスはNone以外に設定してください。");
                return;
            }

            AudioManager.SE.Play("button");
            HomeState.SetStatus(selectStatus);
        }
    }
}