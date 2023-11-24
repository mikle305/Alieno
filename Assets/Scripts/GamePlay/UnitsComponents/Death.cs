using System;
using Additional.Extensions;
using GamePlay.Characteristics;
using UnityEngine;

namespace GamePlay.UnitsComponents
{
    public class Death : MonoBehaviour, IDestroy
    {
        [SerializeField] private HealthData _health;

        public event Action Happened;


        private void Awake()
        {
            _health.ZeroReached += Die;
        }

        private void OnDestroy()
        {
            _health.ZeroReached -= Die;
        }

        private void Die()
        {
            Happened?.Invoke();
            this.Dispose();
        }
    }
}