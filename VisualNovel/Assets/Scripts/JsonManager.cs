using Newtonsoft.Json.Linq;
using UnityEngine;

public class JsonManager : SingleTon<JsonManager>
{
    public string Locale { get; set; } = "ko_KR";
    public JObject GetLocale()
    {
        TextAsset JsonFile = (TextAsset)ResourceManager.Instance.Get<TextAsset>($"Localization/{Locale}");
        string jsonString = JsonFile.text;
        JObject json = JObject.Parse(jsonString);
        return json;
    }
}
