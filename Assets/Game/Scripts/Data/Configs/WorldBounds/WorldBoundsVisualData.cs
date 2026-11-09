using System;
using UnityEngine;

namespace Assets.Game.Scripts.Data.Configs.WorldBounds
{
    [Serializable]
    public class WorldBoundsVisualData
    {
        [field: SerializeField] public float LineWidth { get; private set; }
        [field: SerializeField] public Material LineMaterial { get; private set; }
    }
}