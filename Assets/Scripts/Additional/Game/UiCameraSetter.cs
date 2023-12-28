using Services;
using UnityEngine;
using VContainer;

namespace Additional.Game
{
    [RequireComponent(typeof(Canvas))]
    public class UiCameraSetter : MonoBehaviour
    {
        [SerializeField] private RenderMode _renderMode = RenderMode.ScreenSpaceCamera;
        
        private ObjectsProvider _objectsProvider;
        
        
        [Inject]
        public void Construct(ObjectsProvider objectsProvider)
        {
            _objectsProvider = objectsProvider;
        }

        private void Start()
        {
            var canvas = GetComponent<Canvas>();
            canvas.renderMode = _renderMode;
            canvas.worldCamera = _objectsProvider?.UiCamera;
            canvas.planeDistance = 5;
        }
    }
}
