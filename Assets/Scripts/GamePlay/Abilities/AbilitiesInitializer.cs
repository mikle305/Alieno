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
                for (int i = 0; i < entry.UpLevelTimes; i++)
                    _abilitiesEntity.UpLevel(entry.Id);
            });
        }
    }
}