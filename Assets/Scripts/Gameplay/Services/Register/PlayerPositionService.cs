using Assets.Scripts.Gameplay.Interfaces.Common;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Services.Register
{
    public class PlayerPositionService : IPlayerPosition
    {      
        public Vector2 PlayerPosition { get; private set; }

        public void SetPosition(Vector2 position)
        {
            PlayerPosition = position;
        }

    }
}