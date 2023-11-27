using UnityEngine;

namespace UI.GamePlay
{
    public class Hud : MonoBehaviour
    {
        [field: SerializeField] public CharacteristicView HealthView { get; private set; }
        [field: SerializeField] public CharacteristicView DashView { get; private set; }
    }
}