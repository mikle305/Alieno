using Additional.Game;
using Services.Notifications;
using TMPro;
using UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class ErrorPopup : MonoSingleton<ErrorPopup>
    {
        [SerializeField] private Window _popupWindow;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _invisibleBackground;
        [SerializeField] private Button _confirmButton;

        private MessageNotifier _messageNotifier;

        
        private void Start()
        {
            _messageNotifier = MessageNotifier.Instance;
            _messageNotifier.NotificationHappened += ShowPopup;
            _confirmButton.onClick.AddListener(HidePopup);
        }

        private void ShowPopup(string message)
        {
            _text.text = message;
            _invisibleBackground.SetActive(true);
            _popupWindow.Toggle(ToggleMode.Open);
        }

        private void HidePopup()
        {
            _text.text = string.Empty;
            _invisibleBackground.SetActive(false);
            _popupWindow.Toggle(ToggleMode.Close);
        }
    }
}