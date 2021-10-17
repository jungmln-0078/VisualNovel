using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<Scene> Scenes = XmlManager.LoadXml();
        foreach (Scene scene in Scenes)
        {
            Debug.Log(scene.Sid);
            foreach (DialogData dialogData in scene.DialogDatas)
            {
                Debug.Log(dialogData.Type);
            }
        }
        Debug.Log(Scenes.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
