using Additional.Game;
using TMPro;
using UnityEngine;

namespace UI.WindowsInfo
{
    public class EmailResetInfo : MonoSingleton<EmailResetInfo>
    {
        [SerializeField] private TMP_InputField _emailField;
        
        public string Email => _emailField.text;
    }
}