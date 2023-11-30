using System;
using System.Collections.Generic;
using GamePlay.Characteristics;
using GamePlay.Statuses;
using GamePlay.UnitsComponents;
using Services.Statuses;
using UnityEngine;

namespace GamePlay.Projectile
{
    public class ProjectileDamage : MonoBehaviour, IDestroy
    {
        private DamageService _damageService;
        private HealthData _sender;
        private float _mainDamage;
        private float _critChance;
        private float _critMultiplier;


        public Dictionary<Type, Status> Statuses { get; } = new();

        public event Action Happened;


        public void Init(HealthData sender, float mainDamage, float critChance, float critMultiplier)
        {
            _critMultiplier = critMultiplier;
            _critChance = critChance;
            _damageService = DamageService.Instance;
            _sender = sender;
            _mainDamage = mainDamage;
        }

        private void OnTriggerEnter(Collider receiver) 
            => HandleReceiver(receiver);

        private void OnDisable()
        {
            Statuses.Clear();
            Happened?.Invoke();
        }

        private void HandleReceiver(Collider receiver)
        {
            receiver.TryGetComponent(out HealthData receiverHealth);
            _damageService.Process(CreateDamageData(receiverHealth));
        }

        private DamageData CreateDamageData(HealthData receiver) 
            => new()
            {
                Sender = _sender,
                Receiver = receiver,
                Projectile = gameObject,
                MainDamage = _mainDamage,
                CritChance = _critChance,
                CritMultiplier = _critMultiplier,
                Statuses = Statuses,
            };
    }
}