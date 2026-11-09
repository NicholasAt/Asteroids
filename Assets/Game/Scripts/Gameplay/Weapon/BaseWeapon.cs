using Assets.Game.Scripts.Gameplay.Interfaces.Common;

namespace Assets.Game.Scripts.Gameplay.Weapon
{
    public abstract class BaseWeapon : IWeapon
    {
        public abstract void Update(bool isPress,float deltaTime);
    }
}