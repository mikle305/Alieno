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
                .Where(entry => entry.Key != AbilityId)
                .Select(entry => entry.Value)
                .ToArray();

            for (var i = 0; i < CurrentLevel.AdditionsCount; i++)
            {
                await UniTask.WaitForSeconds(CurrentLevel.ShotDelay);
                abilities.ForEach(a => a.OnCall());
            }
        }
    }
}