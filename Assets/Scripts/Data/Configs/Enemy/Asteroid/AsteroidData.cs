using UnityEngine;

namespace Assets.Scripts.Data.Configs.Enemy.Asteroid
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Enemy/Asteroid data")]
    public class AsteroidData : ScriptableObject
    {
        [field: SerializeField] public GameObject AsteroidPrefab { get; private set; }
        [field: SerializeField] public float Mass { get; private set; }
    }
}