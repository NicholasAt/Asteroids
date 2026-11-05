using UnityEngine;

namespace Assets.Scripts.Data.Configs.Bullet
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Bullet data")]
    public class BulletData : ScriptableObject
    {
        [field: SerializeField] public float BulletMass { get; private set; } = 0.5f;
        [field: SerializeField] public float BulleRadius { get; private set; } = 0.5f;
        [field: SerializeField] public float LifeTime { get; private set; }
        [field: SerializeField] public GameObject BulletPrefab { get; private set; }
    }
}