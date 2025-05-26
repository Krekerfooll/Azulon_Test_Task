//No need to be afraid, this class is not an external library, it's written by me,
//I just don't want to see it actively overused outside of my projects or projects I've authorized to use it.
//There are no licenses, it's all on good faith.

using System.Collections.Generic;
using System;
using UnityEngine;
using Newtonsoft.Json;

namespace RomanKapustynskyi.SaveSystem
{
    public enum StorageMode
    {
        SessionOnly,
        Persistent
    }

    public class StorageProxy<T>
    {
        public event Action<T> OnValueChanged;

        public T Value
        {
            get => Get();
            set => Set(value);
        }

        private readonly string _key;
        private readonly StorageMode _mode;
        private readonly T _defaultValue;

        private static Dictionary<string, object> Cache { get; } = new();
        private static bool UseRawString => typeof(T).IsPrimitive || typeof(T) == typeof(string);

        public StorageProxy(string key, T defaultValue = default, StorageMode mode = StorageMode.Persistent)
        {
            _key = key;
            _defaultValue = defaultValue;
            _mode = mode;

            if (!Cache.ContainsKey(_key))
            {
                var initial = LoadFromStorage();
                Cache[_key] = initial;
            }
        }

        public void ForceSave(bool notify = false)
        {
            var current = Get();
            SaveToStorage(current);

            if (notify)
            {
                OnValueChanged?.Invoke(current);
            }
        }

        private T Get()
        {
            return Cache.TryGetValue(_key, out var val) ? (T)val : _defaultValue;
        }
        private void Set(T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(Get(), newValue))
                return;

            Cache[_key] = newValue;
            SaveToStorage(newValue);
            OnValueChanged?.Invoke(newValue);
        }

        private T LoadFromStorage()
        {
            if (_mode == StorageMode.SessionOnly)
                return _defaultValue;

            if (!PlayerPrefs.HasKey(_key))
                return _defaultValue;

            var json = PlayerPrefs.GetString(_key);
            return FromJson(json);
        }
        private void SaveToStorage(T value)
        {
            if (_mode == StorageMode.SessionOnly)
                return;

            PlayerPrefs.SetString(_key, ToJson(value));
            PlayerPrefs.Save();
        }

        private static string ToJson(T value)
        {
            return UseRawString ? value?.ToString() ?? "" : JsonConvert.SerializeObject(value);
        }
        private static T FromJson(string json)
        {
            if (string.IsNullOrEmpty(json)) return default;
            return UseRawString ? (T)Convert.ChangeType(json, typeof(T)) : JsonConvert.DeserializeObject<T>(json);
        }
    }
}
