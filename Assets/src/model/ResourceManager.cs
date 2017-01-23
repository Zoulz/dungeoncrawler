using System;
using System.Collections.Generic;
using trailmarch.model.api;
using UnityEngine;
using Object = UnityEngine.Object;

namespace trailmarch.model
{
    public class ResourceManager : IResourceManager
    {
        private Dictionary<String, Object> _resources;

        public ResourceManager()
        {
            _resources = new Dictionary<string, Object>();
        }

        public void LoadResource(String key, String uri)
        {
            Object res = Resources.Load(uri);
            if (res != null)
            {
                _resources[key] = res;
            }
        }

        public void LoadAllResources(String baseKey, String uri)
        {
            Object[] res = Resources.LoadAll(uri);
            int iter = 1;

            if (res != null)
            {
                foreach (Object r in res)
                {
                    _resources[baseKey + iter.ToString()] = r;
                    iter++;
                }
            }
        }

        public void UnloadResource(String key)
        {
            if (_resources[key] != null)
            {
                Resources.UnloadAsset(_resources[key]);
                _resources.Remove(key);
            }
        }

        public void UnloadAllResources()
        {
            foreach (String key in _resources.Keys)
            {
                Resources.UnloadAsset(_resources[key]);
            }

            _resources = new Dictionary<string, Object>();
        }

        public Object GetResource(String key)
        {
            if (_resources.ContainsKey(key))
            {
                return _resources[key];
            }

            Debug.Log("Trying to get non-existing resource: " + key);
            return null;
        }
    }
}
