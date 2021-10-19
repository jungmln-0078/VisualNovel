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

    public bool IsWritingText = false;
    public Text DialogText;
    public Text CharacterText;
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

    // Start is called before the first frame update
    void Start()
    {
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
                        break;
                    }
            }
        }
    }

    IEnumerator WriteText(string talker, string text)
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
    }
}
