using Additional.Game;
using DG.Tweening;
using UnityEngine;

namespace Services
{
    public class FpsService : MonoSingleton<FpsService>
    {
        protected override void Awake()
        {
            base.Awake();
            
            SetBackgroundLoadingPriority();
            SetTweensPoolCount();
            Set60TargetFps();
        }

        private void Set60TargetFps()
        {
#if !UNITY_WEBGL && !UNITY_EDITOR
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
#endif
        }

        private static void SetBackgroundLoadingPriority() 
            => Application.backgroundLoadingPriority = ThreadPriority.Low;

        private static void SetTweensPoolCount() 
            => DOTween.SetTweensCapacity(tweenersCapacity: 500, sequencesCapacity: 125);
    }
}