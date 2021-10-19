using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private List<Scene> _scenes;
    private Scene _currentScene;
    private int _sceneIdx = 0;
    private DialogData _currentDialog;
    private int _dialogIdx = 0;
    private Coroutine _coroutine;
    private GameObject _selectScreen;

    public bool IsWritingText = false;
    public Text DialogText;
    public Text CharacterText;
    public GameObject Case;
    public SpriteRenderer BackgroundSprite;
    public Text NextButton;

    public void IncreaseDialogIdx()
    {
        if (_dialogIdx < _currentScene.DialogDatas.Count - 1)
        {
            _dialogIdx++;
            LoadDialog();
        }
        else
        {
            _sceneIdx = _scenes.FindIndex(s => s.Sid == _currentScene.NextScene);
            _dialogIdx = 0;
            LoadScene();
        }
    }

    public void EndWriteText()
    {
        StopCoroutine(_coroutine);
        DialogText.text = _currentDialog.Props.Str;
        NextButton.text = "\n¡å  ";
        IsWritingText = false;
    }

    public void GotoScene(string sid)
    {
        if (sid != "")
        {
            _sceneIdx = _scenes.FindIndex(s => s.Sid == sid);
            _dialogIdx = 0;
            LoadScene();
        } else
        {
            IncreaseDialogIdx();
        }
        foreach (GameObject _case in GameObject.FindGameObjectsWithTag("Case"))
        {
            Destroy(_case);
        }
        _selectScreen.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        _selectScreen = GameObject.Find("Canvas").transform.Find("Select").gameObject;
        _scenes = XmlManager.LoadXml();
        LoadScene();
    }

    void LoadScene()
    {
        _currentScene = _scenes[_sceneIdx];
        BackgroundSprite.sprite = Resources.Load<Sprite>($"Background/{_currentScene.Background}");
        LoadDialog();
    }

    void LoadDialog()
    {
        if (_dialogIdx <= _currentScene.DialogDatas.Count - 1)
        {
            _currentDialog = _currentScene.DialogDatas[_dialogIdx];
            switch (_currentDialog.Type)
            {
                case DialogDataType.Text:
                    {
                        _coroutine = StartCoroutine(WriteText(_currentDialog.Props.Character, _currentDialog.Props.Str));
                        break;
                    }
                case DialogDataType.ShowCharacter:
                    {
                        break;
                    }
                case DialogDataType.HideCharacter:
                    {
                        break;
                    }
                case DialogDataType.Select:
                    {
                        _coroutine = StartCoroutine(WriteText(_currentDialog.Props.Character, _currentDialog.Props.Str, "Select"));
                        break;
                    }
            }
        }
    }

    void ShowSelectScreen()
    {
        _selectScreen.SetActive(true);
        foreach (DialogData _case in _currentDialog.Props.Cases)
        {
            int idx = _currentDialog.Props.Cases.FindIndex(c => c.Props.Sid == _case.Props.Sid);

            GameObject caseObj = Instantiate(Case);
            caseObj.name = $"Case {idx}";
            caseObj.transform.SetParent(_selectScreen.transform);
            caseObj.transform.GetChild(0).GetComponent<Text>().text = _case.Props.Str;
            caseObj.GetComponent<SelectCase>().SetGoto(_case.Props.Sid);

            RectTransform pos = caseObj.GetComponent<RectTransform>();
            pos.anchoredPosition = new Vector2(0, 100 - (idx * 100));
            pos.localScale = new Vector3(1, 1, 1);
        }
    }

    IEnumerator WriteText(string talker, string text, string type = "")
    {
        CharacterText.text = talker;
        DialogText.text = "";
        NextButton.text = "";
        IsWritingText = true;
        for (int i = 0; i < text.Length; i++)
        {
            DialogText.text += text[i];
            yield return new WaitForSeconds(0.06f);
        }
        NextButton.text = "\n¡å  ";
        IsWritingText = false;
        if (type == "Select")
        {
            ShowSelectScreen();
        }
    }
}
