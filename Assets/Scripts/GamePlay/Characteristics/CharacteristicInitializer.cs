using UnityEngine;

namespace GamePlay.Characteristics
{
    public class CharacteristicInitializer : MonoBehaviour
    {
        [SerializeField] private Characteristic _characteristic;
        [SerializeField] private float _value;


        private void Awake()
        {
            _characteristic.Init(_value, _value);
        }
    }
}