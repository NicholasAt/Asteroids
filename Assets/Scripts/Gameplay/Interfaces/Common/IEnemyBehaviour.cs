using System;

namespace Assets.Scripts.Gameplay.Interfaces.Common
{
    public interface IEnemyBehaviour : IApplyDamage, IWrapped
    {
        Action OnDestroy { get; set; }
        void Update(float deltaTime);
        void Destroy();
    }
}