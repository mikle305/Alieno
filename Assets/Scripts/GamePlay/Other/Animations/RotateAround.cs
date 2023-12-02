using UnityEngine;

namespace GamePlay.Other.Animations
{
    public class RotateAround : MonoBehaviour
    {
        [SerializeField] GameObject target;
        [SerializeField] private float _speed = 1f;
        void Update()
        {
            transform.RotateAround(target.transform.position, Vector3.up, _speed * Time.deltaTime);
        }
    }
}