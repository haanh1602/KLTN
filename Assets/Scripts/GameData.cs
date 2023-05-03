using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    private const string UnlockLevelKey = "UNLOCK_LEVEL";
    
    public List<QuestionData> questionsData;
    [SerializeField] SerializableSet _set;
    private int levelUnlock = 1;
    public int PlayingLevel = 1;

    public int LevelUnlock => levelUnlock;

    protected override void Awake()
    {
        base.Awake();
        
        //Global Setting Game
        Application.targetFrameRate = 60;
        Time.timeScale = 1f;
        
        Deserializer.Deserialize(_set);
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.HasKey(UnlockLevelKey))
        {
            levelUnlock = PlayerPrefs.GetInt(UnlockLevelKey);
            if (levelUnlock < 1)
            {
                levelUnlock = 1;
                PlayerPrefs.SetInt(UnlockLevelKey, levelUnlock);
            }
        }
        else
        {
            levelUnlock = 1;
            PlayerPrefs.SetInt(UnlockLevelKey, levelUnlock);
        }
    }

    public void UnlockNewLevel()
    {
        levelUnlock = Mathf.Max(levelUnlock, PlayingLevel + 1);
        PlayerPrefs.SetInt(UnlockLevelKey, levelUnlock);
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
