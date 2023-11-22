using UI.GamePlay;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Prefabs Config", fileName = "PrefabsConfig")]
    public class PrefabsConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Character { get; private set; }
        [field: SerializeField] public Hud Hud { get; private set; }
        [field: SerializeField] public GameObject Marker { get; private set; }
    }
}