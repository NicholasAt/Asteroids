namespace Assets.Game.Scripts.Data.Models.Weapon
{
    public class Constant_WeaponModel : IWeaponModel
    {
        public int CurrentCharges { get; private set; }
        public int MaxCharges { get; private set; }
        public float CurrentCD { get; private set; }

        public void SetCharges(int currentCharges, int maxCharges)
        {
            CurrentCharges = currentCharges;
            MaxCharges = maxCharges;
        }
        public void SetCD(float currentCD)
        {
            CurrentCD = currentCD;
        }
    }
}