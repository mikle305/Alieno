using System;
using System.Collections.Generic;
using GamePlay.Characteristics;
using UnityEngine;

namespace GamePlay.Damage
{
    public class DamageData
    {
        public float MainDamage { get; set; }
        public HealthData Sender { get; set; }
        
        /// <summary>
        /// Must be null if obstacle
        /// </summary>
        public HealthData Receiver { get; set; }
        
        public GameObject Projectile { get; set; }
        public Dictionary<Type, Status> Statuses { get; set; }
    }
}