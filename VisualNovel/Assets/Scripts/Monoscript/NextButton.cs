using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    public void OnClick()
    {
        DialogManager dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
        if (dialogManager.IsWritingText && GameObject.Find("Select") == null)
        {
            dialogManager.EndWriteText();
        } else
        {
            dialogManager.IncreaseDialogIdx();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameObject.Find("Select") == null)
        {
            OnClick();
        }
    }
}
