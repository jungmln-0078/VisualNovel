using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
public enum DialogDataType{
    Text,
    ShowCharacter,
    HideCharacter,
    Select,
    Case,
    Effect
}
public class Scene
{
    public string Sid { get; set; }
    public string Background { get; set; }
    public string NextScene { get; set; }
    public string? Bgm { get; set; }
    public List<DialogData> DialogDatas { get; set; }

    public Scene(string sid, string background, string next, string? bgm)
    {
        this.Sid = sid;
        this.Background = background;
        this.NextScene = next;
        this.Bgm = bgm;
        this.DialogDatas = new List<DialogData>();
    }
}

public class DialogData
{
    public Scene Scene { get; set; }
    public DialogDataType Type { get; set; }
    public Prop Props { get; set; }

    public DialogData(Scene scene, DialogDataType type, Prop props)
    {
        this.Scene = scene;
        this.Type = type;
        this.Props = props;
    }
}

public class Prop
{
    public string? Cid { get; set; }
    public string? Sid { get; set; }
    public string? Standing { get; set; }
    public string? Str { get; set; }
    public List<DialogData>? Cases { get; set; }
}