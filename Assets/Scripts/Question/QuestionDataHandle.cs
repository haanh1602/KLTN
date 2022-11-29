using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestionDataHandle
{
    public const int numberAnswer = 4;
    public static List<QuestionData> LoadData()
    {
        List<QuestionData> questionsData = new List<QuestionData>();
        int numberQuestion = QuestionSheet.GetDictionary().Count;
        for(int i = 0; i < numberQuestion; i++)
        {
            QuestionData question = new QuestionData();
            question.id = QuestionSheet.Get(i).id;
            question.qLevel = QuestionSheet.Get(i).level;
            question.score = QuestionSheet.Get(i).score;
            question.questionBeforeImage = QuestionSheet.Get(i).question_1;
            question.questionAfterImage = QuestionSheet.Get(i).question_2;
            question.questionAfterImage = QuestionSheet.Get(i).question_2;
            if(QuestionSheet.Get(i).image != null)
            {
                question.questionTexture2D = Resources.Load<Texture2D>("QuestionData/QuestionSprite/" + QuestionSheet.Get(i).image);
            }

            question.answer = new List<AnswerData>();
            for (int j = 0; j < numberAnswer; j++)
            {
                AnswerData answerData = new AnswerData();
                answerData.id = j;
                answerData.questionId = question.id;
                switch(j)
                {
                    case 0:
                        answerData.isRight = true;
                        answerData.content = QuestionSheet.Get(i).right_answer;
                        break;
                    case 1:
                        answerData.content = QuestionSheet.Get(i).wrong_answer_1;
                        break;
                    case 2:
                        answerData.content = QuestionSheet.Get(i).wrong_answer_2;
                        break;
                    case 3:
                        answerData.content = QuestionSheet.Get(i).wrong_answer_3;
                        break;
                }
                question.answer.Add(answerData);
            }
            questionsData.Add(question);
        }
        return questionsData;
    }
}


public class QuestionData
{
    public int id;
    public string qLevel;
    public int score;
    public string questionBeforeImage = "";
    public string questionAfterImage = "";
    public List<AnswerData> answer;

    public Texture2D questionTexture2D = null;
    
    public QuestionData() { }

    public QuestionData(QuestionData data)
    {
        this.id = data.id;
        this.qLevel = data.qLevel;
        this.score = data.score;
        this.questionBeforeImage = data.questionBeforeImage;
        this.questionAfterImage = data.questionAfterImage;
        this.questionTexture2D = data.questionTexture2D;
        this.answer = data.answer;
    }
}

public class AnswerData
{
    public int id;
    public int questionId;
    public string content;
    public bool isRight = false;

    public AnswerData() {}

    public AnswerData(int id, int questionId, string content, bool isRight)
    {
        this.id = id;
        this.questionId = questionId;
        this.content = content;
        this.isRight = isRight;
    }
}

public enum QuestionLevel
{
    Hard,
    Medium,
    Easy,
    None
}
