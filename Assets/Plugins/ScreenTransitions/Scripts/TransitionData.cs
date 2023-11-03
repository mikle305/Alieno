using System;
using DG.Tweening;
using UnityEngine;

namespace ScreenTransitions
{
    [Serializable]
    public class TransitionData
    {
        [field: SerializeField] public float Duration { get; private set; } = 0.8f;
        [field: SerializeField] public Ease Ease { get; private set; } = Ease.Linear;
    }
}