using UnityEngine;

namespace Assets.Scripts.Data.Configs.UI
{
    [CreateAssetMenu(menuName = "Data/Gameplay/UI/Window data")]
    public class WindowsData : ScriptableObject
    {
        [field: SerializeField] public GameObject HUDPrefab { get; private set; }
    }
}