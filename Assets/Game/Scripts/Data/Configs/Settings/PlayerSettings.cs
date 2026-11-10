using Assets.Game.Scripts.Data.Configs.Weapon;
using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Data.Configs.Settings
{
    [Serializable]
    public class PlayerSettings : IValidatableSettings
    {
        public int StartHealth = 3;
        public float InvulnerabilitySeconds = 2f;
        public ShipSettings Ship = new ShipSettings();
        public GunSettings Gun = new GunSettings();
        public LaserSettings Laser = new LaserSettings();
        public List<WeaponId> StartingWeaponIds = new List<WeaponId> { WeaponId.Gun_Gun, WeaponId.Constant_Laser };

        public bool TryValidate(out string error)
        {
            if (StartHealth <= 0)
                return Fail("StartHealth must be greater than 0", out error);
            if (InvulnerabilitySeconds < 0f)
                return Fail("InvulnerabilitySeconds must not be negative", out error);

            if (Ship == null)
                return Fail("Ship is null", out error);
            if (Ship.Mass <= 0f)
                return Fail("Ship.Mass must be greater than 0", out error);
            if (Ship.Size <= 0f)
                return Fail("Ship.Size must be greater than 0", out error);

            if (Gun == null)
                return Fail("Gun is null", out error);
            if (Gun.FireRate <= 0f)
                return Fail("Gun.FireRate must be greater than 0", out error);
            if (Gun.Bullet == null)
                return Fail("Gun.Bullet is null", out error);
            if (Gun.Bullet.Mass <= 0f)
                return Fail("Gun.Bullet.Mass must be greater than 0", out error);
            if (Gun.Bullet.Radius <= 0f)
                return Fail("Gun.Bullet.Radius must be greater than 0", out error);
            if (Gun.Bullet.LifeTime <= 0f)
                return Fail("Gun.Bullet.LifeTime must be greater than 0", out error);
            if (Gun.Bullet.Damage < 0)
                return Fail("Gun.Bullet.Damage must not be negative", out error);

            if (Laser == null)
                return Fail("Laser is null", out error);
            if (Laser.MaxCharges <= 0)
                return Fail("Laser.MaxCharges must be greater than 0", out error);
            if (Laser.ChargeCooldown <= 0f)
                return Fail("Laser.ChargeCooldown must be greater than 0", out error);
            if (Laser.RayLength <= 0f)
                return Fail("Laser.RayLength must be greater than 0", out error);
            if (Laser.RayWidth <= 0f)
                return Fail("Laser.RayWidth must be greater than 0", out error);
            if (Laser.Damage < 0)
                return Fail("Laser.Damage must not be negative", out error);

            if (StartingWeaponIds == null || StartingWeaponIds.Count == 0)
                return Fail("StartingWeaponIds is null or empty", out error);
            if (StartingWeaponIds.Contains(WeaponId.None))
                return Fail("StartingWeaponIds contains None", out error);

            error = null;
            return true;
        }

        private bool Fail(string message, out string error)
        {
            error = message;
            return false;
        }
    }

    [Serializable]
    public class ShipSettings
    {
        public float Force = 1f;
        public float SpeedRotate = 3f;
        public float SpeedLimit = 5f;
        public float Mass = 1f;
        public float Drag = 0.5f;
        public float Size = 0.5f;
        public float AngularDrag = 3f;
    }

    [Serializable]
    public class GunSettings
    {
        public float FireRate = 4f;
        public float BulletForce = 10f;
        public float BulletSpeedLimit = 10f;
        public float ForwardSpawnPointOffset = 0.5f;
        public BulletSettings Bullet = new BulletSettings();

        public float Cooldown => FireRate > 0f ? 1f / FireRate : float.MaxValue;
    }

    [Serializable]
    public class BulletSettings
    {
        public float Mass = 0.5f;
        public float Radius = 0.5f;
        public float LifeTime = 1.5f;
        public int Damage = 1;
    }

    [Serializable]
    public class LaserSettings
    {
        public int Damage = 1;
        public int MaxCharges = 3;
        public float ChargeCooldown = 5f;
        public float RayWidth = 0.3f;
        public float RayLength = 15f;
        public float EffectDuration = 0.1f;
    }
}