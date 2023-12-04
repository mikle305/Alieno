using DG.Tweening;
using UnityEngine;

namespace Services.TransparentObstacles
{
    public class TransparentObstacle : MonoBehaviour
    {
        [SerializeField] private Material _mainMaterial;
        [SerializeField] private Material _transparentMaterial;
        
        private float _duration = 0.2f;

        private MeshRenderer[] _renderers;
        private Tween[] _tweens;
        

        public void SetTransparent(bool isTransparent)
        {
            if (_renderers == null)
            {
                _renderers = GetComponentsInChildren<MeshRenderer>(includeInactive: true);
                _tweens = new Tween[_renderers.Length];
            }

            for (var i = 0; i < _renderers.Length; i++)
            {
                _tweens[i]?.Kill();
                MeshRenderer meshRenderer = _renderers[i];
                if (meshRenderer == null)
                    continue;
                
                if (isTransparent)
                {
                    meshRenderer.material = _transparentMaterial;
                    _tweens[i] = meshRenderer.material.DOFade(0.0f, _duration);
                }
                else
                {
                    _tweens[i] = meshRenderer.material
                        .DOFade(1.0f, _duration)
                        .OnComplete(() => meshRenderer.material = _mainMaterial);
                }
            }
        }

        private void OnDestroy()
        {
            if (_tweens == null)
                return;
            
            foreach (Tween tween in _tweens) 
                tween?.Kill();
        }
    }
}