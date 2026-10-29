using Assets.Scripts.Gameplay.Interfaces.Services;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Services.Register
{
    public class CharacterRegister : ICharacterRegister
    {      
        public Vector2 PlayerPosition { get; private set; }

        public void SetPosition(Vector2 position)
        {
            PlayerPosition = position;
        }

    }
}