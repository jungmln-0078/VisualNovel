using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    public void OnClick()
    {
        DialogManager dialogManager = DialogManager.Instance;
        if (dialogManager.IsWritingText && GameObject.Find("Select") == null)
        {
            dialogManager.StopWriteText();
        } else
        {
            dialogManager.NextDialog();
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
