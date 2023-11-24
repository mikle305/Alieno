using UnityEngine;

namespace UI.GamePlay
{
    public class Hud : MonoBehaviour
    {
        [field: SerializeField] public CharacteristicHudView HealthView { get; private set; }
        [field: SerializeField] public CharacteristicHudView DashView { get; private set; }
    }
}