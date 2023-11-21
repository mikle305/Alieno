using Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Player
{
    public class PlayerDash : MonoBehaviour
    {
        [Header("Dash Settings")]
        [SerializeField] private Rigidbody _playerBody;
        [SerializeField] private float _dashDistance = 5f;
        [SerializeField] private float _dashSpeed = 5f;
        [SerializeField] private LayerMask _obstacleLayer;

        [Header("Trail Settings")]
        [SerializeField] private Material _dashMaterial;
        [FormerlySerializedAs("_trailRenderer")] [SerializeField] private SkinnedMeshRenderer _trailMesh;
        [SerializeField] private float _trailLife = 0.3f;
    
        private float _horizontal;
        private float _vertical;

        [FormerlySerializedAs("_isDashing")] public bool IsDashing;
        private PlayerMovement _movementComponent;
        private Vector3 _dashTarget;
        private RaycastHit hit;
        private void Awake()
        {
            _movementComponent = GetComponent<PlayerMovement>();
        }
        
        private void Start()
        {
            GameService.Instance.OnRoomFinish += StopDashing;
        }
        void Update ()
        {
            if (Input.GetKeyDown("space") && !IsDashing)
            {
                _movementComponent.ClearVelocity();
                Dash(_dashDistance);
            }
        }
    
        private void FixedUpdate()
        {
            if (IsDashing)
            {
                SpawnTrailMesh();
                _playerBody.velocity = Vector2.zero;
 
                float distSqr = (_dashTarget - transform.position).sqrMagnitude;
                if (distSqr < 0.1f)
                {
                    StopDashing();
                    // owner.Vitals.IsInvincible = false;
                }
                else
                {
                    _playerBody.MovePosition(Vector3.Lerp(transform.position,
                        _dashTarget, _dashSpeed * Time.deltaTime));
                }
            }
        }
        public void Dash(float dashDistance)
        {
        
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
            
            var direction = new Vector3(_horizontal, 0,_vertical);
            if (direction.x != 0 && direction.z != 0)
                direction /= 2;
            
            IsDashing = true;

            if (Physics.Raycast(transform.position, direction, out hit, dashDistance,_obstacleLayer))
            {
                _dashTarget =  transform.position + (hit.point - transform.position) / 3.5f;
            }
            else
            {
                _dashTarget = transform.position + (Vector3)(direction * dashDistance);
            }
        }

        public void StopDashing()
        {
            IsDashing = false;
            _dashTarget = Vector2.zero;
        }

        private void SpawnTrailMesh()
        {
            GameObject go = new GameObject("Trail");
            go.transform.SetPositionAndRotation(_playerBody.position,_playerBody.rotation);
            MeshRenderer mr = go.AddComponent<MeshRenderer>();
            MeshFilter mf = go.AddComponent<MeshFilter>();

            Mesh mesh = new Mesh();
            _trailMesh.BakeMesh(mesh);

            mf.mesh = mesh;
            mr.material = _dashMaterial;
        
            Destroy(go,_trailLife);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle") && IsDashing)
            {
                StopDashing();
            }
        }
    }
}
