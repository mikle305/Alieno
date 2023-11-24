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
            _model.ValueChanged += UpdateStatBar;
            UpdateStatBar();
        }
        
        private void UpdateStatBar() 
            => _view.SetValue(_model.Current, _model.Max);
    }
}