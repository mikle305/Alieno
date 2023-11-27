using UnityEngine;

namespace GamePlay.Other
{
    public class DirectionArrow : MonoBehaviour
    {
        [SerializeField] private Transform _exitPoint;
        private void Update()
        {
            if (_exitPoint == null)
            {
                TryFindExit();
                return;
            }
        
            transform.LookAt(_exitPoint);
        }

        private void TryFindExit()
        {
            _exitPoint = GameObject.Find("ExitPoint").transform;
        }
    }
}
