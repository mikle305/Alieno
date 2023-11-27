using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay.Characteristics;
using GamePlay.Player;
using GamePlay.UnitsComponents;
using UnityEngine;
using UnityEngine.Serialization;

public class EntityOnDeathEffect : MonoBehaviour
{
    [Header("Obligatory Component")]
    [SerializeField] private Death _deathComponent;
    
    [Header("Mesh Effect Settings")]
    [SerializeField] private DissolvePrefab _dissolvePrefab;
    [SerializeField] private List<MeshFilter> _meshFilters;
    [SerializeField] private List<SkinnedMeshRenderer> _skinnedMeshRenderers;
    
    [Header("SpawnEffect Settings")]
    [SerializeField] private GameObject _effectToSpawn;
    
    [Header("Shared settings")]
    [SerializeField] private float _lifetime = 1f;
    [SerializeField] private float _addScale = 1.05f;

    private void Start()
    {
        _deathComponent.Happened += DeathEffects;
    }

    private void DeathEffects()
    {
        SpawnMeshes();
        SpawnSkinnedMeshes();
        SpawnVisualEffects();
    }

    private void OnDestroy()
    {
        _deathComponent.Happened -= DeathEffects;
    }

    private void SpawnMeshes()
    {
        foreach (var meshRenderer in _meshFilters)
        {
            var trail = Instantiate(_dissolvePrefab,meshRenderer.transform.position, meshRenderer.transform.rotation);
           
            trail.StartDissolving(_lifetime, meshRenderer.GetComponent<MeshRenderer>().material.mainTexture);
            trail.MeshFilter.mesh = meshRenderer.mesh;
            trail.transform.localScale = meshRenderer.transform.lossyScale * _addScale;
            Destroy(trail.gameObject,_lifetime);
        }
    }

    private void SpawnSkinnedMeshes()
    {
        foreach (var meshRenderer in _skinnedMeshRenderers)
        {
            var trail = Instantiate(_dissolvePrefab,meshRenderer.transform.position, meshRenderer.transform.rotation);

            Mesh mesh = new Mesh();
            meshRenderer.BakeMesh(mesh);
           trail.StartDissolving(_lifetime, meshRenderer.material.mainTexture);
            trail.MeshFilter.mesh = mesh;
            trail.transform.localScale = meshRenderer.transform.lossyScale * _addScale;
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
