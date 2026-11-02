namespace Assets.Scripts.Gameplay.Signals
{
    public readonly struct ShipChangeHealthSignal
    {
        public readonly int CurrentHealth;

        public ShipChangeHealthSignal(int currentHealth)
        {
            CurrentHealth = currentHealth;
        }
    }
}