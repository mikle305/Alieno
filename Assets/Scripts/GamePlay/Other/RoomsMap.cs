using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Other
{
    public class RoomsMap : MonoBehaviour
    {
        [field: SerializeField] public Button NextLvlButton { get; private set; }
        [field: SerializeField] public Toggle AutoSkipToggle { get; private set; }
        [field: SerializeField] public Transform Pointer { get; private set; }
        [field: SerializeField] public List<Transform> LevelNumbers { get; private set; }
    }
}