using Additional.Game;
using UnityEngine;

namespace Services
{
    public class FpsService : MonoSingleton<FpsService>
    {
        protected override void Awake()
        {
            base.Awake();
            
            SetBackgroundLoadingPriority();
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
        {
            Application.backgroundLoadingPriority = ThreadPriority.Low;
        }
    }
}