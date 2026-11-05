using UnityEngine;

namespace Assets.Scripts.Data.Configs.EnemySpawner
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Enemy/Enemy spawner data")]
    public class EnemySpawnerData : ScriptableObject
    {
        [field: SerializeField] public int Asteroids { get; private set; }
        [field: SerializeField] public int Saucers { get; private set; }
        [field: SerializeField] public int Fragments { get; private set; }
        [field: SerializeField] public int RandomAngle { get; private set; }
    }
}