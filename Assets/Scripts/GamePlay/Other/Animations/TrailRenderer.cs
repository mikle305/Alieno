using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Other.Animations
{
    public class TrailRenderer : MonoBehaviour
    {
        [FormerlySerializedAs("_dashMaterial")]
        [Header("Trail Settings")]
        [SerializeField] private Material _trailMaterial;
        [SerializeField] private SkinnedMeshRenderer _trailMesh;
        [SerializeField] private float _trailLife = 0.1f;
        [SerializeField] private Transform _playerBody;


        private void Update() 
            => SpawnTrailMesh();

        private void SpawnTrailMesh()
        {
            GameObject go = new GameObject("Trail");
            go.transform.SetPositionAndRotation(_playerBody.position,_playerBody.rotation);

            MeshRenderer mr = go.AddComponent<MeshRenderer>();
            MeshFilter mf = go.AddComponent<MeshFilter>();

            Mesh mesh = new Mesh();
            _trailMesh.BakeMesh(mesh);

            mf.mesh = mesh;
            mr.material = _trailMaterial;
        
            Destroy(go,_trailLife);
        }
    }
}
