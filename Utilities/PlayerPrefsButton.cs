#if UNITY_EDITOR 
using UnityEditor;
using UnityEngine;

namespace Assets.Plugins.Utilities
{
    class PlayerPrefsButton
    {
        [MenuItem("Tools/Clear Player Prefs")]
        public static void ClearAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
#endif