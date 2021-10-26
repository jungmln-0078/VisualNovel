using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSceneButton : MonoBehaviour
{
    public string Scene;
    public void OnClick()
    {
        SceneManager.LoadScene(Scene);
    }
}
