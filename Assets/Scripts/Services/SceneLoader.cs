using System;
using System.Collections;
using Additional.Game;
using Cysharp.Threading.Tasks;
using UI.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class SceneLoader : MonoSingleton<SceneLoader>
    {
        private LoadingCurtain _loadingCurtain;

        private void Start()
        {
            _loadingCurtain = LoadingCurtain.Instance;
        }

        public void Load(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }
            
            _loadingCurtain.Show(onCurtainShown: LoadScene);
            return;

            
            void LoadScene()
                => LoadSceneAsync(nextScene, onLoaded).Forget();
        }

        private async UniTask LoadSceneAsync(string scene, Action onLoaded)
        {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(scene);
            await loadingOperation;
            
            _loadingCurtain.Hide();
            onLoaded?.Invoke();
        }
    }
}