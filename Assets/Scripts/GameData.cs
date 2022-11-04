using System;
using System.Collections.Generic;
using System.Linq;

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
        /*Random random = new Random();
        var suf = questionsData.OrderBy(a => random.NextDouble());
        questionsData.Clear();
        questionsData.AddRange(suf);*/
    }
}
