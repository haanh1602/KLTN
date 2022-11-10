using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    private QuestionData questionData;
    
    [SerializeField] private TextMeshProUGUI questionBeforeImageTMP;
    [SerializeField] private Image questionImage;
    [SerializeField] private TextMeshProUGUI questionAfterImageTMP;

    [SerializeField] private Button[] answerButtons;

    private RectTransform _imgRectTransform;
    private Vector2 _defaultSizeDelta;

    public Action OnAnsweredQuestion = delegate { };

    private void Awake()
    {
        _imgRectTransform = questionImage.GetComponent<RectTransform>();
        _defaultSizeDelta = _imgRectTransform.sizeDelta;
    }

    private void Start()
    {
        OnAnsweredQuestion += UIGameManager._ins.OpenJoystick;
    }

    public void Init(QuestionData questionData)
    {
        this.questionData = questionData;
        questionBeforeImageTMP.text = this.questionData.questionBeforeImage;
        questionAfterImageTMP.text = questionData.questionAfterImage;
        if (questionData.questionTexture2D != null)
        {
            questionImage.gameObject.SetActive(true);
            DisplayNewImage(questionData.questionTexture2D);
        }
        else
        {
            questionImage.gameObject.SetActive(false);
        }
        Debug.LogError(answerButtons.Length + ", " + questionData.answer.Count);
        for (int i = 0; i < answerButtons.Length && i < questionData.answer.Count; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = this.questionData.answer[i].content;
            answerButtons[i].onClick.RemoveAllListeners();
            var tempI = i;
            answerButtons[i].onClick.AddListener(() =>
            {
                if (questionData.answer[tempI].isRight) OnClickWrongAnswer();
                else OnClickRightAnswer(questionData.answer[tempI]);
                OnAnsweredQuestion?.Invoke();
            });
        }
    }

    private void OnClickWrongAnswer()
    {
        Debug.Log("Wrong answer!");
        GameManager.Instance.OnWrongClicked();
    }

    private void OnClickRightAnswer(AnswerData answerData)
    {
        Debug.Log("Right answer!");
        GameManager.Instance.OnRightClicked();
    }
    
    public void DisplayNewImage(Texture2D newTexture2D)
    {
        questionImage.sprite = Sprite.Create(newTexture2D, new Rect(0, 0, newTexture2D.width, newTexture2D.height), Vector2.zero);
        if (newTexture2D.height == 0 || newTexture2D.width == 0)
        {
            Debug.LogWarning(newTexture2D + " cannot display!");
            return;
        }
        questionImage.preserveAspect = true;
        questionImage.raycastTarget = false;
        Vector2 sizeDelta;
        if (newTexture2D.height > newTexture2D.width)
        {
            sizeDelta = new Vector2(_defaultSizeDelta.x,
                _defaultSizeDelta.y * (newTexture2D.height != 0? ((float) newTexture2D.height / newTexture2D.width) : 1f));
            _imgRectTransform.sizeDelta = sizeDelta;
        }
        else
        {
            sizeDelta = new Vector2(_defaultSizeDelta.x * (newTexture2D.width != 0? ((float) newTexture2D.width / newTexture2D.height) : 1f),
                _defaultSizeDelta.y); 
            _imgRectTransform.sizeDelta = sizeDelta;
        }
    }
}
