using UnityEngine;
using TMPro;

namespace _Application
{
    public class ValidateInputField : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField inputField;
        public void OnValidate()
        {
            if (int.TryParse(inputField.text, out int num))
            {
                int clamp = Mathf.Clamp(num, 1, 99);
                inputField.text = clamp.ToString();
            }
            else
            {
                inputField.text = (1).ToString();
            }
        }
    }
}