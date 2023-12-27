using Services;
using UnityEngine;
using VContainer;

namespace GamePlay.Projectile
{
    public class HomingProjectile : MonoBehaviour
    {
        [SerializeField] private float _turnSpeed = 15f;
        [SerializeField] private Rigidbody _projRigid;
        
        private ObjectsProvider _objectsProvider;


        [Inject]
        public void Construct(ObjectsProvider objectsProvider)
        {
            _objectsProvider = objectsProvider;
        }
    
        private void FixedUpdate()
        {
            RotateToPlayer(_objectsProvider.Character);
        }
    
        private void RotateToPlayer(GameObject target)
        {
            if (target == null)
                return;
            
            Quaternion originalRotation = transform.rotation;
            transform.LookAt(target.transform.position+new Vector3(0,0.3f,0));
            Quaternion newRotation = transform.rotation;

            transform.rotation = Quaternion.Lerp(originalRotation, newRotation, _turnSpeed * Time.deltaTime);
            _projRigid.AddForce(_projRigid.transform.forward*5.5f,ForceMode.Impulse);
        }
    }
}
