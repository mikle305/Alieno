using System;
using UnityEngine;

namespace GamePlay.Characteristics
{
    public class CharacteristicInitializer : MonoBehaviour
    {
        [SerializeField] private Characteristic _characteristic;
        [SerializeField] private float _value;


        private void Start()
        {
            _characteristic.Init(_value, _value);
        }
    }
}