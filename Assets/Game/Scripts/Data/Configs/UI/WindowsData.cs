using UnityEngine;

namespace Assets.Game.Scripts.Data.Configs.UI
{
    [CreateAssetMenu(menuName = "Data/Gameplay/UI/Window data")]
    public class WindowsData : ScriptableObject
    {
        [field: SerializeField] public GameObject HUDPrefab { get; private set; }
        [field: SerializeField] public GameObject MobileInputPrefab { get; private set; }
    }
}