using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    public List<QuestionData> questionsData;
    [SerializeField] SerializableSet _set;
    private void Awake()
    {
        Deserializer.Deserialize(_set);
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

    public void ShuffleQuestionsDataList()
    {
        //List<QuestionData> tempList = new List<QuestionData>();
        //tempList.AddRange(questionsData);
        //questionsData.Clear();
        //while(tempList.Count != 0)
        //{
        //    questionsData.
        //}
        questionsData = questionsData.OrderBy(x => Random.value).ToList();
    }
}
