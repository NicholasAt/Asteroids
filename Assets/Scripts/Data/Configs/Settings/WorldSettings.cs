using System;
using UnityEngine;

namespace Assets.Scripts.Data.Configs.Settings
{
    [Serializable]
    public class WorldSettings
    {
        public Vector2 MapSize = new Vector2(16f, 9f);
        public Vector2 OutMapSize = new Vector2(20f, 13f);
        public int MaxEnemies = 12;
    }
}
