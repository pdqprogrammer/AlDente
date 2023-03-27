using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static readonly string languageFileName = "/languageData.sav"; 
    public static void SaveData(bool isItalian)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + languageFileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        LanguageSaveData languageSaveData = new LanguageSaveData(isItalian);

        formatter.Serialize(stream, languageSaveData);
        stream.Close();
    }

    public static LanguageSaveData LoadData(bool initial = false)
    {
        string path = Application.persistentDataPath + languageFileName;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LanguageSaveData languageData = formatter.Deserialize(stream) as LanguageSaveData;
            stream.Close();

            return languageData;
        }
        else
        {
            if (!initial)
            {
                Debug.LogError("Unable to find save data" + path + "!");
            }
            
            return null;
        }
    }
}
