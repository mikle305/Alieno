using Additional.Extensions;
using Cysharp.Threading.Tasks;
using SaveData;
using UnityEngine;

namespace GamePlay.Abilities
{
    public class AbilitiesInitializer : MonoBehaviour
    {
        [SerializeField] private AbilitiesEntity _abilitiesEntity;
        [SerializeField] private AbilityEntry[] _defaultAbilities;


        private void Start() 
            => InitDefaultAbilities().Forget();

        private async UniTask InitDefaultAbilities()
        {
            await UniTask.Yield();
            _defaultAbilities.ForEach(entry =>
            {
                _abilitiesEntity.AddAbility(entry.Id);
                for (var i = 1; i < entry.Level; i++)
                    _abilitiesEntity.UpLevel(entry.Id);
            });
        }
    }
}