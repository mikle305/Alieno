using Services;
using UnityEngine;
using VContainer;

namespace Additional.Game
{
    [RequireComponent(typeof(Canvas))]
    public class UiCameraSetter : MonoBehaviour
    {
        private ObjectsProvider _objectsProvider;
        
        
        [Inject]
        public void Construct(ObjectsProvider objectsProvider)
        {
            _objectsProvider = objectsProvider;
        }

        private void Start()
        {
            Camera uiCamera = _objectsProvider != null
                 ? _objectsProvider.UiCamera
                 : null;

            var canvas = GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = uiCamera;
            canvas.planeDistance = 5;
        }
    }
}
