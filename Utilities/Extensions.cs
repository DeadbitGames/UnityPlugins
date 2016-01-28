using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Plugins.Utilities
{
	public static class Extensions
	{
		/// <summary>
		/// Returns all monobehaviours (casted to T)
		/// </summary>
		/// <typeparam name="T">interface type</typeparam>
		/// <param name="gObj"></param>
		/// <returns></returns>
		public static T[] GetInterfaces<T>(this GameObject gObj)
		{
			if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
			var mObjs = gObj.GetComponents<T>();
			return mObjs;
		}

		public static RigidbodySave GetSave(this Rigidbody rigidbody)
		{
			var savedRigidbody = new RigidbodySave(rigidbody);
			rigidbody.isKinematic = true;
			return savedRigidbody;
		}

		public static void Load(this Rigidbody rigidbody, RigidbodySave save)
		{
			rigidbody.isKinematic = false;
			rigidbody.velocity = save.Velocity;
			rigidbody.angularVelocity = save.AngularVelocity;
		}

		/// <summary>
		/// Invoke Coroutine which fire method after time
		/// </summary>
		/// <param name="monobehaviour">GameObject on which you want to perform the method</param>
		/// <param name="callback">Delayed method</param>
		/// <param name="delay">Desired delay</param>
		public static void StartMethodDelayed(this MonoBehaviour monobehaviour, Action callback, float delay)
		{
			monobehaviour.StartCoroutine(DelayFireMethod(delay,callback));
		}

		public static IEnumerator DelayFireMethod(float delay,Action callback)
		{
			yield return new WaitForSeconds(delay);
			callback();
		}

		public static Vector3 AvgPos(this IEnumerable<Transform> self)
		{
			var vec = Vector3.zero;
			var count = 0;

			foreach (var t in self)
			{
				vec += t.position;
				count++;
			}

			return vec / count;
		}

		public static Vector3 Lerp(this Vector3 origin, Vector3 target, float speed)
		{
			return new Vector3(Mathf.Lerp(origin.x, target.x, speed * Time.deltaTime), Mathf.Lerp(origin.y, target.y, speed * Time.deltaTime), Mathf.Lerp(origin.z, target.z, speed * Time.deltaTime));
		}
		public static Vector2 Lerp(this Vector2 origin, Vector2 target, float speed)
		{
			return new Vector2(Mathf.Lerp(origin.x, target.x, speed * Time.deltaTime), Mathf.Lerp(origin.y, target.y, speed * Time.deltaTime));
		}

	    public static IEnumerable<Transform> Children(this Transform self)
	    {
	        var e = self.GetEnumerator();
	        while (e.MoveNext())
	            yield return (Transform)e.Current;
	    }

        public static T FindInUpHierarchy<T>(this GameObject go) where T : Component
        {
            var transform = go.transform;
            do
            {
                var comp = transform.GetComponent<T>();
                if (comp) return comp;
                transform = transform.parent;
            } while (transform != null);
            return null;
        }

        public static T InstantiateAsChild<T>(this GameObject thisGo, T prefab, bool worldPositionStays = false) where T : Component
        {
            var go = Object.Instantiate(prefab);
            go.transform.SetParent(thisGo.transform, worldPositionStays);
            return go;
        }

        public static T Random<T>(this IEnumerable<T> me)
        {
            var enumerable = me as IList<T> ?? me.ToList();
            return enumerable.ElementAt(UnityEngine.Random.Range(0, enumerable.Count()));
        }

	    public static void SetLayerRecursively(this GameObject obj, int layer)
	    {
	        obj.layer = layer;

	        foreach (Transform child in obj.transform)
	        {
	            child.gameObject.SetLayerRecursively(layer);
	        }
	    }
	}
}
