using StaticData.Abilities;

namespace GamePlay.Abilities
{
    public abstract class AbilityComponent
    {
        public abstract void InitData(AbilityData abilityData);
    }

    public abstract class AbilityComponent<TData> : AbilityComponent
        where TData : AbilityData
    {
        private TData _data;
        
        public override sealed void InitData(AbilityData abilityData)
            => _data = abilityData as TData;
    }
}