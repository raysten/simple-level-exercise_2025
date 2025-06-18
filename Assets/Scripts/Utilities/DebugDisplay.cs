using TMPro;
using UnityEngine;

namespace Utilities
{
    public class DebugDisplay : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _textField;

        public void ShowMessage(string message)
        {
            _textField.SetText(message);
        }
    }
}
