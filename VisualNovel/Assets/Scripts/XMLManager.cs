using System;
using System.IO;
using System.Xml;

namespace VisualNovelXML
{
    class XMLManager
    {
        public static void ReadXML()
        {
            XmlDocument Xml = new();
            // 유니티 스크립트용 Path로 변경해야함
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/data.xml";

            Xml.Load(path);
            XmlNodeList XmlList = Xml.GetElementsByTagName("Scene");

            foreach (XmlNode Scene in XmlList)
            {
                string sid = Scene.Attributes.GetNamedItem("sid")?.Value;
                string backGround = Scene.Attributes.GetNamedItem("background")?.Value;
                string bgm = Scene.Attributes.GetNamedItem("bgm")?.Value;

                if (backGround != null)
                {
                    // 배경 바꾸기
                }
                if (bgm != null)
                {
                    // BGM 바꾸기
                }
                // 싱글톤 SceneManager, CharacterManager 선언
                // Scene이 변경되면 모든 스탠딩, 배경은 초기화된다.
                foreach (XmlNode item in Scene)
                {
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
                                if (talker != null)
                                {
                                    // 화자 설정
                                }
                                if (standing != null)
                                {
                                    // 스탠딩 설정
                                    // 미입력시 이전 스탠딩을 유지한다.
                                }
                                foreach (char _char in str)
                                {
                                    // 텍스트 출력
                                    // 대사 속도 설정에 따라 한글자씩
                                }
                                break;
                            }
                        // 대화 내용 없이 캐릭터 스탠딩을 출력할 때 사용
                        case "ShowCharacter":
                            {
                                string cid = item.Attributes.GetNamedItem("cid")?.Value;
                                string standing = item.Attributes.GetNamedItem("standing")?.Value;
                                if (cid != null)
                                {
                                    // 캐릭터 설정
                                }
                                if (standing != null)
                                {
                                    // 스탠딩 설정
                                    // 미입력시 이전 스탠딩을 유지한다.
                                }
                                break;
                            }
                        // 캐릭터 스탠딩을 삭제할 때 사용
                        case "HideCharacter":
                            {
                                string cid = item.Attributes.GetNamedItem("cid")?.Value;
                                if (cid != null)
                                {
                                    // 해당 캐릭터 스탠딩을 삭제한다.
                                }
                                break;
                            }
                        // 선택지
                        case "Select":
                            {
                                string talker = item.Attributes.GetNamedItem("talker")?.Value;
                                string standing = item.Attributes.GetNamedItem("standing")?.Value;
                                // 로컬라이징 json 파일의 값으로 대체해야함
                                string str = item.Attributes.GetNamedItem("string")?.Value;
                                if (talker != null)
                                {
                                    // 화자 설정
                                }
                                if (standing != null)
                                {
                                    // 스탠딩 설정
                                    // 미입력시 이전 스탠딩을 유지한다.
                                }
                                if (str != null)
                                {
                                    foreach (char _char in str)
                                    {
                                        // 텍스트 출력
                                        // 대사 속도 설정에 따라 한글자씩
                                    }
                                }
                                foreach (XmlNode _case in item)
                                {
                                    string _goto = _case.Attributes.GetNamedItem("goto")?.Value;
                                    if (_goto != null)
                                    {
                                        // 선택지 창 띄우기
                                    }
                                }
                                break;
                            }
                        // 화면 효과
                        case "Effect":
                            {
                                string type = item.Attributes.GetNamedItem("type")?.Value;
                                string duration = item.Attributes.GetNamedItem("duration")?.Value;
                                switch (type)
                                {
                                    case "FadeIn":
                                        {
                                            // 페이드 인 효과
                                            break;
                                        }
                                    case "FadeOut":
                                        {
                                            // 페이드 아웃 효과
                                            break;
                                        }
                                }
                                break;
                            }
                        // 다음 씬 설정
                        // 씬의 맨 마지막에 작성한다.
                        case "NextScene":
                            {
                                sid = item.Attributes.GetNamedItem("sid")?.Value;
                                if (sid != null)
                                {
                                    // 씬 이동
                                }
                                break;
                            }
                    }
                }
            }
        }
    }
}
