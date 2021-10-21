using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json.Linq;

public class XmlManager : SingleTon<XmlManager>
{
    public List<Scene> LoadXml()
    {
        XmlDocument Xml = new XmlDocument();
        TextAsset XmlFile = ResourceManager.Instance.Get("data") as TextAsset;
        Xml.LoadXml(XmlFile.text);
        XmlNodeList XmlList = Xml.GetElementsByTagName("Scene");

        JObject Json = JsonManager.Instance.GetLocale();

        List<Scene> Scenes = new List<Scene>();
        for (int sceneIdx = 0; sceneIdx < XmlList.Count; ++sceneIdx)
        {
            XmlNode sceneNode = XmlList[sceneIdx];
            string sid = sceneNode.Attributes.GetNamedItem("sid")?.Value;
            string backGround = sceneNode.Attributes.GetNamedItem("background")?.Value;
            string nextScene = sceneNode.Attributes.GetNamedItem("next")?.Value;
            string bgm = sceneNode.Attributes.GetNamedItem("bgm")?.Value;

            Scene scene = new Scene(sid, backGround, nextScene, bgm);

            for (int dialogIdx = 0; dialogIdx < sceneNode.ChildNodes.Count; ++dialogIdx)
            {
                XmlNode dialogNode = XmlList[sceneIdx].ChildNodes[dialogIdx];
                string TagName = dialogNode.Name;
                switch (TagName)
                {
                    case "Text":
                        {
                            string talker = dialogNode.Attributes.GetNamedItem("talker")?.Value;
                            string standing = dialogNode.Attributes.GetNamedItem("standing")?.Value;
                            string str = dialogNode.Attributes.GetNamedItem("string")?.Value;
                            Prop props = new Prop
                            {
                                Character = talker == "narrator" ? "" : (string)Json["Character"][talker]["Name"],
                                Standing = standing,
                                Str = (string)Json["Text"][str]
                            };
                            DialogData dialogData = new DialogData(scene, DialogDataType.Text, props);
                            scene.DialogDatas.Add(dialogData);
                            break;
                        }
                    case "ShowCharacter":
                        {
                            string cid = dialogNode.Attributes.GetNamedItem("cid")?.Value;
                            string standing = dialogNode.Attributes.GetNamedItem("standing")?.Value;
                            Prop props = new Prop
                            {
                                Character = cid,
                                Standing = standing,
                            };
                            DialogData dialogData = new DialogData(scene, DialogDataType.ShowCharacter, props);
                            scene.DialogDatas.Add(dialogData);
                            break;
                        }
                    case "HideCharacter":
                        {
                            string cid = dialogNode.Attributes.GetNamedItem("cid")?.Value;
                            Prop props = new Prop
                            {
                                Character = cid
                            };
                            DialogData dialogData = new DialogData(scene, DialogDataType.HideCharacter, props);
                            scene.DialogDatas.Add(dialogData);
                            break;
                        }
                    case "Select":
                        {
                            string talker = dialogNode.Attributes.GetNamedItem("talker")?.Value;
                            string standing = dialogNode.Attributes.GetNamedItem("standing")?.Value;
                            string str = dialogNode.Attributes.GetNamedItem("string")?.Value;
                            List<DialogData> Cases = new List<DialogData>();
                            for (int caseIdx = 0; caseIdx < dialogNode.ChildNodes.Count; ++caseIdx)
                            {
                                XmlNode caseNode = dialogNode.ChildNodes[caseIdx];
                                string gotoSid = caseNode.Attributes.GetNamedItem("goto")?.Value;
                                string caseStr = caseNode.Attributes.GetNamedItem("string")?.Value;
                                Prop caseProps = new Prop
                                {
                                    Sid = gotoSid,
                                    Str = (string)Json["Text"][caseStr]
                                };
                                DialogData Case = new DialogData(scene, DialogDataType.Case, caseProps);
                                Cases.Add(Case);
                            }
                            Prop props = new Prop
                            {
                                Character = (string)Json["Character"][talker]["Name"],
                                Standing = standing,
                                Str = (string)Json["Text"][str],
                                Cases = Cases
                            };
                            DialogData dialogData = new DialogData(scene, DialogDataType.Select, props);
                            scene.DialogDatas.Add(dialogData);
                            break;
                        }
                }
            }
            Scenes.Add(scene);
        }
        return Scenes;
    }
}
