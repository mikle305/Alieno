using Additional.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GamePlay.Abilities
{
    public class AbilitiesInitializer : MonoBehaviour
    {
        [SerializeField] private AbilitiesEntity _abilitiesEntity;
        [SerializeField] private DefaultAbilityEntry[] _defaultAbilities;


        private void Start() 
            => InitDefaultAbilities().Forget();

        private async UniTask InitDefaultAbilities()
        {
            await UniTask.Yield();
            _defaultAbilities.ForEach(entry =>
            {
                _abilitiesEntity.AddAbility(entry.Id);
                if (!_abilitiesEntity.AbilitiesMap.ContainsKey(entry.Id))
                    return;
                    
                for (var i = 1; i < entry.Level; i++)
                    _abilitiesEntity.UpLevel(entry.Id);
            });
        }
    }
}