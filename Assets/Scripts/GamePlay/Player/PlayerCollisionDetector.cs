using System;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
        public event Action PlayerEntered;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerEntered?.Invoke();
            }
        }
    }
}
