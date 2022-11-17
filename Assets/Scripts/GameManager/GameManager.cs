using System;

public class GameManager : Singleton<GameManager>
{
    public int QuestTh { get; private set; } = 0;
    public int MaxQuest { get; private set; } = 0;

    public int Score { get; private set; } = 0;

    private void Awake()
    {
        StartNewGame();
    }

    private void StartNewGame() {
        GameData.Instance.ShuffleQuestionsDataList();
        MaxQuest = GameData.Instance.questionsData.Count;
    }

    public void NextQuest()
    {
        QuestTh++;
        if (QuestTh > MaxQuest) return;
        
        UIGameManager._ins.OpenJoystick();
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
