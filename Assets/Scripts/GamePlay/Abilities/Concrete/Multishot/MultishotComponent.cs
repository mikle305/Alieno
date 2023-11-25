using System.Collections.Generic;
using System.Linq;
using Additional.Extensions;
using Cysharp.Threading.Tasks;

namespace GamePlay.Abilities
{
    public class MultishotComponent : AbilityComponent<MultishotData, MultishotLevelData>
    {
        public override void OnCall()
            => RecallShotComponents().Forget();

        private async UniTask RecallShotComponents()
        {
            AbilityComponent[] abilities = Entity
                .AbilitiesMap
                .Where(CheckShotComponent)
                .Select(entry => entry.Value)
                .ToArray();

            for (var i = 0; i < CurrentLevel.AdditionsCount; i++)
            {
                await UniTask.WaitForSeconds(CurrentLevel.ShotDelay, delayTiming: PlayerLoopTiming.FixedUpdate);
                abilities.ForEach(a => a.OnCall());
            }
        }

        private bool CheckShotComponent(KeyValuePair<AbilityId, AbilityComponent> abilityEntry) 
            => CurrentLevel.ApplicableAbilities.Contains(abilityEntry.Key);
    }
}