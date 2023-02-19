using System.Collections.Generic;
using MagicExcel;

[System.Serializable]
public class QuestionSheet
{
    /// <summary>
    /// comment
    /// </summary>
    public int id;

    /// <summary>
    /// comment
    /// </summary>
    public string question_1;

    /// <summary>
    /// comment
    /// </summary>
    public string image;

    /// <summary>
    /// comment
    /// </summary>
    public string question_2;

    /// <summary>
    /// comment
    /// </summary>
    public string right_answer;

    /// <summary>
    /// comment
    /// </summary>
    public string wrong_answer_1;

    /// <summary>
    /// comment
    /// </summary>
    public string wrong_answer_2;

    /// <summary>
    /// comment
    /// </summary>
    public string wrong_answer_3;

    /// <summary>
    /// comment
    /// </summary>
    public string level;

    /// <summary>
    /// comment
    /// </summary>
    public int score;


    private static Dictionary<int, QuestionSheet> dictionary = new Dictionary<int, QuestionSheet>();

    /// <summary>
    /// Get QuestionSheet by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>QuestionSheet</returns>
    public static QuestionSheet Get(int id)
    {
        return dictionary[id];
    }
    
    public static Dictionary<int, QuestionSheet> GetDictionary()
    {
        return dictionary;
    }

    public static void SetDictionary(Dictionary<int, QuestionSheet> dic) {
        dictionary = dic;
    }
}
