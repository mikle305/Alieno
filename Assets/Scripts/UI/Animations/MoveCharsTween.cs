using CharTween;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Animations
{
    public class MoveCharsTween : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private float _radius = 0.05f;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Color _color = Color.yellow;
        
        private CharTweener _charTweener;
        

        public void Enable() 
            => AnimateText();

        public void Disable()
            => _charTweener.KillAll();

        private void AnimateText()
        {
            if (_charTweener == null)
                _charTweener = _textMesh.GetCharTweener();
            
            for (var i = 0; i < _charTweener.CharacterCount; i++)
            {
                Tween circleTween = _charTweener
                    .DOMoveCircle(i, _radius, _duration)
                    .SetLoops(-1, LoopType.Restart);

                // Offset animations based on character index in string
                float timeOffset = (float) i / _charTweener.CharacterCount;
                circleTween.fullPosition = timeOffset;
            }
        }
    }
}