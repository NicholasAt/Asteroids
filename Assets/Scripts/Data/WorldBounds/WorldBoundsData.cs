using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.WorldBounds
{
    [CreateAssetMenu(menuName = "Data/Gameplay/World bounds")]
    public class WorldBoundsData : ScriptableObject
    {
        [field: SerializeField] public Vector2 MapSize { get; private set; }
        [field: SerializeField] public Vector2 OutMapSize { get; private set; }
    }
}