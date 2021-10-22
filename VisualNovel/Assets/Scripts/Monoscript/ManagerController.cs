using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerController : MonoBehaviour
{
    private string _sceneTemp;
    void Awake()
    {
        object[] instance = FindObjectsOfType(typeof(ManagerController));
        DontDestroyOnLoad(gameObject);
        ResourceManager.Instance.LoadResources();
        if (instance.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        LoadManagers(scene.name);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void LoadManagers(string scene)
    {
        switch (scene)
        {
            case "InGame":
                {
                    DialogManager dialogManager = DialogManager.Instance;
                    dialogManager.OnResume();
                    break;
                }
            case "Main":
                {
                    if (_sceneTemp == "InGame")
                    {
                        DialogManager.Instance.StopAllCoroutines();
                    }
                    break;
                }
        }
        _sceneTemp = scene;
    }
}