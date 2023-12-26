using System;
using System.Collections;
using Additional.Game;
using Cysharp.Threading.Tasks;
using UI.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Services
{
    public class SceneLoader
    {
        public event Action<Action> ShowCurtainInvoked;
        public event Action HideCurtainInvoked;
        
        
        public void Load(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }
            
            ShowCurtainInvoked?.Invoke(LoadScene);
            return;

            
            void LoadScene()
                => LoadSceneAsync(nextScene, onLoaded).Forget();
        }

        private async UniTask LoadSceneAsync(string scene, Action onLoaded)
        {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(scene);
            await loadingOperation;
            
            HideCurtainInvoked?.Invoke();
            onLoaded?.Invoke();
        }
    }
}