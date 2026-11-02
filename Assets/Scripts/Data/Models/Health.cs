using System;

namespace Assets.Scripts.Data.Models
{
    public class Health
    {
        public int CurrentHealth { get; private set; }
        public Action OnHealthChange { get; set; }

        public Health(int maxHealth)
        {
            CurrentHealth = maxHealth;
        }

        public void ChangeHealth(int health)
        {
            CurrentHealth = health;
            OnHealthChange?.Invoke();
        }
    }
}