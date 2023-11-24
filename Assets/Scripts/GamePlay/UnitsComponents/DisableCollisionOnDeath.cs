using UnityEngine;

namespace GamePlay.UnitsComponents
{
    public class NonCollisionOnDeath : MonoBehaviour
    {
        private IDestroy _destroy;
        private Collider[] _colliders;

        
        private void Awake()
        {
            _colliders = transform.GetComponents<Collider>();
            _destroy = GetComponent<IDestroy>();
            _destroy.Happened += DisableColliders;
        }

        private void OnDestroy()
        {
            _destroy.Happened -= DisableColliders;
        }

        private void DisableColliders()
        {
            foreach (Collider col in _colliders) 
                col.enabled = false;
        }
    }
}