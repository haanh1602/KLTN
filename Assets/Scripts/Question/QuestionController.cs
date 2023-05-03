using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionBeforeImageTMP;
    [SerializeField] private Image questionImage;
    [SerializeField] private TextMeshProUGUI questionAfterImageTMP;

    [SerializeField] private Answer[] answers;

    private RectTransform _imgRectTransform;
    private Vector2 _defaultSizeDelta;

    public Action OnAnsweredQuestion = delegate {};

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
        EnemyManager._ins.activeEnemy = enemy;
        
        questionBeforeImageTMP.text = enemy.question.questionBeforeImage.Trim();
        if (enemy.question.questionTexture2D != null)
        {
            questionImage.gameObject.SetActive(true);
            DisplayImage(enemy.question.questionTexture2D);
        }
        else
        {
            questionImage.gameObject.SetActive(false);
        }
        questionAfterImageTMP.text = enemy.question.questionAfterImage.Trim();

        for (int i = 0; i < answers.Length && i < enemy.question.answer.Count; i++)
        {
            answers[i].Init(enemy.question.answer[i].content, enemy.question.answer[i].isRight);
        }

        GameManager.Instance.NewQuest();
    }

    public void OnClickWrongAnswer()
    {
        AudioManager.Instance.PlayWrongAnswer();
        
        UIManager.Instance.AnswerQuestion(false);
        GameManager.Instance.OnWrongClicked();
        foreach (var answer in answers)
        {
            if (!answer.IsRight) continue;
            answer.PlayResultAnimation(false);
            break;
        }
        DisableAnswerInteractable();
    }

    public void RemoveAnswersListener()
    {
        foreach (var answer in answers)
        {
            answer.Button.onClick.RemoveAllListeners();
        }
    }

    public void DisableAnswerInteractable()
    {
        foreach (var answer in answers)
        {
            answer.Button.onClick.RemoveAllListeners();
        }
    }

    public void OnClickRightAnswer()
    {
        AudioManager.Instance.PlayRightAnswer();
        
        UIManager.Instance.AnswerQuestion(true);
        GameManager.Instance.OnRightClicked();
        DisableAnswerInteractable();
    }

    private void ShowRightAnswerDetail()
    {
        
    }
    
    public void DisplayImage(Texture2D newTexture2D)
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
