using System;
using UnityEngine;

namespace Assets.Plugins.Utilities
{
    [Serializable]
    public class RigidbodySavedInstance
    {
        public Rigidbody Rigidbody;
        public RigidbodySave RigidbodySave;

        public RigidbodySavedInstance(Rigidbody rigidbody)
        {
            Rigidbody = rigidbody;
            RigidbodySave = rigidbody.GetSave();
        }
    }
}
