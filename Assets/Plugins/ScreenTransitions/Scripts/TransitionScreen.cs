using DG.Tweening;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenTransitions
{
    [RequireComponent(typeof(Image))]
    public class TransitionScreen : MonoBehaviour
    {
        [SerializeField] private string _shaderParam = "_Progress";
        [SerializeField] private float _beginValue;
        [SerializeField] private float _endValue;
        
        [Space(10)]
        [SerializeField] private bool _setDefaultValueOnStart;
        
        [SerializeField, ShowIf(nameof(_setDefaultValueOnStart), true)] 
        private float _defaultValue;

        [SerializeField] private TransitionData _enterData;
        [SerializeField] private TransitionData _exitData;
        
        private Image _image;


        protected void Awake()
        {
            _image = GetComponent<Image>();
            _image.raycastTarget = false;
            TrySetDefaultValue();
        }

        public TransitionOperation Enter()
        {
            _image.raycastTarget = true;
            var operation = new TransitionOperation();
            
            SetMaterialValue(_beginValue);
            _image.material.DOFloat(_endValue, _shaderParam, _enterData.Duration)
                .SetEase(_enterData.Ease)
                .OnComplete(operation.Complete);

            return operation;
        }

        public TransitionOperation Exit()
        {
            _image.raycastTarget = true;
            var operation = new TransitionOperation();

            SetMaterialValue(_endValue);
            _image.material.DOFloat(_beginValue, _shaderParam, _exitData.Duration)
                .SetEase(_exitData.Ease)
                .OnComplete(() =>
                {
                    _image.raycastTarget = false;
                    operation.Complete();
                });
            
            return operation;
        }

        private void SetMaterialValue(float value) 
            => _image.material.SetFloat(_shaderParam, value);

        private void TrySetDefaultValue()
        {
            if (_setDefaultValueOnStart)
                SetMaterialValue(_defaultValue);
        }
    }
}