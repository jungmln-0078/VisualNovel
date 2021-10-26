using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingleTon<ResourceManager>
{
    private Dictionary<string, Object> _dataBase = new Dictionary<string, Object>();
    private void Add<T>(string path) where T : Object
    {
        _dataBase.Add(path, Resources.Load<T>(path));
    }
    public Object Get<T>(string id) where T : Object
    {
        if (!_dataBase.TryGetValue(id, out Object result))
        {
            Add<T>(id);
            _dataBase.TryGetValue(id, out Object result1);
            return result1;
        }
        return result;
    }
}
