using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GamePlay.Characteristics
{
    public class CharacteristicsCollection : MonoBehaviour
    {
        [SerializeField] private CharacteristicEntry[] _initialCharacteristics;
        
        private Dictionary<CharacteristicId, Characteristic> _characteristicsMap;


        private void Awake()
        {
            InitCharacteristics(_initialCharacteristics);
        }
        
        public bool TryGet(CharacteristicId id, out Characteristic characteristic)
            => _characteristicsMap.TryGetValue(id, out characteristic);
        
        private void InitCharacteristics(CharacteristicEntry[] characteristics)
        {
            _characteristicsMap = characteristics.ToDictionary(
                entry => entry.Id,
                entry => new Characteristic(entry.Value, entry.Value));
        }
    }
}