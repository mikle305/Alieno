using GamePlay.Characteristics;
using UnityEngine;

namespace UI.GamePlay
{
    public class CharacteristicViewForceInit : MonoBehaviour
    {
        [SerializeField] private Characteristic _model;
        [SerializeField] private CharacteristicView _view;
        [SerializeField] private bool _forceUpdate = true;


        private void Start() 
            => Bind();

        private void Bind()
        {
            var presenter = new CharacteristicPresenter(_model, _view);
            presenter.Bind(_forceUpdate);
            _view.Init(presenter);
        }
    }
}