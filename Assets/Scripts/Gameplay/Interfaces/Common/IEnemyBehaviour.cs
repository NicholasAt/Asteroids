using System;

namespace Assets.Scripts.Gameplay.Interfaces.Common
{
    public interface IEnemyBehaviour : IApplyDamage, IWrapped
    {
        void Update(float deltaTime);
        void Destroy();
    }
}