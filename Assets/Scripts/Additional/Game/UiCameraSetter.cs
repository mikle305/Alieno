using Services;
using UnityEngine;

namespace Additional.Game
{
    [RequireComponent(typeof(Canvas))]
    public class UiCameraSetter : MonoBehaviour
    {
        private ObjectsProvider _objectsProvider;

        private void Start()
        {
            _objectsProvider = ObjectsProvider.Instance;
            Camera uiCamera = _objectsProvider != null
                 ? _objectsProvider.UICamera
                 : null;

            var canvas = GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = uiCamera;
        }
    }
}
