using System;
using UnityEngine;

namespace Assets.Game.Scripts.Data.Configs.Settings
{
    [Serializable]
    public class WorldSettings : IValidatableSettings
    {
        public Vector2 MapSize = new Vector2(16f, 9f);
        public Vector2 OutMapSize = new Vector2(20f, 13f);
        public int MaxEnemies = 12;

        public bool TryValidate(out string error)
        {
            if (MapSize.x <= 0f || MapSize.y <= 0f)
            {
                error = "MapSize must be greater than 0";
                return false;
            }

            if (OutMapSize.x < MapSize.x || OutMapSize.y < MapSize.y)
            {
                error = "OutMapSize must not be smaller than MapSize";
                return false;
            }

            if (MaxEnemies <= 0)
            {
                error = "MaxEnemies must be greater than 0";
                return false;
            }

            error = null;
            return true;
        }
    }
}