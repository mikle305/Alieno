using System;

namespace GamePlay.Abilities
{
    public abstract class AbilityComponent
    {
        public abstract AbilityId AbilityId { get; }
        public abstract int CurrentLevelId { get; protected set; }
        public abstract void Init(AbilitiesEntity entity, AbilityData data);
        public abstract void UpLevel();

        public virtual void OnTick() { }
        public virtual void OnCall() { }
        public virtual void OnDestroy() { }
    }

    public abstract class AbilityComponent<TData, TLevelData> : AbilityComponent 
        where TData : AbilityData<TLevelData> 
        where TLevelData : AbilityLevelData, new()
    {
        private TData _data;

        protected TLevelData CurrentLevel => _data.Levels[CurrentLevelId - 1];
        protected AbilitiesEntity Entity { get; private set; }

        public sealed override int CurrentLevelId { get; protected set; }
        public sealed override AbilityId AbilityId => _data.Id;
        

        public sealed override void Init(AbilitiesEntity entity, AbilityData data)
        {
            Entity = entity;
            _data = data as TData;
            CurrentLevelId = 1;
            OnCreate();
        }

        public sealed override void UpLevel()
        {
            if (_data.Levels.Length <= CurrentLevelId)
                throw new ArgumentOutOfRangeException();
                
            CurrentLevelId++;
            OnLevelUp();
        }

        protected virtual void OnCreate() { }
        protected virtual void OnLevelUp() { }
    }
}