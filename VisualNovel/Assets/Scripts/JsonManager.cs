using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class JsonManager : SingleTon<JsonManager>
{
    public string Locale { get; set; } = "ko_KR";
    public JObject GetLocale()
    {
        TextAsset JsonFile = Resources.Load($"Localization/{Locale}") as TextAsset;
        string jsonString = JsonFile.text;
        JObject json = JObject.Parse(jsonString);
        return json;
    }
}
