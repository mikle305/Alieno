namespace UI.GamePlay
{
    public interface ICharacteristicView
    {
        public void Init(CharacteristicPresenter presenter);
        public void SetValue(float current, float max);
    }
}