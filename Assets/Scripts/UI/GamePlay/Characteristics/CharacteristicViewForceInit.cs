using GamePlay.Characteristics;
using UnityEngine;

namespace UI.GamePlay
{
    public class CharacteristicViewForceInit : MonoBehaviour
    {
        [SerializeField] private Characteristic _model;
        [SerializeField] private CharacteristicView _view;


        private void Awake() 
            => Bind();

        private void Bind()
        {
            var presenter = new CharacteristicPresenter(_model, _view);
            presenter.Bind();
            _view.Init(presenter);
        }
    }
}