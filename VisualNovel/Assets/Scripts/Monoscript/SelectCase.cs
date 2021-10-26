using UnityEngine;

public class SelectCase : MonoBehaviour
{
    private string _goto;

    public void OnClick()
    {
        DialogManager dialogManager = DialogManager.Instance;
        dialogManager.GotoScene(_goto);
    }

    public void SetGoto(string sid)
    {
        _goto = sid;
    }
}
