using System;
using UnityEngine;

namespace GamePlay.UnitsComponents
{
    public class EffectOnSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject _effectPrefab;
        [SerializeField] private float _clearTime = 2f;


        private void OnEnable() 
            => SpawnEffect();

        private void SpawnEffect()
        {
            GameObject effect = Instantiate(_effectPrefab, transform.position, transform.rotation);
            Destroy(effect, _clearTime);
        }
    }
}
