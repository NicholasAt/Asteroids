using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Data.Configs.Settings
{
    public static class JsonConfigLoader
    {
        public static T Load<T>(string resourcePath) where T : class, new()
        {
            TextAsset asset = Resources.Load<TextAsset>(resourcePath);
            if (asset == null)
            {
                Debug.LogError($"Config not found: Resources/{resourcePath}.json. Using defaults for {typeof(T).Name}");
                return new T();
            }

            try
            {
                T result = JsonConvert.DeserializeObject<T>(asset.text);
                if (result != null)
                    return result;

                Debug.LogError($"Config is empty: Resources/{resourcePath}.json. Using defaults for {typeof(T).Name}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Config parse error in Resources/{resourcePath}.json: {e.Message}. Using defaults for {typeof(T).Name}");
            }
            return new T();
        }
    }
}
