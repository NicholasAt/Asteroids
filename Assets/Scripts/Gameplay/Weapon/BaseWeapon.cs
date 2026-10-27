namespace Assets.Scripts.Gameplay.Weapon
{
    public abstract class BaseWeapon : IWeapon
    {
        public abstract void Update(bool isPress,float deltaTime);
    }
}