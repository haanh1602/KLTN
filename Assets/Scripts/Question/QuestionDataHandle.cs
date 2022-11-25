using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestionDataHandle
{
    public static List<QuestionData> LoadData()
    {
        var textAsset = Resources.Load<TextAsset>("QuestionData/Questions");
        var rawText = textAsset.text;
        var questionsRawData = rawText.Split(new string[] {"[Q]"}, StringSplitOptions.RemoveEmptyEntries);
        List<QuestionData> questionsData = new List<QuestionData>();
        foreach (var questionRawData in questionsRawData)
        {
            questionsData.Add(new QuestionData(questionRawData.Trim()));
        }

        return questionsData;
    }
}


public class QuestionData
{
    public string id;
    public string qLevel;
    public int score;
    public string questionBeforeImage = "";
    public string questionAfterImage = "";
    public List<AnswerData> answer;

    public Texture2D questionTexture2D = null;
    
    public QuestionData() { }

    public QuestionData(string data)
    {
        data = data.Trim();
        string[] questAndAnswer = data.Split(new string[] {"[#]"}, StringSplitOptions.RemoveEmptyEntries);
        string[] questionDetail = questAndAnswer[0].Trim().Split('~');
        string[] questionStats = questionDetail[0].Trim().Split('_');
        id = questionStats[0];
        qLevel = questionStats[1];
        Int32.TryParse(questionStats[2], out score);
        string[] rawQuestionParts = questionDetail[1].Trim().Split('@');
        bool beforeImage = true;
        for (int i = 0; i < rawQuestionParts.Length; i++)
        {
            if (i % 2 != 0)
            {
                beforeImage = false;
                questionTexture2D = Resources.Load<Texture2D>("QuestionData/QuestionSprite/" + rawQuestionParts[i].Trim());
                rawQuestionParts[i] = "";
            }
            questionBeforeImage ??= "";
            if (beforeImage)
            {
                questionBeforeImage += rawQuestionParts[i];
            }
            else
            {
                questionAfterImage += rawQuestionParts[i];
            }
        }
        string[] answersData =
            questAndAnswer[1].Trim().Split(new string[] {"[A]"}, StringSplitOptions.RemoveEmptyEntries);
        answer = new List<AnswerData>();
        for (var i = 0; i < answersData.Length; i++)
        {
            string[] answerStats = answersData[i].Split('~');
            AnswerData answerData = new AnswerData();
            answerData.id = answerStats[0];
            answerData.questionId = id;
            string answerContent = answerStats[1];
            if (answerContent.Contains("[*]"))
            {
                answerData.isRight = true;
                answerContent = answerContent.Replace("[*]", "");
            }

            answerData.content = answerContent;
            answer.Add(answerData);
        }

        /*
        var answerSuf = answer.OrderBy(a => new Guid()).ToList();
        answer.Clear();
        answer.AddRange(answerSuf);*/
    }
}

public class AnswerData
{
    public string id;
    public string questionId;
    public string content;
    public bool isRight;

    public AnswerData() { }

    public AnswerData(string id, string questionId, string content, bool isRight)
    {
        this.id = id;
        this.questionId = questionId;
        this.content = content;
        this.isRight = isRight;
    }
}

public class QuestionLevel
{
    public const string Hard = "H";
    public const string Medium = "M";
    public const string Easy = "E";
    public const string None = "";
}
