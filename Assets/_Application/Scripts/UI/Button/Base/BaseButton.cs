using UnityEngine;
using UnityEngine.UI;

namespace _Application
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        protected void OnValidate()
        {
#if UNITY_EDITOR
            if (!button)
            {
                button = GetComponent<Button>();
                UnityEditor.Events.UnityEventTools.RemovePersistentListener(button.onClick, OnPressedButton);
                UnityEditor.Events.UnityEventTools.AddPersistentListener(button.onClick, OnPressedButton);
            }
#endif
        }

        protected abstract void OnPressedButton();
    }
}