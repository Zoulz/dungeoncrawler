using System;
using Object = UnityEngine.Object;

namespace trailmarch.model.api
{
    public interface IResourceManager
    {
        void LoadResource(String key, String uri);
        void LoadAllResources(String baseKey, String uri);
        void UnloadResource(String key);
        void UnloadAllResources();
        Object GetResource(String key);
    }
}
