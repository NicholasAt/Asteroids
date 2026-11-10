namespace Assets.Game.Scripts.Data.Models.Weapon
{
    public class ConstantWeaponModel : IWeaponModel
    {
        public int CurrentCharges { get; private set; }
        public int MaxCharges { get; private set; }
        public float CooldownRemaining { get; private set; }

        public void SetCharges(int currentCharges, int maxCharges)
        {
            CurrentCharges = currentCharges;
            MaxCharges = maxCharges;
        }

        public void SetCooldownRemaining(float cooldownRemaining)
        {
            CooldownRemaining = cooldownRemaining;
        }
    }
}