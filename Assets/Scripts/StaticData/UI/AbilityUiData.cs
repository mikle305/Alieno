using System;
using GamePlay.Abilities;
using UnityEngine;

namespace StaticData.UI
{
    [Serializable]
    public class AbilityUiData
    {
        [field: SerializeField] public AbilityId Id { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    }
}