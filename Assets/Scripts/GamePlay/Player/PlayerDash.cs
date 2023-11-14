using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

    private bool _isDashing;
    private Vector3 _dashTarget;

    private void Start()
    {
        
    }
    void Update ()
    {
        if (Input.GetKeyDown("space") && !_isDashing)
        {
            print("Dash Start");
            Dash(_dashDistance);
        }

    }
    
    private void FixedUpdate()
    {
        if (_isDashing)
        {
            SpawnTrailMesh();
            _playerBody.velocity = Vector2.zero;
 
            float distSqr = (_dashTarget - transform.position).sqrMagnitude;
            if (distSqr < 0.1f)
            {
                OnDashFinish();
                _dashTarget = Vector2.zero;
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
        // owner.Movement.Velocity.normalized
        
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        var direction = new Vector3(_horizontal, 0,_vertical);
        
        _isDashing = true;

        var hit = Physics.Raycast(transform.position, direction, dashDistance, _obstacleLayer);
 
        if (hit)
        {
            print("Obstacle hit");
            
            // _dashTarget = transform.position + (Vector3)((direction * dashDistance) * hit.fraction);
            _dashTarget = transform.position + (Vector3)((direction * dashDistance));
        }
        else
        {
            _dashTarget = transform.position + (Vector3)(direction * dashDistance);
        }
    }

    public void OnDashFinish()
    {
        _isDashing = false;
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
}
