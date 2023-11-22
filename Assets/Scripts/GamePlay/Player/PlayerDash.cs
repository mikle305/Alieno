using Cysharp.Threading.Tasks;
using GamePlay.Characteristics;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerDash : MonoBehaviour
    {
        [SerializeField] private DashData _dashData;
        [SerializeField] private Rigidbody _playerBody;
        [SerializeField] private LayerMask _obstacleLayer;

        [Header("Trail Settings")]
        [SerializeField] private Material _dashMaterial;
        [SerializeField] private SkinnedMeshRenderer _trailMesh;
        [SerializeField] private float _trailLife = 0.3f;
    
        private Vector3 _dashTarget;
        private RaycastHit _hit;
        
        public bool OnCooldown { get; private set; }
        public bool IsDashing { get; private set; }
        
    
        private void FixedUpdate()
        {
            if (!IsDashing)
                return;
            
            UpdateDash();
        }

        private void OnCollisionEnter(Collision other) 
            => StopOnObstacle(other);

        public void Dash(Vector2 inputDirection)
        {
            if (IsDashing || OnCooldown)
                return;
            
            IsDashing = true;
            SetDashTarget(inputDirection);
        }

        public void Stop()
        {
            StartCooldown().Forget();
            IsDashing = false;
            _dashTarget = Vector3.zero;
        }

        private void UpdateDash()
        {
            SpawnTrailMesh();
            Vector3 currentPosition = transform.position;
            float distSqr = (_dashTarget - currentPosition).sqrMagnitude;
            
            if (distSqr < 0.1f)
            {
                Stop();
                return;
            }

            Vector3 movePosition = Vector3.Lerp(currentPosition, _dashTarget, _dashData.Speed.GetValue() * Time.deltaTime);
            _playerBody.MovePosition(movePosition);
        }

        private void SetDashTarget(Vector2 inputDirection)
        {
            Vector3 position = transform.position;
            if (inputDirection.x != 0 && inputDirection.y != 0)
                inputDirection /= 2;

            var direction = new Vector3(inputDirection.x, 0, inputDirection.y);

            if (Physics.Raycast(position, direction, out _hit, _dashData.Distance.GetValue(), _obstacleLayer))
                _dashTarget = position + (_hit.point - position) / 3.5f;
            else
                _dashTarget = position + direction * _dashData.Distance.GetValue();
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
            Destroy(mesh,_trailLife);
        }

        private void StopOnObstacle(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle") && IsDashing)
                Stop();
        }

        private async UniTask StartCooldown()
        {
            OnCooldown = true;
            await UniTask.WaitForSeconds(_dashData.UseRate.GetValue());
            OnCooldown = false;
        }
    }
}
