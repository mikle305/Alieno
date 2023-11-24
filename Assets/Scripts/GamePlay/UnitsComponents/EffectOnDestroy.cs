using UnityEngine;

namespace GamePlay.UnitsComponents
{
    public class EffectOnDestroy : MonoBehaviour
    {
        [SerializeField] private GameObject _effectPrefab;
        [SerializeField] private float _clearTime = 2f;
        
        private IDestroy _destroy;


        private void Awake()
        {
            _destroy = GetComponent<IDestroy>();
            _destroy.Happened += SpawnEffect;
        }

        private void SpawnEffect()
        {
            GameObject effect = Instantiate(_effectPrefab, transform.position, transform.rotation);
            Destroy(effect,_clearTime);
        }
    }
}
