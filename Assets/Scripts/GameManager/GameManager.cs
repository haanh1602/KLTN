using System;

public class GameManager : Singleton<GameManager>
{
    public int QuestTh { get; private set; } = 0;
    public int MaxQuest { get; private set; } = 0;

    public int Score { get; private set; } = 0;

    private void Awake()
    {
        MaxQuest = GameData.Instance.questionsData.Count;
        UIManager.Instance.questionController.Init(GameData.Instance.questionsData[QuestTh]);
    }

    public void NextQuest()
    {
        QuestTh++;
        if (QuestTh > MaxQuest) return;
        UIManager.Instance.questionController.Init(GameData.Instance.questionsData[QuestTh]);
    }

    public void OnRightClicked()
    {
        Score += GameData.Instance.questionsData[QuestTh].score;
        NextQuest();
    }

    public void OnWrongClicked()
    {
        NextQuest();
    }
}
