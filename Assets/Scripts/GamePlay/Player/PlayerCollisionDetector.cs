using System;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
        private Action _functionToCall;
    
        
        public void InitCollisionTrigger(Action func)
        {
            _functionToCall = func;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _functionToCall.Invoke();
            }
        }
    }
}
