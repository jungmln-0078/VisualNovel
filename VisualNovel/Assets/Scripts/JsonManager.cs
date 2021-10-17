using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;

public class JsonManager
{
    public static string Locale { get; set; }
    public static object GetLocale()
    {
        TextAsset JsonFile = Resources.Load($"Localization/{Locale}.json") as TextAsset;
        string jsonString = JsonFile.text;
        object json = JsonConvert.DeserializeObject(jsonString);
        return json;
    }
}
