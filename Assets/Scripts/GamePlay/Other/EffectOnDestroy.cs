using System;
using UnityEngine;

public class EffectOnDestroy : MonoBehaviour
{
    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private float _clearTime = 2f;
    
    private void OnDestroy()
    {
        var effect = Instantiate(_effectPrefab, transform.position, transform.rotation);
        
        Destroy(effect,_clearTime);
    }
}
