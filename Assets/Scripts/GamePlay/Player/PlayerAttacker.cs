using Cysharp.Threading.Tasks;
using GamePlay.Abilities;
using GamePlay.Characteristics;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Player
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private AbilitiesEntity _abilitiesEntity;
        [FormerlySerializedAs("_attack")] [SerializeField] private ProjectileAttack _projectileAttack;

        private bool _onCooldown;


        private void Update() 
            => Attack();

        public void Attack()
        {
            if (_onCooldown)
                return;
            
            _abilitiesEntity.Call();
            StartCooldown().Forget();
        }

        private async UniTask StartCooldown()
        {
            _onCooldown = true;
            await UniTask.WaitForSeconds(_projectileAttack.UseRate.GetValue());
            _onCooldown = false;
        }
    }
}