using GamePlay.Characteristics;
using Services.Factories;
using UnityEngine;

namespace UI.GamePlay
{
    public class SpawnPopupOnHealth : MonoBehaviour
    {
        [SerializeField] private HealthData _healthData;
        [SerializeField] private Color _damageColor = Color.red;
        [SerializeField] private Color _healColor = Color.green;
        
        private UiFactory _uiFactory;

        
        private void Start()
        {
            _uiFactory = UiFactory.Instance;
            _healthData.IncreaseTried += OnIncreased;
            _healthData.DecreaseTried += OnDecreased;
        }

        private void OnDestroy()
        {
            _healthData.IncreaseTried -= OnIncreased;
            _healthData.DecreaseTried -= OnDecreased;
        }

        private void OnIncreased(float value)
            => CreatePopup(value, _healColor);

        private void OnDecreased(float value)
            => CreatePopup(value, _damageColor);

        private void CreatePopup(float value, Color color)
        {
            print(gameObject.name);
            DamagePopupView view = _uiFactory.CreateDamagePopup(transform.position);
            view.Play(value, color);
        }
    }
}