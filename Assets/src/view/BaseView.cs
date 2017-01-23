using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using Object = UnityEngine.Object;

namespace trailmarch.view
{
    public abstract class BaseView : View, IDisposable
    {
        public abstract void Init();

        public abstract void Dispose();

        protected GameObject CreateGameObject(Object resource, Vector3 pos, Transform parent)
        {
            GameObject obj = (GameObject)Instantiate(resource);
            obj.transform.position = pos;
            obj.transform.SetParent(parent);

            return obj;
        }
    }
}
