using GamePlay.Other.Ids;
using Services.ObjectPool;
using UI.GamePlay;
using UnityEngine;

namespace Services.Factories
{
    public class UiFactory
    {
        private readonly ObjectPoolsProvider _poolsProvider;


        public UiFactory(ObjectPoolsProvider poolsProvider)
        {
            _poolsProvider = poolsProvider;
        }

        public DamagePopupView CreateDamagePopup(Vector3 position)
        {
            GameObject damagePopup = _poolsProvider.TakeUiElement(UiElementId.DamagePopup);
            damagePopup.transform.position = position;
            damagePopup.transform.rotation = Quaternion.identity;
            return damagePopup.GetComponent<DamagePopupView>();
        }
    }
}