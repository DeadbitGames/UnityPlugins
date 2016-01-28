using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Plugins.Repository
{
    public static class RepositoryInfo
    {
        public static RepositoryData Data;
        public static string GetBuildDate { get { return DateTime.Now.ToString("g"); } }

        static RepositoryInfo()
        {
#if UNITY_EDITOR
            Data = new Editor().GetData();
#else
            Data = new Builded().GetData();
#endif
        }

    }

    public interface IIRepositoryInfo
    {
        RepositoryData GetData();
    }

    abstract class Repository : IIRepositoryInfo
    {
        public abstract RepositoryData GetData();
    }

    class Editor : Repository
    {
        const string DirstatePath = @"../.hg/dirstate";
        const string MessagePath = @"../.hg/last-Message.txt";
        const string BranchPath = @"../.hg/Branch";

        public string GetRevision()
        {
            if (File.Exists(DirstatePath))
            {
                using (var reader = new BinaryReader(File.Open(DirstatePath, FileMode.Open)))
                {
                    try
                    {
                        var revision = reader.ReadBytes(6).Take(6).ToArray();
                        //complete hash is 20 bytes, second 20 bytes is parent
                        return BitConverter.ToString(revision).Replace("-", "").ToLower();
                    }
                    catch (Exception)
                    {
                        return "NONE";
                    }
                }
            }
            return "NONE";
        }

        public string GetMessage()
        {
            if (File.Exists(MessagePath))
            {
                using (StreamReader reader = File.OpenText(MessagePath))
                {
                    try
                    {
                        return reader.ReadToEnd();
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                        return "NONE";
                    }
                }
            }
            return "NONE";
        }

        public string GetBranch()
        {
            if (File.Exists(BranchPath))
            {
                using (StreamReader reader = File.OpenText(BranchPath))
                {
                    try
                    {
                        return reader.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                        return "NONE";
                    }
                }
            }
            return "NONE";
        }

        public override RepositoryData GetData()
        {
            return new RepositoryData(GetBranch(), GetRevision(), GetMessage(), RepositoryInfo.GetBuildDate);
        }
    }
    class Builded : Repository
    {
        public override RepositoryData GetData()
        {
            return RepositoryTools.Load();
        }
    }
}