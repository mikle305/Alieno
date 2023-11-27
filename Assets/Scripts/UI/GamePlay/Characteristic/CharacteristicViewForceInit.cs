using GamePlay.Characteristics;
using UnityEngine;

namespace UI.GamePlay
{
    public class CharacteristicViewForceInit : MonoBehaviour
    {
        [SerializeField] private Characteristic _model;
        [SerializeField] private CharacteristicView _view;


        private void Start() 
            => Bind();

        private void Bind()
        {
            var presenter = new CharacteristicPresenter(_model, _view);
            _view.Init(presenter);
        }
    }
}