using UnityEngine;

namespace GamePlay.Other
{
    public class DirectionArrow : MonoBehaviour
    {
        private Transform _target;

        public void SetTarget(Transform target)
            => _target = target;
        
        private void Update()
        {
            if (_target == null)
                return;

            Vector3 targetPosition = _target.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }
    }
}
