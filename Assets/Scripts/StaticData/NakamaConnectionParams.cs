using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "Static Data/Nakama Connection", fileName = "NewConnectionParams")]
    public class NakamaConnectionParams : ScriptableObject
    {
        [field: SerializeField] public string Scheme { get; private set; }
        [field: SerializeField] public string Host { get; private set; }
        [field: SerializeField] public int Port { get; private set; }
        [field: SerializeField] public string ServerKey { get; private set; }
    }
}