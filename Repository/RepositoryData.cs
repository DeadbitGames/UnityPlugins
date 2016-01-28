namespace Assets.Plugins.Repository
{
    public class RepositoryData
    {
        public readonly string Branch;
        public readonly string Revision;
        public readonly string Message;
        public readonly string BuildDate;

        public string Formatted
        {
            get { return string.Format("{0} rev. {1} build date {2}", Branch, Revision, BuildDate); }
        }

        public RepositoryData()
        {
        }

        public RepositoryData(string branch, string revision, string message, string buildDate)
        {
            Branch = branch;
            Revision = revision;
            Message = message;
            BuildDate = buildDate;
        }
    }
}
