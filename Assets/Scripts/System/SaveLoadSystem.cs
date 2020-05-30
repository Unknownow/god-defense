using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadSystem : MonoBehaviour
{
    private static readonly string SavePath = Application.dataPath + "/Save/";
    private static readonly string SaveFileName = "save.json";

    public static void Save(Save save)
    {
        string saveData = JsonUtility.ToJson(save);
        Utils.WriteFile(Path.Combine(SavePath, SaveFileName), saveData);
    }

    public static Save Load()
    {
        string saveData = Utils.ReadFile(SavePath + SaveFileName);
        Save saveObject = JsonUtility.FromJson<Save>(saveData);
        return saveObject;
    }
}
