using UnityEngine;

namespace Assets.Scripts.Data.Configs.Enemy.Fragments
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Enemy/Fragments data")]
    public class FragmentsData : ScriptableObject
    {
        [field: SerializeField] public GameObject FragmentPrefab { get; private set; }
    }
}