using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private BasePlayer player;
    public BasePlayer Player => player;

    private bool answering = false;
    public bool IsAnswering => answering;

    private int questIndex = 0;
    private int maxQuest = 0;
    private int score = 0;

    private int rightAnswers = 0;
    private int wrongAnswers = 0;
    private int timeLapse = 0;

    public int QuestIndex
    {
        get => questIndex;
        private set
        {
            if (questIndex != value)
            {
                if (!UIManager.IsNull) UIManager.Instance.SetQuestionIndex(value, maxQuest);
            }

            questIndex = value;
        }
    }

    public int MaxQuest { 
        get => maxQuest;
        private set
        {
            if (maxQuest != value)
            {
                if (!UIManager.IsNull) UIManager.Instance.SetQuestionIndex(questIndex, value);
            }
            maxQuest = value;
        }}

    public int Score
    {
        get => score;
        private set
        {
            if (score != value)
            {
                if (!UIManager.IsNull) UIManager.Instance.SetScore(value);
            }
            score = value;
        }
    }

    public int RightAnswers => rightAnswers;
    public int WrongAnswers => wrongAnswers;
    public int TimeLapse => timeLapse;

    public void StartAnswer()
    {
        answering = true;
        EnemyManager._ins.PauseEnemy();
    }

    public void FinishAnswer()
    {
        answering = false;
        EnemyManager._ins.ContinueEnemy();
    }
    
    private void Awake()
    {
        StartNewGame();
    }

    private void StartNewGame() {
        GameData.Instance.ShuffleQuestionsDataList();
        MaxQuest = GameData.Instance.questionsData.Count;
        StartCoroutine(IECountTime());
    }

    private IEnumerator IECountTime()
    {
        yield return new WaitForSeconds(1f);
        timeLapse++;
        if (gameObject.activeSelf) StartCoroutine(IECountTime());
    }

    public void NextQuest()
    {
        QuestIndex++;
        if (QuestIndex > MaxQuest) return;
        
        UIGameManager._ins.OpenJoystick();
    }

    public void NewQuest()
    {
        QuestIndex++;
        QuestIndex = Mathf.Min(MaxQuest, QuestIndex);
    }

    public void OnRightClicked()
    {
        Score += GameData.Instance.questionsData[QuestIndex - 1].score;
        rightAnswers++;
        //NextQuest();
    }

    public void OnWrongClicked()
    {
        //NextQuest();
        wrongAnswers++;
    }
}
