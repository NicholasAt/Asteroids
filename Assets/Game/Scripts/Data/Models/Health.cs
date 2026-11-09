using System;

namespace Assets.Game.Scripts.Data.Models
{
    public class Health : IDisposable
    {
        public int CurrentHealth { get; private set; }
        public event Action OnHealthChange;

        public Health(int maxHealth)
        {
            CurrentHealth = maxHealth;
        }

        public void ChangeHealth(int health)
        {
            CurrentHealth = health;
            OnHealthChange?.Invoke();
        }

        public void Dispose()
        {
            OnHealthChange = null;
        }
    }
}