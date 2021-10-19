using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json.Linq;

class XmlManager
{
    public static List<Scene> LoadXml()
    {
        XmlDocument Xml = new XmlDocument();
        TextAsset XmlFile = Resources.Load("data") as TextAsset;

        Xml.LoadXml(XmlFile.text);
        XmlNodeList XmlList = Xml.GetElementsByTagName("Scene");

        JObject Json = JsonManager.GetLocale();

        List<Scene> Scenes = new List<Scene>();

        foreach (XmlNode Scene in XmlList)
        {
            string sid = Scene.Attributes.GetNamedItem("sid")?.Value;
            string backGround = Scene.Attributes.GetNamedItem("background")?.Value;
            string nextScene = Scene.Attributes.GetNamedItem("next")?.Value;
            string bgm = Scene.Attributes.GetNamedItem("bgm")?.Value;

            Scene scene = new Scene(sid, backGround, nextScene, bgm);

            // 싱글톤 SceneManager, CharacterManager 선언
            // Scene이 변경되면 모든 스탠딩, 배경은 초기화된다.
            foreach (XmlNode item in Scene)
            {
                ;
                string TagName = item.Name;
                switch (TagName)
                {
                    // 기본 대화 내용 출력
                    case "Text":
                        {
                            string talker = item.Attributes.GetNamedItem("talker")?.Value;
                            string standing = item.Attributes.GetNamedItem("standing")?.Value;
                            // 로컬라이징 json 파일의 값으로 대체해야함
                            string str = item.Attributes.GetNamedItem("string")?.Value;
                            Prop props = new Prop
                            {
                                Character = talker == "narrator" ? "" : talker,
                                Standing = standing,
                                Str = (string) Json["Text"][str]
                            };
                            DialogData dialogData = new DialogData(scene, DialogDataType.Text, props);
                            scene.DialogDatas.Add(dialogData);
                            break;
                        }
                    // 대화 내용 없이 캐릭터 스탠딩을 출력할 때 사용
                    case "ShowCharacter":
                        {
                            string cid = item.Attributes.GetNamedItem("cid")?.Value;
                            string standing = item.Attributes.GetNamedItem("standing")?.Value;
                            Prop props = new Prop
                            {
                                Character = cid,
                                Standing = standing,
                            };
                            DialogData dialogData = new DialogData(scene, DialogDataType.ShowCharacter, props);
                            scene.DialogDatas.Add(dialogData);
                            break;
                        }
                    // 캐릭터 스탠딩을 삭제할 때 사용
                    case "HideCharacter":
                        {
                            string cid = item.Attributes.GetNamedItem("cid")?.Value;
                            Prop props = new Prop
                            {
                                Character = cid
                            };
                            DialogData dialogData = new DialogData(scene, DialogDataType.HideCharacter, props);
                            scene.DialogDatas.Add(dialogData);
                            break;
                        }
                    // 선택지
                    case "Select":
                        {
                            string talker = item.Attributes.GetNamedItem("talker")?.Value;
                            string standing = item.Attributes.GetNamedItem("standing")?.Value;
                            // 로컬라이징 json 파일의 값으로 대체해야함
                            string str = item.Attributes.GetNamedItem("string")?.Value;
                            List<DialogData> Cases = new List<DialogData>();
                            foreach (XmlNode _case in item)
                            {
                                string _goto = _case.Attributes.GetNamedItem("goto")?.Value;
                                string _str = item.Attributes.GetNamedItem("string")?.Value;
                                Prop _props = new Prop
                                {
                                    Sid = _goto,
                                    Str = (string)Json["Text"][_str]
                                };
                                DialogData Case = new DialogData(scene, DialogDataType.Case, _props);
                                Cases.Add(Case);
                            }
                            Prop props = new Prop
                            {
                                Character = talker,
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
