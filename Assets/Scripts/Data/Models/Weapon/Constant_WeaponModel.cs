namespace Assets.Scripts.Data.Models.Weapon
{
    public class Constant_WeaponModel : IWeaponModel
    {
        public int CurrentCharges { get; private set; }
        public float CurrentCD { get; private set; }

        public void SetCharges(int currentCharges)
        {
            CurrentCharges = currentCharges;
        }
        public void SetCD(float currentCD)
        {
            CurrentCD = currentCD;
        }
    }
}