using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private BasePlayer player;
    [SerializeField] private EnemyManager enemyManager;

    public GetQuestHandle QuestHandle = new GetQuestHandle();
    
    public BasePlayer Player
    {
        get
        {
            if (player == null) player = FindObjectOfType<BasePlayer>();
            return player;
        }
    }
    public BonusPointFxManager BonusPointFxManager;
    public int ID = 0;
    private static bool answering = false;
    public static bool IsAnswering => answering;
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
            /*if (score != value)
            {
                if (!UIManager.IsNull) UIManager.Instance.SetScore(value);
            }*/
            score = value;
        }
    }

    public int RightAnswers => rightAnswers;

    public int WrongAnswers
    {
        get => wrongAnswers;
        set
        {
            if (value == 2)
            {
                // Warning
                player.ShowWarning();
            }
            wrongAnswers = value;
        }
    }
    public int TimeLapse => timeLapse;

    public bool Pass => WrongAnswers < 3;
    
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
        ID = 0;
        GameData.Instance.ShuffleQuestionsDataList();
        QuestHandle.Init(GameData.Instance.questionsData);
        MaxQuest = 10;
        enemyManager.SpawnEnemy(GameData.Instance.questionsData);
        StartCoroutine(IECountTime());
        answering = false;
        rightAnswers = 0;
        wrongAnswers = 0;
    }

    private IEnumerator IECountTime()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => EnemyManager._ins.IsPause);
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
        WrongAnswers++;
    }
}

public class GetQuestHandle
{
    private List<QuestionData> _questionDataShuffledClone = new List<QuestionData>();
    private List<QuestionData> _questionDataBeTaken = new List<QuestionData>();

    public List<QuestionData> HardQuests { get; private set; }
    public List<QuestionData> MediumQuests { get; private set; }
    public List<QuestionData> EasyQuests { get; private set; }
    
    public void Init(List<QuestionData> questionData, bool shuffle = false)
    {
        _questionDataShuffledClone = new List<QuestionData>();
        _questionDataShuffledClone.AddRange(shuffle? questionData.OrderBy(x => Random.value).ToList() : questionData);
        HardQuests = _questionDataShuffledClone.Where(quest => quest.QuestLevel() == QuestionLevel.Hard).ToList();
        MediumQuests = _questionDataShuffledClone.Where(quest => quest.QuestLevel() == QuestionLevel.Medium).ToList();
        EasyQuests = _questionDataShuffledClone.Where(quest => quest.QuestLevel() == QuestionLevel.Easy).ToList();
    }

    public QuestionData TakeAQuestionData(QuestionLevel level)
    {
        if (_questionDataShuffledClone.Count == 0)
        {
            Debug.LogError("Hết questionData!");
            return null;
        }
            
        QuestionData result = null;
        for (int i = 0; i < _questionDataShuffledClone.Count; i++)
        {
            if (_questionDataShuffledClone[i].QuestLevel() == level)
            {
                result = _questionDataShuffledClone[i];
                _questionDataShuffledClone.RemoveAt(i);
                break;
            }
        }
            
        if (result != null) _questionDataBeTaken.Add(result);
        else Debug.LogError("Không thể tìm được quest với level " + level);
            
        return result;
    }
}
