using System;

namespace Assets.Game.Scripts.Gameplay.Interfaces.Common
{
    public interface IApplyDamage
    {
        event Action OnDie;
        void TakeDamage(float damage,bool isSplit);
    }
}