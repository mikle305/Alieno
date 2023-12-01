using System;
using GamePlay.Projectile;

namespace GamePlay.Abilities
{
    public abstract class AbilityComponent
    {
        public abstract AbilityId AbilityId { get; }
        public int CurrentLevelId { get; protected set; }
        public abstract void Init(AbilitiesEntity entity, AbilityData data, int level);
        public abstract void SetLevel(int level);

        public virtual void OnTick() { }
        public virtual void OnShotCalled() { }
        public virtual void OnShotDone(ProjectileDamage projectile) { }
        public virtual void OnDestroy() { }
    }

    public abstract class AbilityComponent<TData, TLevelData> : AbilityComponent 
        where TData : AbilityData<TLevelData> 
        where TLevelData : AbilityLevelData, new()
    {
        private TData _data;

        protected TLevelData CurrentLevel => _data.Levels[CurrentLevelId - 1];
        protected AbilitiesEntity Entity { get; private set; }
        
        protected virtual void OnCreate() { }
        protected virtual void OnLevelChanged() { }
        
        public sealed override AbilityId AbilityId => _data.Id;


        public sealed override void Init(AbilitiesEntity entity, AbilityData data, int level)
        {
            Entity = entity;
            _data = data as TData;
            
            if (_data!.MaxLevel < level)
                throw new ArgumentOutOfRangeException();
            
            CurrentLevelId = level;
            OnCreate();
        }

        public sealed override void SetLevel(int level)
        {
            if (_data.MaxLevel < level)
                throw new ArgumentOutOfRangeException();
                
            CurrentLevelId = level;
            OnLevelChanged();
        }
    }
}