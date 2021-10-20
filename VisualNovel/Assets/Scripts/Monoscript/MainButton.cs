using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButton : MonoBehaviour
{
    public string Scene;
    public void OnClick()
    {
        SceneManager.LoadScene(Scene);
    }
}
