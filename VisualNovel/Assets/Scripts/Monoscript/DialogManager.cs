using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoSingleTon<DialogManager>
{
    [SerializeField]
    private List<Scene> _scenes;
    private Scene _currentScene;
    private int _sceneIdx = 0;
    private DialogData _currentDialog;
    private int _dialogIdx = 0;
    private Coroutine _coroutine;

    // set on start()
    private GameObject _selectScreen;
    private Text _dialogText;
    private Text _characterText;
    private GameObject _caseButton;
    private SpriteRenderer _backgroundSprite;
    private Text _nextButton;

    private bool _isWritingSelectText = false;

    public bool IsWritingText = false;

    public void NextDialog()
    {
        if (_dialogIdx < _currentScene.DialogDatas.Count - 1)
        {
            _dialogIdx++;
            LoadDialog();
        }
        else
        {
            int nextIdx = _scenes.FindIndex(s => s.Sid == _currentScene.NextScene);
            if (nextIdx != -1)
            {
                _sceneIdx = nextIdx;
                _dialogIdx = 0;
                LoadScene();
            }
        }
    }

    public void StopWriteText()
    {
        StopCoroutine(_coroutine);
        _dialogText.text = _currentDialog.Props.Str;
        _nextButton.text = "\n¡å  ";
        IsWritingText = false;
        ToggleMainButton();
        _coroutine = null;
        if (_isWritingSelectText)
        {
            ShowSelectScreen();
        }
    }

    public void GotoScene(string sid)
    {
        if (sid != "")
        {
            _sceneIdx = _scenes.FindIndex(s => s.Sid == sid);
            _dialogIdx = 0;
            LoadScene();
        }
        else
        {
           NextDialog();
        }
        foreach (GameObject _case in GameObject.FindGameObjectsWithTag("Case"))
        {
            Destroy(_case);
        }
        _selectScreen.SetActive(false);
    }

    public void OnResume()
    {
        LoadAsset();
        LoadScene();
    }

    void ToggleMainButton()
    {
        GameObject.Find("Canvas").transform.Find("GotoMain").gameObject.SetActive(!IsWritingText);
    }

    void Start()
    {
        LoadAsset();
        LoadScene();
    }

    void LoadAsset()
    {
        _selectScreen = GameObject.Find("Canvas").transform.Find("Select").gameObject;
        _dialogText = GameObject.Find("Talk").gameObject.GetComponent<Text>();
        _characterText = GameObject.Find("TalkerName").gameObject.GetComponent<Text>();
        _caseButton = ResourceManager.Instance.Get("Prefab/Case") as GameObject;
        _backgroundSprite = GameObject.Find("Background").gameObject.GetComponent<SpriteRenderer>();
        _nextButton = GameObject.Find("ButtonDisplay").gameObject.GetComponent<Text>();
        _scenes = XmlManager.Instance.LoadXml();
    }

    void LoadScene()
    {
        _currentScene = _scenes[_sceneIdx];
        _backgroundSprite.sprite = Resources.Load<Sprite>($"Background/{_currentScene.Background}");
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
                        if (_coroutine == null)
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
                        if (_coroutine == null)
                            _coroutine = StartCoroutine(WriteText(_currentDialog.Props.Character, _currentDialog.Props.Str, "Select"));
                        _isWritingSelectText = true;
                        break;
                    }
            }
        }
    }

    void ShowSelectScreen()
    {
        _isWritingSelectText = false;
        _selectScreen.SetActive(true);
        List<DialogData> cases = _currentDialog.Props.Cases;
        for (int caseIdx = 0; caseIdx < cases.Count; ++caseIdx)
        {
            GameObject caseObj = Instantiate(_caseButton);
            caseObj.name = $"Case {caseIdx}";
            caseObj.transform.SetParent(_selectScreen.transform);
            caseObj.transform.GetChild(0).GetComponent<Text>().text = cases[caseIdx].Props.Str;
            caseObj.GetComponent<SelectCase>().SetGoto(cases[caseIdx].Props.Sid);

            RectTransform pos = caseObj.GetComponent<RectTransform>();
            pos.anchoredPosition = new Vector2(0, (50 * (cases.Count - 1)) - (100 *  caseIdx));
            pos.localScale = new Vector3(1, 1, 1);
        }
    }

    IEnumerator WriteText(string talker, string text, string type = "")
    {
        StringBuilder sb = new StringBuilder("");
        _characterText.text = talker;
        _dialogText.text = "";
        _nextButton.text = "";
        IsWritingText = true;
        ToggleMainButton();
        for (int i = 0; i < text.Length; ++i)
        {
            sb.Append(text[i]);
            _dialogText.text = sb.ToString();
            yield return new WaitForSeconds(0.06f);
        }
        _nextButton.text = "\n¡å  ";
        IsWritingText = false;
        ToggleMainButton();
        _coroutine = null;
        if (type == "Select")
        {
            ShowSelectScreen();
        }
    }
}
