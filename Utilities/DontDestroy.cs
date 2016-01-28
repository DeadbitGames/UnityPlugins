using UnityEngine;

namespace Assets.Plugins.Utilities
{
	public class DontDestroy : MonoBehaviour 
	{
		public void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}
