using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    private BaseEnemy enemy;
    
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

    public void Init(BaseEnemy enemy)
    {
        this.enemy = enemy;
        questionBeforeImageTMP.text = enemy.question.questionBeforeImage;
        questionAfterImageTMP.text = enemy.question.questionAfterImage;
        if (enemy.question.questionTexture2D != null)
        {
            questionImage.gameObject.SetActive(true);
            DisplayNewImage(enemy.question.questionTexture2D);
        }
        else
        {
            questionImage.gameObject.SetActive(false);
        }
        Debug.LogError(answerButtons.Length + ", " + enemy.question.answer.Count);
        for (int i = 0; i < answerButtons.Length && i < enemy.question.answer.Count; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = this.enemy.question.answer[i].content;
            answerButtons[i].onClick.RemoveAllListeners();
            var tempI = i;
            answerButtons[i].onClick.AddListener(() =>
            {
                if (enemy.question.answer[tempI].isRight) OnClickWrongAnswer();
                else OnClickRightAnswer(enemy.question.answer[tempI]);
                OnAnsweredQuestion?.Invoke();
                EnemyManager._ins.AddToPoolEnemy(this.enemy);
                EnemyManager._ins.listAliveEnemy.Remove(this.enemy);
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
