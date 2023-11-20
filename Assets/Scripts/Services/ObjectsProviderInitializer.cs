using Additional.Game;
using UI.GamePlay;
using UnityEngine;

namespace Services
{
    public class ObjectsProviderInitializer : MonoSingleton<ObjectsProviderInitializer>
    {
        [SerializeField] private Hud _hud;
        [SerializeField] private GameObject _character;
        
        private ObjectsProvider _objectsProvider;

        
        private void Start()
        {
            _objectsProvider = ObjectsProvider.Instance;
            _objectsProvider.Hud = _hud;
            _objectsProvider.Character = _character;
        }
    }
}