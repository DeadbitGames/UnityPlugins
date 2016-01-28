using System.Collections;

namespace Assets.Plugins.Utilities
{
    public static class Utils
    {
        public static IEnumerator Concat(params IEnumerator[] enumerators)
        {
            foreach (var e in enumerators)
            {
                while (e.MoveNext()) { yield return e.Current; }
            }
        }
    }
}
