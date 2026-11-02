using UnityEngine;

namespace Assets.Scripts.Data.Models
{
    public class Health : MonoBehaviour
    {
        public int CurrentHealth { get; private set; }

        public Health(int maxHealth)
        {
            CurrentHealth = maxHealth;
        }

        public void ChangeHealth(int health)
        {
            CurrentHealth = health;
        }
    }
}