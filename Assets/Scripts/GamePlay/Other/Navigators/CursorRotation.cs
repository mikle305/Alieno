using UnityEngine;

namespace GamePlay.Other.Navigators
{
    public class CursorRotation : MonoBehaviour
    {
        [SerializeField] float _rotationSpeed = 15;

        void Update()
        {
            transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
        }
    }
}
