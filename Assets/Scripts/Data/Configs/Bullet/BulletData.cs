using UnityEngine;

namespace Assets.Scripts.Data.Configs.Bullet
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Bullet data")]
    public class BulletData : ScriptableObject
    {
        [field: SerializeField] public GameObject BulletPrefab { get; private set; }
    }
}
