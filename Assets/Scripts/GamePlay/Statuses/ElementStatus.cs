namespace GamePlay.Statuses
{
    public abstract class ElementStatus : Status
    {
        public float DamageCoefficient { get; set; }
        public float Rate { get; set; }
        public int CountLeft { get; set; }
    }
}