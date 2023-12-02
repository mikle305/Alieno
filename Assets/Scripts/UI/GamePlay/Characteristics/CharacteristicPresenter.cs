using GamePlay.Characteristics;

namespace UI.GamePlay
{
    public class CharacteristicPresenter : ICharacteristicPresenter
    {
        private readonly ICharacteristicView _view;
        private readonly Characteristic _model;

        
        public CharacteristicPresenter(Characteristic model, ICharacteristicView view)
        {
            _model = model;
            _view = view;
        }

        public void Bind(bool forceUpdate = false)
        {
            _model.ValueChanged += UpdateStatBar;
            if (forceUpdate)
                UpdateStatBar();
        }

        public void Unbind()
            => _model.ValueChanged -= UpdateStatBar;

        private void UpdateStatBar() 
            => _view.SetValue(_model.Current, _model.Max);
    }
}