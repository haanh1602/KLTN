using System.Collections.Generic;

public class GameData : Singleton<GameData>
{
    public List<QuestionData> questionsData;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void InitQuestionData()
    {
        questionsData = QuestionDataHandle.LoadData();
    }
}
