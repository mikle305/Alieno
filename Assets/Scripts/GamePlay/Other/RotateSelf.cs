using UnityEngine;

namespace GamePlay.Other
{
    public class RotateSelf : MonoBehaviour
    {
        [SerializeField] float _speed = 50f;

        private void Update()
        {
            transform.Rotate (Vector3.up * (_speed * Time.deltaTime), Space.Self);
        }
    }
}
