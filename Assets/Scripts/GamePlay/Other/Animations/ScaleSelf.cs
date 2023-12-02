using UnityEngine;

namespace GamePlay.Other.Animations
{
    public class ScaleSelf : MonoBehaviour
    {
        [SerializeField] [Range(0,1)] private float _bouncyScale = 1f;
     
        [SerializeField] private bool _scaleX;
        [SerializeField] private bool _scaleY;
        [SerializeField] private bool _scaleZ;
        [SerializeField] private float _lerpTime;

        private float _timePassed;

        private Vector3 _startingScale;
        private Vector3 _targetScale;

        private bool _downscaling = true;
        void Start()
        {
            _startingScale = transform.localScale;
            _targetScale = _startingScale * _bouncyScale;
        }

        void Update()
        {
            if (_downscaling)
                _timePassed += Time.deltaTime;
            else
                _timePassed -= Time.deltaTime;
        
            transform.localScale =  Vector3.Lerp(_startingScale, _targetScale, _timePassed/_lerpTime);

            if (_timePassed >= _lerpTime)
                _downscaling = false;
            else if (_timePassed <= 0)
                _downscaling = true;    
        }
    }
}
