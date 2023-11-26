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


        private void OnEnable()
        {
            _health.ZeroReached += Die;
        }

        private void OnDisable()
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