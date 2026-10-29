using UnityEngine;

namespace Assets.Scripts.Data.Enemy.Saucer
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Enemy/Saucer data")]
    public class SaucerData : ScriptableObject
    {
        [field: SerializeField] public GameObject SaucerPrefab { get; private set; }
    }
}