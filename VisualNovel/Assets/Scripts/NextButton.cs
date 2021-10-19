using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    public void OnClick()
    {
        DialogManager dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
        if (dialogManager.IsWritingText)
        {
            dialogManager.EndWriteText();
        } else
        {
            dialogManager.IncreaseDialogIdx();
        }
    }
}
