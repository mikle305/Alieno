using Additional.Game;
using GamePlay.Other.Ids;
using Services.ObjectPool;
using UI.GamePlay;
using UnityEngine;

namespace Services.Factories
{
    public class UiFactory : MonoSingleton<UiFactory>
    {
        private ObjectPoolsProvider _objectPoolsProvider;

        
        private void Start()
        {
            _objectPoolsProvider = ObjectPoolsProvider.Instance;
        }

        public DamagePopupView CreateDamagePopup(Vector3 position)
        {
            GameObject damagePopup = _objectPoolsProvider.TakeUiElement(UiElementId.DamagePopup);
            damagePopup.transform.position = position;
            damagePopup.transform.rotation = Quaternion.identity;
            
            return damagePopup.GetComponent<DamagePopupView>();
        }
    }
}