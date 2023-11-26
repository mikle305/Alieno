using System;
using System.Collections.Generic;
using GamePlay.Characteristics;
using GamePlay.Damage;
using GamePlay.UnitsComponents;
using Services.Damage;
using UnityEngine;

namespace GamePlay.Projectile
{
    public class ProjectileDamage : MonoBehaviour, IDestroy
    {
        private DamageService _damageService;
        private HealthData _sender;
        private float _damage;

        public Dictionary<Type, Status> Statuses { get; } = new();

        public event Action Happened;


        public void Init(HealthData sender, float damage)
        {
            _damageService = DamageService.Instance;
            _sender = sender;
            _damage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            SendDamage(other.gameObject);
        }

        private void OnDisable()
        {
            Statuses.Clear();
            Happened?.Invoke();
        }

        private void SendDamage(GameObject receiver)
        {
            receiver.TryGetComponent(out HealthData receiverHealth);
            _damageService.Process(CreateDamageData(receiverHealth));
        }

        private DamageData CreateDamageData(HealthData receiverHealth) 
            => new()
            {
                Sender = _sender,
                Receiver = receiverHealth,
                Projectile = gameObject,
                MainDamage = _damage,
                Statuses = Statuses,
            };
    }
}