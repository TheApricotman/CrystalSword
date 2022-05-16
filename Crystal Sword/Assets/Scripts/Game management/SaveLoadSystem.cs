using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadSystem : MonoBehaviour
{
    public string SavePath => $"{Application.persistentDataPath}/save.dat";

    [ContextMenu("Save")]
    public void Save()
    {
        Debug.Log(SavePath);
        var state = LoadFile();

        SaveState(state);
        SaveFile(state);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        var state = LoadFile();
        LoadState(state);
    }

    public void SaveFile(object state)
    { 
            using (var stream = File.Open(SavePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }        
    }

    Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("File Path does not exist!");
            return new Dictionary<string, object>();
        }
        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    void SaveState(Dictionary<string, object> state)
    {
        foreach(var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.ID] = saveable.SaveState();
        }
    }
    void LoadState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            if(state.TryGetValue(saveable.ID, out object savedState))
            {
                saveable.LoadState(savedState);
            }
        }
    }
}
