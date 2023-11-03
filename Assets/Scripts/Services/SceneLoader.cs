using System;
using System.Collections;
using Additional.Game;
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
            
            _loadingCurtain.Enable(onCurtainShown: () => LoadScene(nextScene, onLoaded));
        }

        private void LoadScene(string scene, Action onLoaded) 
            => StartCoroutine(LoadingCoroutine(scene, onLoaded));

        private IEnumerator LoadingCoroutine(string scene, Action onLoaded)
        {
            AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(scene);

            while (!sceneLoading.isDone)
                yield return null;
            
            _loadingCurtain.Disable();
            onLoaded?.Invoke();
        }
    }
}