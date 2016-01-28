namespace Assets.Plugins.Utilities
{
	public class PersistentSingleton<T> : Singleton<T> where T : PersistentSingleton<T>
	{
		protected override void Awake()
		{
			base.Awake();
			DontDestroyOnLoad(gameObject);
		}
	}
}