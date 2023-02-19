using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

public class Deserializer
{
    public static void Deserialize(SerializableSet set)
    {
        SerializableSet localSet = null;
        try {
            string key = MagicExcel.Security.Encrypt(nameof(SerializableSet) + Application.version);
            if (PlayerPrefs.HasKey(key)) {
                string json = MagicExcel.Security.Decrypt(PlayerPrefs.GetString(key));
                localSet = JsonConvert.DeserializeObject<SerializableSet>(json);
            }
        } catch (System.Exception ex) {
            Debug.LogError(ex.ToString());
        }
       
        QuestionSheet.SetDictionary((localSet?.Questions ?? set.Questions).ToDictionary(x => x.id));
    }
}
