using Assets.Game.Scripts.Data.Configs.Enemy;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;

namespace Assets.Game.Scripts.Gameplay.Interfaces.Services.Factory
{
    public interface IEnemyFactory
    {
        IEnemyBehaviour Create(EnemyType enemyType);
    }
}