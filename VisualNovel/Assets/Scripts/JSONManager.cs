using System;
using System.IO;
using Newtonsoft.Json;

namespace VisualNovelXML
{
    class JSONManager
    {
        public static string Locale { get; set; }
        public static object GetLocale()
        {
            StreamReader sr = new(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + $"/{Locale}.json");
            string jsonString = sr.ReadToEnd();
            object json = JsonConvert.DeserializeObject(jsonString);
            return json;
        }
    }
}
