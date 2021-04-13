using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private string saveFileName = "save";
    [SerializeField] private bool loadGameOnAwake = true;

    private static string path;

    private void Awake() 
    {
        path = Application.persistentDataPath + "/" + saveFileName + ".bin";
        if(loadGameOnAwake) LoadGame();
    }

#if UNITY_EDITOR || UNITY_STANDALONE
    private void OnApplicationQuit() 
    {
        SaveGame();    
    }
#else
    private void OnApplicationPause(bool pauseStatus) 
    {
        if(pauseStatus) SaveGame();
    }  
#endif

    public static void ResetGame()
    {
        SaveData data = new SaveData();
        data.ResetDataExceptSettings();
        data.InitializeGameData();
        SerializeData(data);
    }

    public static void SaveGame() => SerializeData(new SaveData());

    public static void LoadGame()
    {
        if(File.Exists(path)) 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = (SaveData)(formatter.Deserialize(stream));
            data.InitializeGameData();
            stream.Close();
        }
    }

    private static void SerializeData(SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }
}