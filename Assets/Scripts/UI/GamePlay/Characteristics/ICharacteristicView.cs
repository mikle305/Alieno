namespace UI.GamePlay
{
    public interface ICharacteristicView
    {
        public void Init(ICharacteristicPresenter presenter);
        public void SetValue(float current, float max);
    }
}