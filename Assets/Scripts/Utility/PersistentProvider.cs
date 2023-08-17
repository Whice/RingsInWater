using System;
using UnityEngine;

namespace Utility
{
    public class PersistentProvider
    {/*
        #region Set

        public void SetInt(string name, int value)
        {
            PlayerPrefs.SetInt(name, value);
        }
        public void SetEnum<T>(string name, T value) where T: Enum
        {
            int intValue = Convert.ToInt32(value);

            SetInt(name, intValue);
        }
        public void SetBool(string name, bool value)
        {
            SetInt(name, value ? 1 : 0);
        }
        public void SetFloat(string name, float value)
        {
            PlayerPrefs.SetFloat(name, value);
        }
        public void SetString(string name, string value)
        {
            PlayerPrefs.SetString(name, value);
        }

        #endregion Set

        #region Get

        public int GetInt(string name, int defalut)
        {
            return PlayerPrefs.GetInt(name, defalut);
        }
        public T GetEnum<T>(string name, T defalut) where T: Enum
        {
            int intValue = Convert.ToInt32(defalut);

            Enum.TryParse<T>(1);
            return (T)GetInt(name, intValue);
        }
        public void SetBool(string name, bool value)
        {
            SetInt(name, value ? 1 : 0);
        }
        public void SetFloat(string name, float value)
        {
            PlayerPrefs.SetFloat(name, value);
        }
        public void SetString(string name, string value)
        {
            PlayerPrefs.SetString(name, value);
        }
        #endregion Set*/

    }
}
