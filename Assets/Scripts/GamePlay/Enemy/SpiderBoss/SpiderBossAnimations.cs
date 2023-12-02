using System;
using UnityEngine;

namespace GamePlay.Enemy
{
    public class SpiderBossAnimations : MonoBehaviour
    {
        [SerializeField] private GameObject _onLandingEffect;
        [SerializeField] private float _effctClearTime = 1f;
    
        public Action JumpAnimationFinished;
        public Action AttackAnimationFinished;

    
        public void InvokeJumpAnimationFinish()
        {
            JumpAnimationFinished?.Invoke();
            GameObject effect = Instantiate(_onLandingEffect, transform.position, Quaternion.identity);
            Destroy(effect,_effctClearTime);
        }
    
        public void InvokeAttackAnimationFinish()
        {
            AttackAnimationFinished?.Invoke();
        }
    }
}
