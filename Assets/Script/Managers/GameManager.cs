using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int NumberLeaf; //Số lá còn trên màn hình
    public void Start()
    {
       // DontDestroyOnLoad(this);
    }
    public Transform _popUpContainer;
    public T OnShowDialog<T>(string path, object data = null, UnityEngine.Events.UnityAction callbackCompleteShow = null) where T:BaseDialog
    {
        GameObject prefab = this.GetResourceFile<GameObject>(path);
        if (prefab != null)
        {
            T objectSpawned = (Instantiate(prefab, _popUpContainer)).GetComponent<T>();
            if (objectSpawned != null)
            {
                objectSpawned.OnShow(data, callbackCompleteShow);
            }
            return objectSpawned as T;
        }
        return null;
    }
    public T GetResourceFile<T>(string path) where T : Object
    {
        return Resources.Load<T>(path) as T;
    }
}