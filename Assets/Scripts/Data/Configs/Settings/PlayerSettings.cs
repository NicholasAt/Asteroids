using System;

namespace Assets.Scripts.Data.Configs.Settings
{
    [Serializable]
    public class PlayerSettings
    {
        public int StartHealth = 3;
        public float InvulnerabilitySeconds = 2f;
        public ShipSettings Ship = new ShipSettings();
        public GunSettings Gun = new GunSettings();
        public LaserSettings Laser = new LaserSettings();
    }

    [Serializable]
    public class ShipSettings
    {
        public float Force = 1f;
        public float SpeedRotate = 3f;
        public float SpeedLimit = 5f;
        public float Mass = 1f;
        public float Drag = 0.5f;
        public float AngularDrag = 3f;
    }

    [Serializable]
    public class GunSettings
    {
        public float FireRate = 4f;
        public float BulletForce = 10f;
        public float BulletSpeedLimit = 10f;
        public float ForwardSpawnPointOffset = 0.5f;
        public bool IsSplit = true;
        public BulletSettings Bullet = new BulletSettings();

        public float Cooldown => FireRate > 0f ? 1f / FireRate : float.MaxValue;
    }

    [Serializable]
    public class BulletSettings
    {
        public float Mass = 0.5f;
        public float Radius = 0.5f;
        public float LifeTime = 1.5f;
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
        public bool IsSplit;
    }
}
