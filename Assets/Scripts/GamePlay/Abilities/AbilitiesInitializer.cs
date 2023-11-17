using Additional.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GamePlay.Abilities
{
    public class AbilitiesInitializer : MonoBehaviour
    {
        [SerializeField] private AbilitiesEntity _abilitiesEntity;
        [SerializeField] private AbilityId[] _defaultAbilities;


        private async UniTaskVoid Start()
        {
            await UniTask.Yield();
            _defaultAbilities.ForEach(_abilitiesEntity.AddAbility);
        }
    }
}