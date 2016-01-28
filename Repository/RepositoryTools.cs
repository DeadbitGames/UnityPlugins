using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif
using UnityEngine;

namespace Assets.Plugins.Repository
{
    public class RepositoryTools
    {
        static string fileName = "Data";
        static string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        //filePath = "jar:file://" + Application.dataPath + "!/assets/" + BundleURL; //Android, use WWW!
#if UNITY_EDITOR
        [MenuItem("Tools/Save Revision")]
        [PostProcessScene]
#endif
        public static void Save()
        {
            try
            {
                if (!Directory.Exists(Application.streamingAssetsPath))
                    Directory.CreateDirectory(Application.streamingAssetsPath);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            try
            {
                if (!File.Exists(filePath))
                    using (File.Create(filePath))
                    {

                    }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    writer.Write(RepositoryInfo.Data.Branch + "\n");
                    writer.Write(RepositoryInfo.Data.Revision + "\n");
                    writer.Write(RepositoryInfo.Data.Message + "\n");
                    writer.Write(RepositoryInfo.GetBuildDate);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }

        public static RepositoryData Load()
        {
            if (!Directory.Exists(Application.streamingAssetsPath) || !File.Exists(filePath))
                return new RepositoryData();

            using (var reader = new StreamReader(filePath))
            {
                return new RepositoryData(reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine());
            }
        }
    }
}