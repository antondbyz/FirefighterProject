using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private string saveFileName = "save";
    [SerializeField] private bool loadSavedGameOnAwake = true;

    private string path;

    private void Awake() 
    {
        path = Application.persistentDataPath + "/" + saveFileName + ".bin";
        if(loadSavedGameOnAwake && File.Exists(path)) 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = (SaveData)(formatter.Deserialize(stream));
            data.LoadSavedData();
            stream.Close();
        }
    }

    private void OnDestroy() 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData();
        formatter.Serialize(stream, data);
        stream.Close();
    }    
}