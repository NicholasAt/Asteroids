using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Data.Configs.Settings
{
    public static class JsonConfigLoader
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ObjectCreationHandling = ObjectCreationHandling.Replace
        };

        public static T Load<T>(string resourcePath) where T : class, new()
        {
            TextAsset asset = Resources.Load<TextAsset>(resourcePath);
            if (asset == null)
            {
                Debug.LogError($"Config not found: Resources/{resourcePath}.json. Using defaults for {typeof(T).Name}");
                return new T();
            }

            T result;
            try
            {
                result = JsonConvert.DeserializeObject<T>(asset.text, SerializerSettings);
            }
            catch (Exception e)
            {
                Debug.LogError($"Config parse error in Resources/{resourcePath}.json: {e.Message}. Using defaults for {typeof(T).Name}");
                return new T();
            }

            if (result == null)
            {
                Debug.LogError($"Config is empty: Resources/{resourcePath}.json. Using defaults for {typeof(T).Name}");
                return new T();
            }

            if (result is IValidatableSettings validatable && validatable.TryValidate(out string error) == false)
            {
                Debug.LogError($"Config is invalid: Resources/{resourcePath}.json: {error}. Using defaults for {typeof(T).Name}");
                return new T();
            }

            return result;
        }
    }
}