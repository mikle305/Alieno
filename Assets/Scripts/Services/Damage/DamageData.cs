using System;
using System.Collections.Generic;
using GamePlay.Characteristics;
using UnityEngine;

namespace Services
{
    public class DamageData
    {
        public float MainDamage { get; set; }
        public HealthData Sender { get; set; }
        public HealthData Receiver { get; set; }
        public GameObject Projectile { get; set; }
        public Dictionary<Type, Status> Statuses { get; set; }
    }
}