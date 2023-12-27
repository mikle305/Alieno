using Services;
using UnityEngine;
using VContainer;

namespace UI.Loading
{
    public class LoadingPresenter : MonoBehaviour
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        
        private SceneLoader _sceneLoader;

        
        [Inject]
        public void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _sceneLoader.ShowCurtainInvoked += _loadingCurtain.Show;
            _sceneLoader.HideCurtainInvoked += _loadingCurtain.Hide;
        }

        private void OnDestroy()
        {
            _sceneLoader.ShowCurtainInvoked -= _loadingCurtain.Show;
            _sceneLoader.HideCurtainInvoked -= _loadingCurtain.Hide;
        }
    }
}