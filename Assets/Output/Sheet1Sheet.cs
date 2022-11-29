using System.Collections.Generic;
using MagicExcel;

[System.Serializable]
public class Sheet1Sheet
{
    /// <summary>
    /// comment
    /// </summary>
    public int id;

    /// <summary>
    /// comment
    /// </summary>
    public string question;


    private static Dictionary<int, Sheet1Sheet> dictionary = new Dictionary<int, Sheet1Sheet>();

    /// <summary>
    /// Get Sheet1Sheet by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Sheet1Sheet</returns>
    public static Sheet1Sheet Get(int id)
    {
        return dictionary[id];
    }
    
    public static Dictionary<int, Sheet1Sheet> GetDictionary()
    {
        return dictionary;
    }

    public static void SetDictionary(Dictionary<int, Sheet1Sheet> dic) {
        dictionary = dic;
    }
}
