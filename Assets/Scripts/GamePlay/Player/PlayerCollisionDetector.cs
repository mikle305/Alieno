using System;
using Additional.Constants;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
        private readonly Vector3 _awakeCheckSize = new(4, 10, 4);
        public event Action PlayerEntered;


        private void Start()
        {
            Collider[] colliders = Physics.OverlapBox(transform.position, _awakeCheckSize / 2, transform.rotation, GameConstants.PlayerLayer);
            
            if (colliders.Length != 0)
                PlayerEntered?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
            => PlayerEntered?.Invoke();
    }
}
