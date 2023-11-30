using System.Collections.Generic;
using GamePlay.Characteristics;
using GamePlay.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.UnitsComponents
{
    public class EntityOnHitEffect : MonoBehaviour
    {
        [Header("Obligatory Component")]
        [SerializeField] private HealthData _healthData;
    
        [Header("Mesh Effect Settings")]
        [SerializeField] private TrailPrefab _trailPrefab;
        [FormerlySerializedAs("_skinnedMeshRenderers")] [SerializeField] private List<MeshFilter> _meshFilters;
        [SerializeField] private List<SkinnedMeshRenderer> _skinnedMeshRenderers;
        [SerializeField] private Material _meshMaterial;
    
        [Header("SpawnEffect Settings")]
        [SerializeField] private GameObject _effectToSpawn;
    
        [Header("Shared settings")]
        [SerializeField] private float _lifetime = 0.2f;
        [SerializeField] private float _addScale = 1.05f;

        private void Start()
        {
            _healthData.ValueChanged += OnHitEffects;
        }

        private void OnHitEffects()
        {
            SpawnMeshes();
            SpawnSkinnedMeshes();
            SpawnVisualEffects();
        }

        private void OnDestroy()
        {
            _healthData.ValueChanged -= OnHitEffects;
        }

        private void SpawnMeshes()
        {
            foreach (var meshRenderer in _meshFilters)
            {
                var trail = Instantiate(_trailPrefab,meshRenderer.transform.position, meshRenderer.transform.rotation,meshRenderer.transform);

                trail.MeshRenderer.material = _meshMaterial;
                trail.MeshFilter.mesh = meshRenderer.mesh;
                trail.transform.localScale = meshRenderer.transform.localScale * _addScale;
                Destroy(trail.gameObject,_lifetime);
                // Destroy(mesh,_lifetime);
            }
        }

        private void SpawnSkinnedMeshes()
        {
            foreach (var meshRenderer in _skinnedMeshRenderers)
            {
                var trail = Instantiate(_trailPrefab,meshRenderer.transform.position, meshRenderer.transform.rotation,meshRenderer.transform);

                Mesh mesh = new Mesh();
                meshRenderer.BakeMesh(mesh);
            
                trail.MeshRenderer.material = _meshMaterial;
                trail.MeshFilter.mesh = mesh;
                trail.transform.localScale = meshRenderer.transform.localScale * _addScale;
                Destroy(trail.gameObject,_lifetime);
                Destroy(mesh,_lifetime);
            }
        }
    
        private void SpawnVisualEffects()
        {
            if(_effectToSpawn == null)
                return;

            var go = Instantiate(_effectToSpawn, transform.position, Quaternion.identity);
            Destroy(go,_lifetime);
        }
    }
}
