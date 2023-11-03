using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "Static Data/App Config", fileName = "AppConfig")]
    public class AppConfig : ScriptableObject
    {
        [field: SerializeField] public NakamaConnectionParams ConnectionParams { get; private set; }
    }
}