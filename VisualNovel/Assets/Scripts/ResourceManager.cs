using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingleTon<ResourceManager>
{
    private Dictionary<string, Object> _dataBase = new Dictionary<string, Object>();
    private void Add<T>(string path) where T : Object
    {
        if (Get(path) == null)
        {
            _dataBase.Add(path, Resources.Load<T>(path));
        }
    }
    public Object Get(string id)
    {
        _dataBase.TryGetValue(id, out Object result);
        return result;
    }
    public void LoadResources()
    {
        Add<TextAsset>("data");
        Add<GameObject>("Prefab/Case");
    }
}
