using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCase : MonoBehaviour
{
    private string _goto;
    
    public void OnClick()
    {
        DialogManager dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
        dialogManager.GotoScene(_goto);
    }

    public void SetGoto(string sid)
    {
        _goto = sid;
    }
}
