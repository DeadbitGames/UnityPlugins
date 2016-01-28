using System;
using UnityEngine;

namespace Assets.Plugins.Utilities
{
	[Serializable]
	public class RigidbodySave
	{
		Vector3 _velocity;

		public Vector3 Velocity
		{
			get { return _velocity; }
		}

		public Vector3 AngularVelocity
		{
			get { return _angularVelocity; }
		}

		Vector3 _angularVelocity;

		public RigidbodySave(Rigidbody rigidbody)
		{
			_velocity = rigidbody.velocity;
			_angularVelocity = rigidbody.angularVelocity;
		}
	}
}
