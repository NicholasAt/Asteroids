using UnityEngine;

namespace Assets.Scripts.Data.Configs.Player
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Player data")]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public int StartHealth { get; private set; }
        [field: SerializeField] public float InvulnerabilitySeconds { get; private set; }
    }
}