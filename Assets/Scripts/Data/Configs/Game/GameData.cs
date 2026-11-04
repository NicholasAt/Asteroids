using UnityEngine;

namespace Assets.Scripts.Data.Configs.Game
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Game data")]
    public class GameData : ScriptableObject
    {
        [SerializeField] bool _isForceMobile;
        public bool IsMobile => _isForceMobile || Application.isMobilePlatform;
    }
}