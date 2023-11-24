using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private float _clearTime = 2f;
    
    private void Start()
    {
        var effect = Instantiate(_effectPrefab, transform.position, transform.rotation);
        
        Destroy(effect,_clearTime);
    }
}
