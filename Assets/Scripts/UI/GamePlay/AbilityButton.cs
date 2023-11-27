using System;
using GamePlay.Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePlay
{
    public class AbilityButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [field: SerializeField] public TextMeshProUGUI Text { get; private set; }
        
        public AbilityId AbilityId { get; set; }

        public event Action<AbilityButton> ButtonClicked;


        private void Awake()
        {
            _button.onClick.AddListener(InvokeButtonClicked);
        }

        private void InvokeButtonClicked() 
            => ButtonClicked?.Invoke(this);
    }
}